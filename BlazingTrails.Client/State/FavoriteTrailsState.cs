﻿using BlazingTrails.Client.Features.Home;
using Blazored.LocalStorage;

namespace BlazingTrails.Client.State;

public class FavoriteTrailsState
{
    private const string FavoriteTrailsKay = "favoriteTrails";
    private bool _isInitialized;
    private List<Trail> _favoriteTrails = new();
    private readonly ILocalStorageService _localStorageService;
    public IReadOnlyList<Trail> FavoriteTrails => _favoriteTrails.AsReadOnly();
    public event Action? OnChange;

    public FavoriteTrailsState(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task Initialize()
    {
        if (!_isInitialized)
        {
            _favoriteTrails = await _localStorageService.GetItemAsync<List<Trail>>(FavoriteTrailsKay) ?? new List<Trail>();
            _isInitialized = true;
            NotifyStateHasChanged();
        }
    }

    public async Task AddFavorite(Trail trail)
    {
        if (_favoriteTrails.Any(_ => _.Id == trail.Id)) return;
        _favoriteTrails.Add(trail);
        await _localStorageService.SetItemAsync(FavoriteTrailsKay, _favoriteTrails);
        NotifyStateHasChanged();
    }

    public async Task RemoveFavorite(Trail trail)
    {
        var existingTrail = _favoriteTrails.SingleOrDefault(_=>_.Id == trail.Id);
        if(existingTrail is null) return;
        _favoriteTrails.Remove(existingTrail);
        await _localStorageService.SetItemAsync(FavoriteTrailsKay, _favoriteTrails);
        NotifyStateHasChanged();
    }

    public bool IsFavorite(Trail trail) => _favoriteTrails.Any(_ => _.Id == trail.Id);
    private void NotifyStateHasChanged() => OnChange?.Invoke();
}
