using System;
using MvvmCross;
using MvvmCross.ViewModels;
using Spectrum.Services;
using Spectrum.ViewModels;

namespace Spectrum.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IRepository, Repository>();

            Akavache.Registrations.Start("Spectrum");

            RegisterAppStart<MainViewModel>();
        }
    }
}
