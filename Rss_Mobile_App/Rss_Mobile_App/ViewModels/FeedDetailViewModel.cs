using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Mobile_App.Views;
using Rss_Tracking_App.Models.Dto;
using System.Collections.ObjectModel;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    [QueryProperty(nameof(FeedGuid), "feed")]
    public partial class FeedDetailViewModel : BaseViewModel
    {
        private readonly IFeedRepository _feedRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IEpisodeRepository _episodeRepo;
        private readonly IUserRepository _userRepo;
        public string FeedGuid
        {
            get => FeedId.ToString();
            set => FeedId = Guid.TryParse(value, out var result) ? result : Guid.Empty;
        }
        [ObservableProperty]
        private Guid feedId = Guid.Empty;
        [ObservableProperty]
        private FeedDto feed = new();
        [ObservableProperty]
        private ObservableCollection<EpisodeDto> episodes = new();
        [ObservableProperty]
        private ObservableCollection<AuthorDto> authors = new();
        [ObservableProperty]
        private ObservableCollection<string> platforms = new(["Youtube", "Omny", "Media", "iTunes", "Soundcloud"]);
        public FeedDetailViewModel(IFeedRepository feedRepo, IAuthorRepository authorRepo, IEpisodeRepository episodeRepo, IUserRepository userRepo,
            INavigationService navigation, IDialogService dialogService) : base(navigation, dialogService)
        {
            _feedRepo = feedRepo; _authorRepo = authorRepo; _episodeRepo = episodeRepo; _userRepo = userRepo;
        }

        public override async Task DoRefresh()
        {
            if (FeedId != Guid.Empty)
            {
                Feed = await _feedRepo.GetRowById(FeedId);
                Episodes = new(await _episodeRepo.GetEpisodesByFeedId(FeedId));
                Authors = new(await _authorRepo.GetAuthorsByFeedId(FeedId));
            }
        }

        [RelayCommand]
        public async Task UpdateFeed()
        {
            await _feedRepo.UpdateFeed(Feed.Id);
            await DoRefresh();
        }

        [RelayCommand]
        public async Task CreateFeed()
        {
            if (FeedId != Guid.Empty) { await DialogService.ConfirmAsync("No permission", "Feed already exists"); return; }
            if (string.IsNullOrWhiteSpace(Feed.Platform)) { await DialogService.ConfirmAsync("Error", "No platform selected"); return; }
            if (string.IsNullOrWhiteSpace(Feed.FeedUri)) { await DialogService.ConfirmAsync("Error", "No Feed URL present"); return; }
            if (string.IsNullOrEmpty(Feed.AuthorName)) Feed.AuthorName = string.Empty;
            await _feedRepo.CreateRow(Feed);
        }

        [RelayCommand]
        public async Task GoToAuthor(AuthorDto selectedAuthor) => await Navigation.NavigateToAsync(nameof(AuthorDetailPage), new Dictionary<string, object> { { "author", selectedAuthor.Id.ToString() } });

        [RelayCommand]
        public async Task GoToEpisode(EpisodeDto selectedEpisode) => await Navigation.NavigateToAsync(nameof(EpisodeDetailPage), new Dictionary<string, object> { { "episode", selectedEpisode.Id.ToString() } });

        [RelayCommand]
        public async Task AddToFavorites()
        {
            await _userRepo.CreateFavorite(Guid.Parse(Preferences.Get("user", string.Empty)), feedId: FeedId);
        }

        [RelayCommand]
        public async Task GoToFeed()
        {
            if (FeedId != Guid.Empty) await Navigation.OpenBrowser(Feed.FeedUri);
        }
    }
}
