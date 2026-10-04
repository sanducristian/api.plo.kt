// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_geo_pose table. Includes all 30 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_geo_pose")]
public sealed record ObjectDynGeoPose {
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id; master_id must equal owner_object_id and role_id the registered geo.pose role</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: owner_object_id; SQL: bigint unsigned; not null.</summary>
    [Column("owner_object_id", TypeName = "bigint unsigned")]
    public required ulong OwnerObjectId { get; init; }

    /// <summary>Column: pose_role_code; SQL: varchar(32); not null.</summary>
    [Column("pose_role_code", TypeName = "varchar(32)")]
    public required string PoseRoleCode { get; init; }

    /// <summary>Column: latitude_deg; SQL: decimal(12,9); not null. Geodetic latitude in degrees</summary>
    [Column("latitude_deg", TypeName = "decimal(12,9)")]
    public required decimal LatitudeDeg { get; init; }

    /// <summary>Column: longitude_deg; SQL: decimal(12,9); not null. Geodetic longitude in degrees</summary>
    [Column("longitude_deg", TypeName = "decimal(12,9)")]
    public required decimal LongitudeDeg { get; init; }

    /// <summary>Column: horizontal_crs_code; SQL: varchar(64); not null.</summary>
    [Column("horizontal_crs_code", TypeName = "varchar(64)")]
    public required string HorizontalCrsCode { get; init; }

    /// <summary>Column: reference_frame_name; SQL: varchar(128); nullable. Exact RTK frame realization when known</summary>
    [Column("reference_frame_name", TypeName = "varchar(128)")]
    public string? ReferenceFrameName { get; init; }

    /// <summary>Column: coordinate_epoch_year; SQL: decimal(9,5); nullable.</summary>
    [Column("coordinate_epoch_year", TypeName = "decimal(9,5)")]
    public decimal? CoordinateEpochYear { get; init; }

    /// <summary>Column: altitude_m; SQL: decimal(14,6); nullable.</summary>
    [Column("altitude_m", TypeName = "decimal(14,6)")]
    public decimal? AltitudeM { get; init; }

    /// <summary>Column: altitude_reference; SQL: varchar(24); not null. ELLIPSOIDAL, ORTHOMETRIC, LOCAL or UNKNOWN</summary>
    [Column("altitude_reference", TypeName = "varchar(24)")]
    public required string AltitudeReference { get; init; }

    /// <summary>Column: vertical_crs_code; SQL: varchar(128); nullable. Vertical datum/geoid model or named local datum</summary>
    [Column("vertical_crs_code", TypeName = "varchar(128)")]
    public string? VerticalCrsCode { get; init; }

    /// <summary>Column: heading_deg; SQL: decimal(9,6); nullable. Clockwise from true north; not magnetic heading or GNSS course</summary>
    [Column("heading_deg", TypeName = "decimal(9,6)")]
    public decimal? HeadingDeg { get; init; }

    /// <summary>Column: pitch_deg; SQL: decimal(9,6); nullable. Positive nose-up</summary>
    [Column("pitch_deg", TypeName = "decimal(9,6)")]
    public decimal? PitchDeg { get; init; }

    /// <summary>Column: roll_deg; SQL: decimal(9,6); nullable. Positive right-side-down</summary>
    [Column("roll_deg", TypeName = "decimal(9,6)")]
    public decimal? RollDeg { get; init; }

    /// <summary>Column: attitude_convention; SQL: varchar(48); not null.</summary>
    [Column("attitude_convention", TypeName = "varchar(48)")]
    public required string AttitudeConvention { get; init; }

    /// <summary>Column: horizontal_accuracy_m; SQL: decimal(12,6); nullable.</summary>
    [Column("horizontal_accuracy_m", TypeName = "decimal(12,6)")]
    public decimal? HorizontalAccuracyM { get; init; }

    /// <summary>Column: vertical_accuracy_m; SQL: decimal(12,6); nullable.</summary>
    [Column("vertical_accuracy_m", TypeName = "decimal(12,6)")]
    public decimal? VerticalAccuracyM { get; init; }

    /// <summary>Column: attitude_accuracy_deg; SQL: decimal(9,6); nullable.</summary>
    [Column("attitude_accuracy_deg", TypeName = "decimal(9,6)")]
    public decimal? AttitudeAccuracyDeg { get; init; }

    /// <summary>Column: fix_type; SQL: varchar(24); not null.</summary>
    [Column("fix_type", TypeName = "varchar(24)")]
    public required string FixType { get; init; }

    /// <summary>Column: rtk_correction_age_s; SQL: decimal(12,3); nullable.</summary>
    [Column("rtk_correction_age_s", TypeName = "decimal(12,3)")]
    public decimal? RtkCorrectionAgeS { get; init; }

    /// <summary>Column: rtk_base_reference; SQL: varchar(128); nullable.</summary>
    [Column("rtk_base_reference", TypeName = "varchar(128)")]
    public string? RtkBaseReference { get; init; }

    /// <summary>Column: antenna_reference_point; SQL: varchar(128); nullable. Physical point to which coordinates refer; record lever arm externally if needed</summary>
    [Column("antenna_reference_point", TypeName = "varchar(128)")]
    public string? AntennaReferencePoint { get; init; }

    /// <summary>Column: observed_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("observed_at_utc", TypeName = "datetime(6)")]
    public required DateTime ObservedAtUtc { get; init; }

    /// <summary>Column: valid_from_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_from_utc", TypeName = "datetime(6)")]
    public required DateTime ValidFromUtc { get; init; }

    /// <summary>Column: valid_to_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_to_utc", TypeName = "datetime(6)")]
    public DateTime? ValidToUtc { get; init; }

    /// <summary>Column: survey_evidence_file_id; SQL: bigint unsigned; nullable.</summary>
    [Column("survey_evidence_file_id", TypeName = "bigint unsigned")]
    public ulong? SurveyEvidenceFileId { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: active_owner_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_owner_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveOwnerId { get; init; }
}
