using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace BeConsistent;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.SetStatusBarColor(Color.FromHex("#191919").ToAndroid());
        Window.SetNavigationBarColor(Color.FromHex("#191919").ToAndroid());

        base.OnCreate(savedInstanceState);
    }
}