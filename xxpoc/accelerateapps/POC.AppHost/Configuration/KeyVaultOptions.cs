// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace POC.AppHost.Configuration;
internal sealed class KeyVaultOptions
{
    public string Name { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string TenantId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;
}
