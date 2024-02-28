using CommunityToolkit.Maui.Core.Platform;

namespace LagerApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
#if ANDROID || IOS || WINDOWS
        if (Application.Current.RequestedTheme == AppTheme.Light)
        {
            statusBar.StatusBarColor = Color.FromHex("#2E7D32");
        }
        else
        {
            statusBar.StatusBarColor = Color.FromHex("#2E7D32");
        }



        Application.Current.RequestedThemeChanged += (s, a) =>
        {
            if (a.RequestedTheme == AppTheme.Light)
            {
                statusBar.StatusBarColor = Color.FromHex("#2E7D32");
            }
            else
            {
                statusBar.StatusBarColor = Color.FromHex("#2E7D32");
            }
        };


#endif
    }
}
