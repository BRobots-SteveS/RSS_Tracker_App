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
    [QueryProperty(nameof(UserGuid), "user")]
    [QueryProperty(nameof(AuthorGuid), "author")]
    public partial class FeedListViewModel : BaseViewModel
    {
        private readonly IFeedRepository _feedRepo;
        private readonly IUserRepository _userRepo;
        public string UserGuid
        {
            get => UserId.ToString();
            set => UserId = Guid.TryParse(value, out var result) ? result : Guid.Empty;
        }
        public string AuthorGuid
        {
            get => AuthorId.ToString();
            set => AuthorId = Guid.TryParse(value, out var result) ? result : Guid.Empty;
        }
        [ObservableProperty]
        private Guid userId = Guid.Empty;
        [ObservableProperty]
        private Guid authorId = Guid.Empty;
        [ObservableProperty]
        private ObservableCollection<FeedDto> feeds = new();

        [ObservableProperty]
        private ObservableCollection<string> platforms = new(["Youtube", "Omny", "Media", "iTunes", "Soundcloud", "Basic"]);

        [ObservableProperty]
        private string title = string.Empty;
        [ObservableProperty]
        private string description = string.Empty;
        [ObservableProperty]
        private string platform = string.Empty;
        [ObservableProperty]
        private string authorName = string.Empty;
        [ObservableProperty]
        private string creatorId = string.Empty;
        public FeedListViewModel(IUserRepository userRepo, IFeedRepository repo,
            INavigationService navigation, IDialogService dialogService) : base(navigation, dialogService)
        {
            _feedRepo = repo;
            _userRepo = userRepo;
        }

        public override async Task DoRefresh()
        {
            if (!string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Description) || !string.IsNullOrEmpty(Platform) || !string.IsNullOrWhiteSpace(AuthorName) || !string.IsNullOrEmpty(CreatorId))
                Feeds = new(await _feedRepo.GetFeedsByFilter(Title, CreatorId, Description, AuthorName, Platform));
            else if (UserId != Guid.Empty)
            {
                var tempFavs = await _userRepo.GetFavoritesByUserId(UserId);
                var tempIds = tempFavs.Select(f => f.Feed.Id).ToList();
                Feeds = new(await _feedRepo.GetFeedsByIds(tempIds));
            }
            else if (AuthorId != Guid.Empty)
                Feeds = new(await _feedRepo.GetFeedByCreator(AuthorId));
            else
                Feeds = new(await _feedRepo.GetAllRows());
            ClearFilters();
        }

        [RelayCommand]
        public async Task AddToFavorites(FeedDto selectedFeed)
        {
            if (selectedFeed == null) return;
            await _userRepo.CreateFavorite(Guid.Parse(Preferences.Get("user", string.Empty)), feedId: selectedFeed.Id);
        }

        [RelayCommand]
        public async Task CreateFeed() => await Navigation.NavigateToAsync(nameof(FeedDetailPage), new Dictionary<string, object>() { { "feed", Guid.Empty.ToString() } });

        [RelayCommand]
        public async Task GoToDetails(FeedDto selectedFeed) => await Navigation.NavigateToAsync(nameof(FeedDetailPage), new Dictionary<string, object> { { "feed", selectedFeed.Id.ToString() } });
        [RelayCommand]
        public async Task DoFilter()
        {
            Feeds = new(await _feedRepo.GetFeedsByFilter(Title, CreatorId, Description, AuthorName, Platform));
        }

        [RelayCommand]
        public void SelectedPlatformChanged(string selectedItem)
        {
            Platform = selectedItem;
        }
        [RelayCommand]
        public void ClearFilters()
        {
            Title = string.Empty;
            Description = string.Empty;
            Platform = string.Empty;
            AuthorName = string.Empty;
            CreatorId = string.Empty;
        }
    }
}
