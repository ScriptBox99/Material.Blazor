﻿using Material.Blazor.MD2;

using Microsoft.Extensions.Options;

using System;

namespace Material.Blazor.Internal.MD2;

/// <summary>
/// The internal implementation of <see cref="IMBSnackbarService"/>.
/// </summary>
internal class SnackbarService : IMBSnackbarService
{
    private MBSnackbarServiceConfiguration configuration = new();

    ///<inheritdoc/>
    public MBSnackbarServiceConfiguration Configuration
    {
        get => configuration;
        set
        {
            configuration = value;
            configuration.OnValueChanged += ConfigurationChanged;
            OnTriggerStateHasChanged?.Invoke();
        }
    }


    private event Action<MBSnackbarSettings> OnAdd;
    private event Action OnTriggerStateHasChanged;


    ///<inheritdoc/>
    event Action<MBSnackbarSettings> IMBSnackbarService.OnAdd
    {
        add => OnAdd += value;
        remove => OnAdd -= value;
    }


    ///<inheritdoc/>
    event Action IMBSnackbarService.OnTriggerStateHasChanged
    {
        add => OnTriggerStateHasChanged += value;
        remove => OnTriggerStateHasChanged -= value;
    }


    public SnackbarService(IOptions<MBServicesOptions> options) : this(options.Value)
    {

    }


    public SnackbarService(MBServicesOptions options)
    {
        Configuration = options.SnackbarServiceConfiguration;
    }


    private void ConfigurationChanged() => OnTriggerStateHasChanged?.Invoke();


    ///<inheritdoc/>
#nullable enable annotations
    public void ShowSnackbar(
        string message,
        Action action = null,
        string actionText = null,
        string additionalClass = "",
        Material.Blazor.MD2.MBNotifierCloseMethod? closeMethod = null,
        bool leading = false,
        bool stacked = false,
        int? timeout = null,
        bool debug = false)
#nullable restore annotations
    {
#if !DEBUG
        if (debug)
        {
            return;
        }
#endif

        var settings = new MBSnackbarSettings()
        {
            Action = action,
            AdditionalClass = additionalClass,
            Message = message,
            ActionText = actionText,
            CloseMethod = closeMethod,
            Leading = leading,
            Stacked = stacked,
            Timeout = timeout
        };

        if (OnAdd is null)
        {
            throw new InvalidOperationException($"Material.Blazor: you attempted to show a snackbar notification from a {Utilities.GetTypeName(typeof(IMBSnackbarService))} but have not placed an MBAnchor component at the top of either App.razor or MainLayout.razor");
        }

        OnAdd?.Invoke(settings);
    }
}
