

| Route | Purpose |
|---|---|
| `GET /api/v1/health` | Process liveness; implemented above |
| `GET /api/v1/session/check` | Validate current authentication; implemented above |
| `GET /api/v1/me` | Return the authenticated user’s basic profile |
| `GET /api/v1/me/contexts` | List permitted company and personal contexts |
| `GET /api/v1/me/applications` | List applications permitted in a validated context |
| `GET /api/v1/reference/...` | Read approved reference data through the cache |
| `GET /api/v1/objects/{id}` | Read a global object after scope and resource checks |


777777777777777776yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy