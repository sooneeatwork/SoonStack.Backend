# setup-secrets.ps1
Write-Host "Setting up development secrets..."

# Navigate to the API project directory
cd AI.Gateway.Api

# Initialize user secrets if not already initialized
dotnet user-secrets init

# Set the secrets
Write-Host "Setting Azure OpenAI configuration..."
dotnet user-secrets set "Azure:OpenAI:DeploymentName" "your-deployment-name"
dotnet user-secrets set "Azure:OpenAI:Endpoint" "your-endpoint"
dotnet user-secrets set "Azure:OpenAI:ApiKey" "your-key"

Write-Host "Secrets setup complete! Please verify using 'dotnet user-secrets list'"