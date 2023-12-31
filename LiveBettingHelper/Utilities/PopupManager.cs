using CommunityToolkit.Maui.Views;

namespace LiveBettingHelper.Utilities
{
    public class PopupManager
    {
        public void ShowPopup(Popup popup)
        {
            var currentPage = GetCurrentPage();
            currentPage.ShowPopup(popup);
        }

        private Page GetCurrentPage()
        {
            // Ellenőrizzük, hogy a MainPage egy NavigationPage-e
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                return navigationPage.CurrentPage;
            }
            // Ellenőrizzük, hogy a MainPage egy TabbedPage-e
            else if (Application.Current.MainPage is TabbedPage tabbedPage)
            {
                return tabbedPage.CurrentPage;
            }
            // Ellenőrizzük, hogy a MainPage egy FlyoutPage-e
            else if (Application.Current.MainPage is FlyoutPage flyoutPage)
            {
                if (flyoutPage.Detail is NavigationPage detailNavigationPage)
                {
                    return detailNavigationPage.CurrentPage;
                }

                return flyoutPage.Detail;
            }
            // Ellenőrizzük, hogy a MainPage egy Shell-e
            else if (Shell.Current != null)
            {
                var currentPage = (Shell.Current?.CurrentItem as IShellSectionController)?.PresentedPage;
            }
            //Alapértelmezett esetben visszaadjuk a MainPage - et
            return Application.Current.MainPage;
        }
    }
}
