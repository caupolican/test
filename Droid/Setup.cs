using Android.Content;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.ViewModels;

namespace Spectrum.Droid
{
    public class Setup : MvxAndroidSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new Spectrum.Core.App();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
        }
    }
}
