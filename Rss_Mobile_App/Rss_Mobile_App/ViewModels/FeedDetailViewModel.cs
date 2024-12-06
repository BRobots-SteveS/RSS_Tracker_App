﻿using CommunityToolkit.Mvvm.ComponentModel;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Mobile_App.ViewModels
{
    public partial class FeedDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        private FeedDto feed;
        public FeedDetailViewModel(INavigationService navigation) : base(navigation)
        {
        }
    }
}