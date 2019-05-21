using AddUtil.ViewModels;
using AddUtil.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AddUtil
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DisplayRootRegistry DisplayRootRegistry = new DisplayRootRegistry();
        CongratulationsViewModel congratulationsViewModel;

        public App()
        {
            DisplayRootRegistry.RegisterWindowType<CongratulationsViewModel, CongratulationsView>();
            DisplayRootRegistry.RegisterWindowType<MergeViewModel, MergeView>();
            DisplayRootRegistry.RegisterWindowType<NewCongratulationViewModel, NewCongratulationView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            congratulationsViewModel = new CongratulationsViewModel();

            await DisplayRootRegistry.ShowModalPresentation(congratulationsViewModel);

            Shutdown();
        }
    }
}
