// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace POC.AppHost.Configuration;
internal sealed class CosmosOptions
{
    public bool UseEmulator { get; set; } = true;
    public string EndpointUrl { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;

    public int? EmulatorPort => string.IsNullOrEmpty(EndpointUrl) ? null : new Uri(EndpointUrl).Port;
}
