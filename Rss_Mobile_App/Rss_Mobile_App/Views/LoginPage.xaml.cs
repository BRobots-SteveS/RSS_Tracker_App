using Rss_Mobile_App.ViewModels;

namespace Rss_Mobile_App.Views;

public partial class LoginPage : UraniumUI.Pages.UraniumContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}
