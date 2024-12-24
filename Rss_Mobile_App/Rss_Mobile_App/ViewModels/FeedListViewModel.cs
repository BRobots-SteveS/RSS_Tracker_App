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
    [QueryProperty(nameof(UserId), "user")]
    [QueryProperty(nameof(AuthorId), "author")]
    public partial class FeedListViewModel : BaseViewModel
    {
        private readonly IFeedRepository _feedRepo;
        private readonly IUserRepository _userRepo;
        [ObservableProperty]
        private Guid? userId = null;
        [ObservableProperty]
        private Guid? authorId = null;
        [ObservableProperty]
        private ObservableCollection<FeedDto> feeds = new();
        public FeedListViewModel(IUserRepository userRepo, IFeedRepository repo,
            INavigationService navigation, IDialogService dialogService) : base(navigation, dialogService)
        { _feedRepo = repo; _userRepo = userRepo; }

        public override async Task DoRefresh()
        {
            if (UserId != null && UserId != Guid.Empty)
                Feeds = new(await _feedRepo.GetFeedsByUserId(UserId.Value));
            else if (AuthorId != null && AuthorId != Guid.Empty)
                Feeds = new(await _feedRepo.GetFeedByCreator(AuthorId.Value));
            else
                Feeds = new(await _feedRepo.GetAllRows());
        }

        [RelayCommand]
        public async Task AddToFavorites(FeedDto selectedFeed)
        {
            if (selectedFeed == null) return;
            await _userRepo.CreateFavorite(Guid.Parse(Preferences.Get("user", string.Empty)), feedId: selectedFeed.Id);
        }

        [RelayCommand]
        public async Task CreateFeed() => await Navigation.NavigateToAsync(nameof(FeedDetailPage));

        [RelayCommand]
        public async Task GoToDetails(FeedDto selectedFeed) => await Navigation.NavigateToAsync(nameof(FeedDetailPage), new Dictionary<string, object> { { "feed", selectedFeed.Id } });
    }
}
