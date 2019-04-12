using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SeCongratulator.Models;

namespace SeCongratulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ApplicationContext db = new ApplicationContext();
            //try
            //{
            //    db.Congratulations.Load();
            //    MessageBox.Show(db.Congratulations.First().ToString());
            //}
            //catch (Exception e) { MessageBox.Show(e.Message); }
        }
    }
}
