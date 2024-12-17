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
    [QueryProperty(nameof(AuthorId), "author")]
    public partial class AuthorDetailViewModel : BaseViewModel
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IFeedRepository _feedRepo;
        [ObservableProperty]
        private Guid authorId = Guid.Empty;
        [ObservableProperty]
        private AuthorDto author = new();
        [ObservableProperty]
        private ObservableCollection<FeedDto> feeds = new();
        public AuthorDetailViewModel(IAuthorRepository authorRepo, IFeedRepository feedRepo,
            INavigationService navigation, IDialogService dialog) : base(navigation, dialog)
        { _authorRepo = authorRepo; _feedRepo = feedRepo; author = new(); feeds = new(); }

        [RelayCommand]
        public async Task ReloadData()
        {
            Author = await _authorRepo.GetRowById(AuthorId);
            Feeds = new(await _feedRepo.GetFeedByCreator(AuthorId));
        }

        [RelayCommand]
        public async Task ToAccountDetails() => await GoToAccountDetails();
        [RelayCommand]
        public async Task ToFeedList() => await GoToFeedList();
        [RelayCommand]
        public async Task ToAuthorList() => await GoToAuthorList();
        [RelayCommand]
        public async Task ToFavorites() => await GoToFavorites();

    }
}
