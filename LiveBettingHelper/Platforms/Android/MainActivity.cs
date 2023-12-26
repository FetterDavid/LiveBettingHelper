using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace LiveBettingHelper;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // Check and request notification permission
#pragma warning disable CA1416 // Disable platform compatibility warning
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Tiramisu is Android 33
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.PostNotifications }, 0);
            }
        }
#pragma warning restore CA1416 // Restore platform compatibility warning
    }
}
