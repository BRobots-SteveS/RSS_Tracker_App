using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using System.Collections.ObjectModel;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    [QueryProperty(nameof(EpisodeGuid), "episode")]
    public partial class EpisodeDetailViewModel : BaseViewModel
    {
        private readonly IEpisodeRepository _repo;
        public string EpisodeGuid
        {
            get => EpisodeId.ToString() ?? Guid.Empty.ToString();
            set => EpisodeId = Guid.TryParse(value, out var result) ? result : Guid.Empty;
        }
        [ObservableProperty]
        private Guid episodeId = Guid.Empty;
        [ObservableProperty]
        private EpisodeDto episode = new();
        public EpisodeDetailViewModel(INavigationService navigation, IDialogService dialogService, IEpisodeRepository repo) : base(navigation, dialogService)
        { _repo = repo; }

        public override async Task DoRefresh()
        {
            Episode = await _repo.GetRowById(EpisodeId);
        }

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
