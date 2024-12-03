﻿using CommunityToolkit.Mvvm.ComponentModel;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Mobile_App.ViewModels
{
    public partial class EpisodeListViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<EpisodeDto> episodes;
        public EpisodeListViewModel(INavigationService navigation) : base(navigation)
        {
        }
    }
}
