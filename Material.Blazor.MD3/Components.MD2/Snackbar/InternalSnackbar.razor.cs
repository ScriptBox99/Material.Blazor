﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Material.Blazor.Internal.MD2;

public partial class InternalSnackbar : ComponentFoundationMD2
{
    /// <summary>
    /// The unique <see cref="SnackbarInstance"/> for this snackbar.
    /// </summary>
    [Parameter] public SnackbarInstance Snackbar { get; set; }


    private ElementReference SnackbarReference { get; set; }

    private string Stacked => Snackbar.Settings.AppliedStacked ? "mdc-snackbar--stacked" : null;

    private string Leading => Snackbar.Settings.AppliedLeading ? "mdc-snackbar--leading" : null;

    private DotNetObjectReference<InternalSnackbar> ObjectReference { get; set; }


    // Would like to use <inheritdoc/> however DocFX cannot resolve to references outside Material.Blazor
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        ObjectReference = DotNetObjectReference.Create(this);
    }


    // Would like to use <inheritdoc/> however DocFX cannot resolve to references outside Material.Blazor
    protected override void Dispose(bool disposing)
    {
        ObjectReference?.Dispose();
        base.Dispose(disposing);
    }


    /// <summary>
    /// Called by Material Components Web when a snackbar is closed, setting the Closed parameter in settings
    /// and calling any OnClose listeners.
    /// </summary>
    [JSInvokable]
    public void Closed()
    {
        if (Snackbar.Settings.Closed)
        {
            return;
        }

        Snackbar.Settings.Closed = true;
        _ = Snackbar.Settings.OnClose?.Invoke(Snackbar);
    }


    /// <inheritdoc/>
    internal override Task InstantiateMcwComponent()
    {
        return InvokeJsVoidAsync("MaterialBlazor.MBSnackbar.init", SnackbarReference, ObjectReference, Snackbar.Settings.AppliedTimeout);
    }
}
