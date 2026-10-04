| Loading strategy | Data | Behaviour |
|---|---|---|
| **Preload at startup** | `obj_table`, `obj_type` | Small shared catalogues needed to resolve object tables and semantic types |
| **Preload at startup** | `sys_currency`, `sys_language`, `sys_country` | Frequently used reference data |
| **Load when a module opens** | `sys_um`, `sys_um_class`, `sys_um_relation`, `sys_um_collection` | Warm the units catalogue when an inventory, manufacturing or engineering module needs it |
| **Load on demand** | Country regions and translations | Load by country and requested language |
| **Load on demand** | Business partners, invoices, products, device descriptions | Retrieve individual records or bounded pages; cache only useful read models |
| **Load on demand** | `obj_id`, `obj_dyn`, `obj_dyn_val_*` | Load identities and selected metadata for the requested objects |
| **Read fresh for critical operations** | Permissions, stock availability, posting state, command eligibility | Validate authoritative state when executing the operation |
| **Do not retain in the general cache** | Large blobs, file contents, audit histories, telemetry history, authentication secrets | Stream or page through dedicated services |

