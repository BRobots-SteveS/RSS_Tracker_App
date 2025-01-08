using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rss_Mobile_App.Repositories;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    [QueryProperty(nameof(AuthorGuid), "author")]
    public partial class AuthorDetailViewModel : BaseViewModel
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IFeedRepository _feedRepo;
        private readonly IUserRepository _userRepo;
        public string AuthorGuid
        {
            get => AuthorId.ToString();
            set => AuthorId = Guid.TryParse(value, out var result) ? result : Guid.Empty;
        }
        [ObservableProperty]
        private Guid authorId = Guid.Empty;
        [ObservableProperty]
        private AuthorDto author = new();
        [ObservableProperty]
        private ObservableCollection<FeedDto> feeds = new();
        public AuthorDetailViewModel(IAuthorRepository authorRepo, IFeedRepository feedRepo, IUserRepository userRepo,
            INavigationService navigation, IDialogService dialog) : base(navigation, dialog)
        { _authorRepo = authorRepo; _feedRepo = feedRepo; _userRepo = userRepo; }

        public override async Task DoRefresh()
        {
            if (AuthorId == Guid.Empty) return;
            Author = await _authorRepo.GetRowById(AuthorId);
            Feeds = new(await _feedRepo.GetFeedByCreator(AuthorId));
        }

        [RelayCommand]
        public async Task GoToFeed(FeedDto selectedItem)
        {
            await Navigation.NavigateToAsync(nameof(FeedDetailViewModel), new Dictionary<string, object> { { "feed", selectedItem.Id.ToString() } });
        }
    }
}
