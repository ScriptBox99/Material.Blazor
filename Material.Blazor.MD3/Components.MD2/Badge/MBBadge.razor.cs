﻿using Material.Blazor.Internal.MD2;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Material.Blazor.MD2;

/// <summary>
/// A plus component badge placed at the top right of the containing div, overlapping the corner, but inside margin and padding..
/// </summary>
public partial class MBBadge : ComponentFoundationMD2
{
    /// <summary>
    /// The badge's style - see <see cref="MBBadgeStyle"/>, defaults to <see cref="MBBadgeStyle.ValueBearing"/>.
    /// </summary>
    [Parameter] public MBBadgeStyle BadgeStyle { get; set; } = MBBadgeStyle.ValueBearing;


    /// <summary>
    /// The button's density.
    /// </summary>
    [Parameter] public string Value { get; set; }


    /// <summary>
    /// When true collapses the badge.
    /// </summary>
    [Parameter] public bool Exited { get; set; }


    // Would like to use <inheritdoc/> however DocFX cannot resolve to references outside Material.Blazor
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _ = ConditionalCssClasses
            .AddIf("mb-badge--dot", () => BadgeStyle == MBBadgeStyle.Dot)
            .AddIf("mb-badge--exited", () => Exited);
    }


    /// <summary>
    /// Sets the exited value and calls SHC.
    /// </summary>
    internal async Task SetValueAndExited(string value, bool exited)
    {
        Value = value;
        Exited = exited;
        await InvokeAsync(StateHasChanged).ConfigureAwait(false);
    }
}
