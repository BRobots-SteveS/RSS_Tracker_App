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
        private readonly FeedRepository _feedRepo;
        [ObservableProperty]
        private Guid? userId;
        [ObservableProperty]
        private Guid? authorId;
        [ObservableProperty]
        private ObservableCollection<FeedDto> feeds;
        public FeedListViewModel(INavigationService navigation, IDialogService dialogService, FeedRepository repo) : base(navigation, dialogService)
        { _feedRepo = repo; }

        [RelayCommand]
        public async Task ReloadData()
        {
            if (UserId != null && UserId != Guid.Empty)
                Feeds = new(await _feedRepo.GetFeedsByUserId(UserId.Value));
            else if (AuthorId != null && AuthorId != Guid.Empty)
                Feeds = new(await _feedRepo.GetFeedByCreator(AuthorId.Value));
            else
                Feeds = new(await _feedRepo.GetAllRows());
        }

        [RelayCommand]
        public async Task CreateFeed() => await Navigation.NavigateToAsync(nameof(FeedDetailPage));

        [RelayCommand]
        public async Task GoToDetails(FeedDto selectedFeed) => await Navigation.NavigateToAsync(nameof(FeedDetailPage), new Dictionary<string, object> { { "feed", selectedFeed.Id } });
    }
}
