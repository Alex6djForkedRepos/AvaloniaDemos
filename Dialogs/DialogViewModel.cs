﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dialogs.Abstractions;

namespace Dialogs;
public abstract partial class DialogViewModel<TResult> : ObservableValidator, IRequestMediator<TResult>
{
    [ObservableProperty] protected TResult? _requestResult = default(TResult);
    [ObservableProperty] private string _title = "";

    private RelayCommand? _acceptCommand;
    public IRelayCommand AcceptCommand => _acceptCommand ??= new RelayCommand(Accept, CanAccept);

    private RelayCommand? _cancelCommand;
    public IRelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);

    public string AcceptName { get; set; } = "Ok";
    public string CancelName { get; set; } = "Cancel";

    /// <summary>
    /// Called when the user accepts the interaction
    /// Responsible for mapping an internal result into RequestResult
    /// </summary>
    protected abstract void Accept();

    protected virtual bool CanAccept() => true;

    /// <summary>
    /// Called when the user cancels an interaction
    /// </summary>
    protected virtual void Cancel()
    {
        _requestResult = default;
        OnPropertyChanged(nameof(RequestResult));
    }
}
