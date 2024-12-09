﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    [QueryProperty(nameof(authorId), "author")]
    public partial class AuthorDetailViewModel : BaseViewModel
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IFeedRepository _feedRepo;
        [ObservableProperty]
        private Guid authorId;
        [ObservableProperty]
        private AuthorDto author;
        [ObservableProperty]
        private ObservableCollection<FeedDto> feeds;
        public AuthorDetailViewModel(IAuthorRepository authorRepo, IFeedRepository feedRepo,
            INavigationService navigation, IDialogService dialog) : base(navigation, dialog)
        { _authorRepo = authorRepo; _feedRepo = feedRepo; ReloadData().GetAwaiter().GetResult(); }

        [RelayCommand]
        public async Task ReloadData()
        {
            Author = await _authorRepo.GetRowById(AuthorId);
            Feeds = new(await _feedRepo.GetFeedByCreator(AuthorId));
        }
    }
}
