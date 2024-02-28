using Android.App;
using Android.Content.PM;
using Android.OS;

namespace LagerApp;

//[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[Activity(Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Portrait)]
public class MainActivity : MauiAppCompatActivity
{
    //Android.Graphics.Color colorLight = Android.Graphics.Color.ParseColor("#f5f5f5");
    //Android.Graphics.Color colorNight = Android.Graphics.Color.ParseColor("#f5f5f5");
    //protected override void OnCreate(Bundle savedInstanceState)
    //{
    //    var darkMode = this.ApplicationContext.Resources.Configuration.UiMode & Android.Content.Res.UiMode.NightMask;
    //    Window.NavigationBarContrastEnforced = true;

    //    switch (darkMode)
    //    {
    //        case Android.Content.Res.UiMode.NightNo:
    //            Window.SetNavigationBarColor(colorLight);
    //            break;
    //        case Android.Content.Res.UiMode.NightUndefined:
    //            Window.SetNavigationBarColor(colorLight);
    //            break;
    //        case Android.Content.Res.UiMode.NightYes:
    //            Window.SetNavigationBarColor(colorNight);
    //            break;
    //        default:
    //            break;
    //    }
    //    base.OnCreate(savedInstanceState);
    //}

    //protected override void OnNightModeChanged(int mode)
    //{
    //    var darkMode = this.ApplicationContext.Resources.Configuration.UiMode & Android.Content.Res.UiMode.NightMask;
    //    Window.NavigationBarContrastEnforced = true;

    //    switch (darkMode)
    //    {
    //        case Android.Content.Res.UiMode.NightNo:
    //            Window.SetNavigationBarColor(colorLight);
    //            break;
    //        case Android.Content.Res.UiMode.NightUndefined:
    //            Window.SetNavigationBarColor(colorLight);
    //            break;
    //        case Android.Content.Res.UiMode.NightYes:
    //            Window.SetNavigationBarColor(colorNight);
    //            break;
    //        default:
    //            break;
    //    }
    //    base.OnNightModeChanged(mode);
    //}
}
