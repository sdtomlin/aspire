// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Configuration;
using POC.AppHost.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr(options =>
{
    options.EnableTelemetry = true;
});

var cosmosConfig = builder.Configuration.GetSection("Cosmos").Get<CosmosOptions>() ?? new CosmosOptions();
var keyVaultConfig = builder.Configuration.GetSection("KeyVault").Get<KeyVaultOptions>() ?? new KeyVaultOptions();

builder.AddContainer("hello", "sebafo/containerapp", "v1")
    .WithEndpoint(3000, "http", "Hello", "PORT");

if (cosmosConfig.UseEmulator)
{
    builder.AddAzureCosmosDB(cosmosConfig.DatabaseName).UseEmulator(cosmosConfig.EmulatorPort);
}

//builder.AddContainer("aa-core-credentials-api", "tsssharedprdaueacr.azurecr.io/aplt/aa-core-credentials-api")
//    .WithEnvironment((env) =>
//    {
//        env.EnvironmentVariables.Add("AZURE_VAULT_NAME", keyVaultConfig.Name);
//        env.EnvironmentVariables.Add("AZURE_CLIENT_ID", keyVaultConfig.ClientId);
//        env.EnvironmentVariables.Add("AZURE_TENANT_ID", keyVaultConfig.TenantId);
//        env.EnvironmentVariables.Add("AZURE_CLIENT_SECRET", keyVaultConfig.ClientSecret);
//        env.EnvironmentVariables.Add("Cosmos__Url", cosmosConfig.EndpointUrl);
//        env.EnvironmentVariables.Add("Cosmos__Key", cosmosConfig.Key);
//        env.EnvironmentVariables.Add("Cosmos__Database", cosmosConfig.DatabaseName);
//        env.EnvironmentVariables.Add("Dapr__Cache", "aacache");
//        env.EnvironmentVariables.Add("Dapr__PubSub", "aapubsub");
//        env.EnvironmentVariables.Add("DAPR_CACHE", "aacache");
//        env.EnvironmentVariables.Add("DAPR_PUBSUB", "aapubsub");
//    })
//    .WithDaprSidecar("aa-core-credentials-api-dapr");

//builder.AddProject<Projects.AA_Core_Notifications_API>("aa-core-platform")
//    .WithDaprSidecar("aa-core-platform-api-dapr");

builder.Build().Run();
