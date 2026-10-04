using System;
using System.Diagnostics;
using System.IO;
using Env = System.Environment;

namespace Kt.Kernel.FileSystem;


/// <summary>
/// Windows/Linux directory helpers for .NET 10. No Windows-specific dependencies.
/// Paths belong to the account running the process (the service account for an API).
/// Methods return an empty string on directory-resolution or creation failure.
/// Callers must check this before combining the result with a filename.
/// </summary>
public class KtSystemFolder {
    public const string PloDirectoryName = "PaperLess Office";


    // Assumption: the supplied source did not contain a QB directory name.
    public const string AppDirectoryName = "KatrixAI";



    /// <summary>
    /// Resolves the per-user local data root, without creating it.
    /// Windows: LocalApplicationData (normally %LOCALAPPDATA%).
    /// Linux: absolute XDG_DATA_HOME, otherwise ~/.local/share.
    /// The legacy name is retained; this is a data directory, not XDG_CONFIG_HOME.
    /// </summary>
    public static string GetDirLocalSettings() => TryGetDirectory(() => {
        if (OperatingSystem.IsWindows()) {
            return GetSpecialFolder(Env.SpecialFolder.LocalApplicationData);
        }

        EnsureLinux();
        string? xdg = Env.GetEnvironmentVariable("XDG_DATA_HOME");
        if (!string.IsNullOrWhiteSpace(xdg) && Path.IsPathFullyQualified(xdg)) {
            return xdg;
        }

        return Path.Combine(GetSpecialFolder(Env.SpecialFolder.UserProfile), ".local", "share");
    }, create: false);

    /// <summary>
    /// Creates and returns an application directory under the per-user data root.
    /// sApp must be one directory name, not an absolute path or nested path.
    /// Returns an empty string on failure, matching the legacy error convention.
    /// </summary>
    public static string GetDirLocalSettings(string sApp) => TryGetDirectory(() => {
        ValidateDirectoryName(sApp);
        string root = GetDirLocalSettings();
        if (string.IsNullOrWhiteSpace(root)) {
            throw new IOException("The per-user data directory could not be resolved.");
        }
        return Path.Combine(root, sApp);
    }, create: true);

    /// <summary>
    /// Creates and returns shared, machine-wide PLO storage.
    /// Windows: CommonApplicationData/PaperLess Office (legacy location).
    /// Linux: /var/lib/PaperLess Office.
    /// KT_PLO_STORAGE_DIR overrides the complete directory on either OS and
    /// must be an absolute path. Provision this directory with write access
    /// for the service account. There is no silent fallback to per-user storage.
    /// </summary>
    public static string GetDirLocalStorage() => TryGetDirectory(() => {
        string? configured = Env.GetEnvironmentVariable("KT_PLO_STORAGE_DIR");
        if (!string.IsNullOrWhiteSpace(configured)) {
            if (!Path.IsPathFullyQualified(configured)) {
                throw new ArgumentException("KT_PLO_STORAGE_DIR must be an absolute path.");
            }
            return configured;
        }

        if (OperatingSystem.IsWindows()) {
            return Path.Combine(GetSpecialFolder(Env.SpecialFolder.CommonApplicationData), PloDirectoryName);
        }

        EnsureLinux();
        return Path.Combine("/var/lib", PloDirectoryName);
    }, create: true);

    /// <summary>Creates and returns the current account's PLO settings directory.</summary>
    public static string GetDirPloSettings() => GetDirLocalSettings(PloDirectoryName);

    /// <summary>Creates and returns the current account's QuadriBot settings directory.</summary>
    public static string GetDirQbSettings() => GetDirLocalSettings(AppDirectoryName);

    private static string GetSpecialFolder(Env.SpecialFolder folder) {
        string path = Env.GetFolderPath(folder, Env.SpecialFolderOption.DoNotVerify);
        if (string.IsNullOrWhiteSpace(path) || !Path.IsPathFullyQualified(path)) {
            throw new IOException($"Cannot resolve an absolute path for {folder}.");
        }
        return path;
    }

    private static void EnsureLinux() {
        if (!OperatingSystem.IsLinux()) {
            throw new PlatformNotSupportedException("Only Windows and Linux are supported.");
        }
    }

    private static void ValidateDirectoryName(string name) {
        if (string.IsNullOrWhiteSpace(name) || name == "." || name == ".." ||
            name.IndexOfAny(new[] { '/', '\\', ':' }) >= 0 ||
            name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) {
            throw new ArgumentException("Provide a single application directory name.", nameof(name));
        }
    }

    private static string TryGetDirectory(Func<string> resolve, bool create) {
        try {
            string path = resolve();
            // Never allow an unresolved root to become a current-directory path.
            if (string.IsNullOrWhiteSpace(path) || !Path.IsPathFullyQualified(path)) {
                throw new IOException("The directory must have an absolute path.");
            }
            path = Path.GetFullPath(path);
            if (create) {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        catch (Exception ex) when (ex is IOException ||
                                   ex is UnauthorizedAccessException ||
                                   ex is ArgumentException ||
                                   ex is NotSupportedException ||
                                   ex is System.Security.SecurityException) {
            Trace.TraceError($"Cannot resolve or create application directory: {ex.Message}");
            return string.Empty;
        }
    }
}
