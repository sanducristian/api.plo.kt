// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_file table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_file")]
public sealed record ObjectDynFile {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: fileName; SQL: varchar(250); not null. The original filename</summary>
    [Column("fileName", TypeName = "varchar(250)")]
    public required string FileName { get; init; }

    /// <summary>Column: fileSize; SQL: bigint unsigned; not null. The file size</summary>
    [Column("fileSize", TypeName = "bigint unsigned")]
    public required ulong FileSize { get; init; }

    /// <summary>Column: fileChecksum; SQL: varchar(64); not null. The file checksum</summary>
    [Column("fileChecksum", TypeName = "varchar(64)")]
    public required string FileChecksum { get; init; }

    /// <summary>Column: checksumAlgorithm; SQL: varchar(16); not null. Algorithm used by fileChecksum; existing 64-character values should be verified as SHA-256</summary>
    [Column("checksumAlgorithm", TypeName = "varchar(16)")]
    public required string ChecksumAlgorithm { get; init; }

    /// <summary>Column: fileTypename; SQL: varchar(64); nullable. The file type name as referenced by a browser</summary>
    [Column("fileTypename", TypeName = "varchar(64)")]
    public string? FileTypename { get; init; }

    /// <summary>Column: storageProvider; SQL: varchar(32); nullable. FileDrive, S3, AzureBlob, Local, SharePoint, or another configured provider</summary>
    [Column("storageProvider", TypeName = "varchar(32)")]
    public string? StorageProvider { get; init; }

    /// <summary>Column: storageKey; SQL: varchar(512); nullable. Provider-specific immutable object, drive item, or path identifier</summary>
    [Column("storageKey", TypeName = "varchar(512)")]
    public string? StorageKey { get; init; }

    /// <summary>Column: contentUrl; SQL: varchar(2048); nullable. Stable canonical URL; never store an expiring signed download URL</summary>
    [Column("contentUrl", TypeName = "varchar(2048)")]
    public string? ContentUrl { get; init; }

    /// <summary>Column: apiReference; SQL: varchar(512); nullable. Stable API route or opaque external API identifier used to resolve the file</summary>
    [Column("apiReference", TypeName = "varchar(512)")]
    public string? ApiReference { get; init; }

    /// <summary>Column: accessMode; SQL: varchar(24); not null. How content is obtained: PRIVATE, AUTHENTICATED_URL, PUBLIC_URL, or API_ONLY</summary>
    [Column("accessMode", TypeName = "varchar(24)")]
    public required string AccessMode { get; init; }

    /// <summary>Column: malwareScanStatus; SQL: varchar(24); not null. NOT_SCANNED, PENDING, CLEAN, REJECTED, or FAILED</summary>
    [Column("malwareScanStatus", TypeName = "varchar(24)")]
    public required string MalwareScanStatus { get; init; }

    /// <summary>Column: metadataJson; SQL: json; nullable. Provider-specific metadata that is not used for accounting decisions</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("metadataJson", TypeName = "json")]
    public string? MetadataJson { get; init; }
}
