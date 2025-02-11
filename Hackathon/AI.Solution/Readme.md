## Development Setup

### Setting up Secrets

1. Get the required Azure OpenAI credentials from your team lead:
   - Deployment Name
   - Endpoint
   - API Key

2. Run the setup script:
   - On Windows: `.\setup-secrets.ps1`
   - On Linux/Mac: `./setup-secrets.sh`

3. Verify your secrets are set correctly:

   ```bash
   cd AI.Gateway.Api
   dotnet user-secrets list
