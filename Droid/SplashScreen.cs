using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;

namespace Spectrum.Droid
{
    [Activity(
        Label = "Spectrum.Droid"
        , MainLauncher = true
        , Icon = "@mipmap/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
