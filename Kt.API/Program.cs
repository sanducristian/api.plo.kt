using Kt.Api;
using Kt.Api.Authentication;
using Microsoft.AspNetCore.HttpOverrides;


// ASP.NET Core (.NET 10). Replace the Program.cs in a webapiaot project.
// No extra NuGet packages. Utf8JsonWriter works without reflection under AOT.
public static class Program {
    private const string LoginLimit = "KtLogin";


    public static void Main(string[] args) {
        var builder = WebApplication.CreateSlimBuilder(args);
        builder.Services.Configure<ForwardedHeadersOptions>(options => {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            // Keep the default loopback-only proxy trust for local Nginx.
        });

        // Your database adapter implementing IKtIdentityBackend.
        builder.Services.AddKtAuthentication<MyIdentityBackend>();
        builder.WebHost.UseKestrelHttpsConfiguration();

        var app = builder.Build();
        app.UseForwardedHeaders();
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseRateLimiter();
        app.UseAuthentication();
        app.UseAuthorization();

        // Registers login, current-user and logout endpoints.
        app.MapKtAuthentication();
        app.MapKtApi(KtAuthenticationApi.Policy);

        app.MapGet("/api/v1/health", () => TypedResults.Text("PLO API: OK"));
        app.MapGet("/api/example", () => Results.Ok("Authenticated")).RequireAuthorization(KtAuthenticationApi.Policy);
        //var group = app.MapGroup("/api/auth");
        //group.MapPost("/login", LoginAsync)
        //     .AllowAnonymous()
        //     .RequireRateLimiting(LoginLimit);

        // Never expose request credentials/claims through production diagnostics.
        if (app.Environment.IsDevelopment()) {
            app.MapGet("/api/v1/debug/auth", (HttpContext context) => AuthInspector.Inspect(context));
        }

        app.Run();
    }

}
