namespace Rss_Mobile_App.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null);
        Task PopAsync();
        Task OpenBrowser(string route);
    }

    public class MauiNavigationService : INavigationService
    {
        public MauiNavigationService() { }

        //public Task InitializeAsync() =>
        //    NavigateToAsync("PieOverviewView");

        public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
        {
            var shellNavigation = new ShellNavigationState(route);

            return routeParameters != null
                ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
                : Shell.Current.GoToAsync(shellNavigation);
        }

        public Task PopAsync() => Shell.Current.GoToAsync("..");

        public async Task OpenBrowser(string route) => await Browser.Default.OpenAsync(route, BrowserLaunchMode.SystemPreferred);
    }
}
