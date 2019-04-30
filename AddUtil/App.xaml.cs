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
        public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();
        CongratulationsViewModel congratulationsViewModel;

        public App()
        {
            displayRootRegistry.RegisterWindowType<CongratulationsViewModel, CongratulationsView>();
            displayRootRegistry.RegisterWindowType<MergeViewModel, MergeView>();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            congratulationsViewModel = new CongratulationsViewModel();

            await displayRootRegistry.ShowModalPresentation(congratulationsViewModel);

            Shutdown();
        }
    }
}
