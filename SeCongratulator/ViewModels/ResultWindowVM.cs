using SeCongratulator.Helper_classes;
using SeCongratulator.Models;
using SeCongratulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SeCongratulator.ViewModels
{
    class ResultWindowVM : ModelBase
    {
        Result result = new Result();
        Congratulation profile = new Congratulation();
        string text = string.Empty;

        public ResultWindowVM()
        {

        }
        public ResultWindowVM(Result result, Congratulation profile)
        {
            Result = result;
            Profile = profile;
            if (Result.Cliche == string.Empty && Result.Poem != string.Empty) Text = Result.Poem;
            else Text = Result.Cliche + '\n'+ '\n' + Result.Poem;
        }

        public ICommand ClickReturn
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    СonstructionWindow window = new СonstructionWindow();
                    window.DataContext = new СonstructionWindowVM(Profile);
                    window.Show();
                    Application.Current.Windows[0].Close();
                }));
            }
        }

        public string Text { get => text; set => SetField(ref text, value); }
        public Result Result { get => result; set => SetField(ref result, value); }
        public Congratulation Profile { get => profile; set => SetField(ref profile, value); }
    }
}
