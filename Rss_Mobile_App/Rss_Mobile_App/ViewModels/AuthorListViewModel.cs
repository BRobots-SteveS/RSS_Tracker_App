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
    public partial class AuthorListViewModel : BaseViewModel
    {
        private readonly IAuthorRepository _repo;
        private readonly IUserRepository _userRepo;

        [ObservableProperty]
        private ObservableCollection<AuthorDto> authors = new();
        [ObservableProperty]
        private string authorName = string.Empty;
        [ObservableProperty]
        private string authorEmail = string.Empty;
        public AuthorListViewModel(IAuthorRepository repo, IUserRepository userRepo,
            INavigationService navigation, IDialogService dialogService) : base(navigation, dialogService)
        { _repo = repo; _userRepo = userRepo; }

        public override async Task DoRefresh()
        {
            if (!string.IsNullOrEmpty(AuthorName) || !string.IsNullOrEmpty(AuthorEmail))
                Authors = new(await _repo.GetAuthorsByNameAndEmail(AuthorName, AuthorEmail));
            else
                Authors = new ObservableCollection<AuthorDto>(await _repo.GetAllRows());
        }

        [RelayCommand]
        public async Task GoToFeeds(AuthorDto selectedAuthor)
        {
            Dictionary<string, object> authorMap = new();
            if (selectedAuthor != null) authorMap.Add("author", selectedAuthor.Id);
            await Navigation.NavigateToAsync(nameof(FeedListPage), authorMap);
        }

        [RelayCommand]
        public async Task GoToDetails(AuthorDto selectedAuthor)
        {
            Dictionary<string, object> authorMap = new();
            if (selectedAuthor != null) authorMap.Add("author", selectedAuthor.Id.ToString());
            await Navigation.NavigateToAsync(nameof(AuthorDetailPage), authorMap);
        }


        [RelayCommand]
        public async Task AddToFavorites(AuthorDto selectedAuthor)
        {
            if (selectedAuthor == null) return;
            await _userRepo.CreateFavorite(Guid.Parse(Preferences.Get("user", string.Empty)), authorId: selectedAuthor.Id);
        }

        [RelayCommand]
        public async Task FilterResults()
        {
            Authors = new(await _repo.GetAuthorsByNameAndEmail(AuthorName, AuthorEmail));
        }
    }
}
