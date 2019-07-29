using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace Spectrum.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public MainViewModel()
        {
        }
        
        public override Task Initialize()
        {
            //TODO: Add starting logic here
		    
            return base.Initialize();
        }

        public override void Prepare()
        {
            Text = "Hellos!";

            base.Prepare();
        }

        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);
        private void ResetText()
        {
            Text = "Hello MvvmCross";
        }

        
        public string Text { get; set; }
    }
}