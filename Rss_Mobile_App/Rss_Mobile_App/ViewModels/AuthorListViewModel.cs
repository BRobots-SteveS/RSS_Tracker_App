﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        public AuthorListViewModel(IAuthorRepository repo, IUserRepository userRepo,
            INavigationService navigation, IDialogService dialogService) : base(navigation, dialogService)
        { _repo = repo; _userRepo = userRepo; }

        [RelayCommand]
        public async Task ReloadData()
        {
            Console.WriteLine("Started fetching all authors.");
            Authors = new ObservableCollection<AuthorDto>(await _repo.GetAllRows());
            Console.WriteLine($"Fetched all records: {Authors.Count}");
        }

        [RelayCommand]
        public async Task GoToFeeds(AuthorDto selectedAuthor)
        {
            Dictionary<string, object> authorMap = new();
            if (selectedAuthor != null) authorMap.Add("author", selectedAuthor.Id);
            await Navigation.NavigateToAsync(nameof(FeedListPage), authorMap);
        }

        [RelayCommand]
        public async Task AddToFavorites(AuthorDto selectedAuthor)
        {
            if (selectedAuthor == null) return;
            await _userRepo.CreateFavorite(Guid.Parse(Preferences.Get("user", string.Empty)), authorId: selectedAuthor.Id);
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
