using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using System.Collections.ObjectModel;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    [QueryProperty(nameof(EpisodeId), "episode")]
    public partial class EpisodeDetailViewModel : BaseViewModel
    {
        private readonly EpisodeRepository _repo;
        [ObservableProperty]
        private Guid episodeId;
        [ObservableProperty]
        private EpisodeDto episode;
        public EpisodeDetailViewModel(INavigationService navigation, IDialogService dialogService, EpisodeRepository repo) : base(navigation, dialogService)
        { _repo = repo; episode = _repo.GetRowById(episodeId).GetAwaiter().GetResult(); }

        [RelayCommand]
        public async Task ReloadData() => Episode = await _repo.GetRowById(EpisodeId);

        [RelayCommand]
        public async Task OpenInBrowser()
        {
            if (string.IsNullOrWhiteSpace(Episode.PreviewUrl))
                await DialogService.DisplayTextPromptAsync(Episode.EpisodeName, "No preview found");
            else
                await Browser.Default.OpenAsync(Episode.PreviewUrl);
        }
    }
}
