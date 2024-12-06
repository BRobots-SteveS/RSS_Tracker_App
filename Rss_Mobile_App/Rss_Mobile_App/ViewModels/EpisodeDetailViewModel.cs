using CommunityToolkit.Mvvm.ComponentModel;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using System.Collections.ObjectModel;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    public partial class EpisodeDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<EpisodeDto> episodes;
        public EpisodeDetailViewModel(INavigationService navigation, IDialogService dialogService) : base(navigation, dialogService)
        { }
    }
}
