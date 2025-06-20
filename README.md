# .NET 8 Web API with OAuth2/OpenID Connect

This project is a .NET 8 Web API featuring:
- Products API controller
- OAuth2 and OpenID Connect authentication (IdentityServer)
- Well-known OpenID endpoints
- Auth and refresh tokens
- OAuth scopes and claims
- Local SQL Server storage for OAuth config (clients, tokens, etc.)
- Swagger for API documentation

## Getting Started
1. Ensure you have .NET 8 SDK and SQL Server installed.
2. Update the connection string in `appsettings.json` for your local SQL Server.
3. Run the project:
   ```pwsh
   dotnet run
   ```
4. Access Swagger UI at `/swagger`.

## Next Steps
- Configure IdentityServer for OAuth2/OpenID Connect.
- Implement Products API controller.
- Set up database migrations for IdentityServer config and operational data.
