using CommunityToolkit.Mvvm.ComponentModel;
using Rss_Mobile_App.Services;

namespace Rss_Mobile_App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public readonly INavigationService Navigation;
        public BaseViewModel(INavigationService navigation)
        {
            Navigation = navigation;
        }
    }
}
