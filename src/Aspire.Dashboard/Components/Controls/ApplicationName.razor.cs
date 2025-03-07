// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using Aspire.Dashboard.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Aspire.Dashboard.Components;

public sealed partial class ApplicationName : ComponentBase, IDisposable
{
    private CancellationTokenSource? _disposalCts;

    [Parameter]
    public required string ResourceName { get; init; }

    [Parameter]
    public required IStringLocalizer Loc { get; init; }

    [Inject]
    public required IDashboardClient DashboardClient { get; init; }

    private string? _applicationName;

    protected override async Task OnInitializedAsync()
    {
        // We won't have an application name until the client has connected to the server.
        if (!DashboardClient.WhenConnected.IsCompletedSuccessfully)
        {
            _disposalCts = new CancellationTokenSource();
            await DashboardClient.WhenConnected.WaitAsync(_disposalCts.Token);
        }

        _applicationName = string.Format(CultureInfo.InvariantCulture, Loc[ResourceName], DashboardClient.ApplicationName);
    }

    public void Dispose()
    {
        _disposalCts?.Cancel();
        _disposalCts?.Dispose();
    }
}
