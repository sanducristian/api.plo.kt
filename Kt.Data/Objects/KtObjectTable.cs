namespace Kt.Data.Objects;

/// <summary>Trusted server configuration: never build a projection from a client's column list.</summary>
public sealed class KtObjectTable {
    public string Name { get; }
    public IReadOnlyList<string> Columns { get; }
    public KtObjectTable(string name, params string[] columns) {
        Validate(name);
        if (columns.Length == 0 || !columns.Contains("id", StringComparer.Ordinal))
            throw new ArgumentException("An explicit projection including id is required.");
        foreach (var column in columns) Validate(column);
        if (columns.Distinct(StringComparer.Ordinal).Count() != columns.Length)
            throw new ArgumentException("Duplicate column.");
        Name = name;
        Columns = Array.AsReadOnly((string[])columns.Clone());
    }
    private static void Validate(string name) {
        if (string.IsNullOrEmpty(name) || name.Length > 196 ||
            !name.All(c => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9' or '_'))
            throw new ArgumentException("Invalid SQL identifier.");
    }
    /// <summary>Reviewed projections from the September 26 schema. Include only needed static tables.</summary>
    public static IReadOnlyList<KtObjectTable> KnownTables { get; } = Array.AsReadOnly(new KtObjectTable[]
    {
        new("obj_dyn", "id", "masterId", "slaveId", "typeId"),
        new("obj_dyn_file", "id", "fileName", "fileSize", "fileChecksum", "checksumAlgorithm", "fileTypename", "storageProvider", "storageKey", "contentUrl", "apiReference", "accessMode", "malwareScanStatus", "metadataJson"),
        new("obj_dyn_geo_pose", "id", "workspace_id", "owner_object_id", "pose_role_code", "latitude_deg", "longitude_deg", "horizontal_crs_code", "reference_frame_name", "coordinate_epoch_year", "altitude_m", "altitude_reference", "vertical_crs_code", "heading_deg", "pitch_deg", "roll_deg", "attitude_convention", "horizontal_accuracy_m", "vertical_accuracy_m", "attitude_accuracy_deg", "fix_type", "rtk_correction_age_s", "rtk_base_reference", "antenna_reference_point", "observed_at_utc", "valid_from_utc", "valid_to_utc", "survey_evidence_file_id", "created_at_utc", "created_by_principal_id", "active_owner_id"),
        new("obj_dyn_location", "id", "country", "county", "department", "city", "street", "building", "zipcode"),
        new("obj_dyn_number", "id", "value", "year", "month", "roleId"),
        new("obj_dyn_number_class", "id", "typeId", "prefix", "value", "sufix", "resetEachYear", "resetEachMonth"),
        new("obj_dyn_price", "id", "currency_id", "value", "date"),
        new("obj_dyn_quantity", "id", "umId", "value"),
        new("obj_dyn_val_attrib", "id", "attrName", "attrValue"),
        new("obj_dyn_val_blob", "id", "data"),
        new("obj_dyn_val_date", "id", "value"),
        new("obj_dyn_val_datetime", "id", "value"),
        new("obj_dyn_val_double", "id", "value"),
        new("obj_dyn_val_generic", "id", "value"),
        new("obj_dyn_val_int", "id", "value"),
        new("obj_dyn_val_string", "id", "languageId", "data"),
        new("obj_dyn_val_time", "id", "value"),
        new("sys_um", "id", "uuid", "class_id", "unit_symbol", "is_base_unit", "decimal_precision", "is_active", "unit_name", "created_utc", "updated_utc"),
        new("sys_um_class", "id", "uuid", "class_code", "class_name", "base_unit_id", "is_active", "created_utc", "updated_utc"),
        new("sys_um_collection", "id", "uuid", "collection_code", "umId", "collection_symbol", "base_unit_id", "base_quantity", "is_active", "collection_name", "created_utc", "updated_utc"),
        new("sys_um_relation", "id", "uuid", "from_unit_id", "to_unit_id", "gain_factor", "offset_value", "relation_type", "is_bidirectional", "is_active", "created_utc"),
        new("sys_currency", "id", "name", "short_name", "iso_code", "numeric_code", "symbol", "minor_unit", "is_active", "created_at_utc", "updated_at_utc", "row_version"),
        new("sys_language", "id", "language_name", "native_name", "iso_code_2", "iso_code_3", "locale_code", "legacy_code", "description"),
        new("core_location", "id", "public_id", "workspace_id", "parent_id", "type_code", "code", "name", "responsible_party_id", "address_object_id", "address_snapshot_json", "time_zone_id", "latitude", "longitude", "altitude_meters", "local_frame_location_id", "pos_x_meters", "pos_y_meters", "pos_z_meters", "is_active", "created_utc", "created_by_principal_id", "updated_utc", "row_version"),
        new("device_unit", "id", "public_id", "module_ref", "device_model_id", "internal_serial_number", "manufacturer_serial_number", "hardware_revision", "manufactured_at_utc", "released_at_utc", "lifecycle_status", "configuration_json", "legacy_object_id", "created_at_utc", "created_by_principal_id", "updated_at_utc", "row_version"),
        new("legacy_component", "id", "manufacturerId", "weight", "douaneId"),
    });
}
