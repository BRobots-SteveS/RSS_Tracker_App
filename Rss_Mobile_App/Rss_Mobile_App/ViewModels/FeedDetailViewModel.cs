using CommunityToolkit.Mvvm.ComponentModel;
using Rss_Mobile_App.Services;
using Rss_Tracking_App.Models.Dto;
using UraniumUI.Dialogs;

namespace Rss_Mobile_App.ViewModels
{
    public partial class FeedDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        private FeedDto feed;
        public FeedDetailViewModel(INavigationService navigation, IDialogService dialogService) : base(navigation, dialogService)
        { }
    }
}
