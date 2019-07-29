using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;

namespace Spectrum.Droid.Views
{
    [Activity(Label = "Create User")]
    public class CreateUserView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CreateUserView);
        }
    }
}
