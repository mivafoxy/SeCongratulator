using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SeCongratulator
{
    public static class ProfileConsts
    {
        public static string nameProfile = string.Empty;
        public static Dictionary<string, Brush> GetHolidayNamesToBrushes()
        {
            var holidayNamesToBrushes = new Dictionary<string, Brush>();

            holidayNamesToBrushes.Add("День Рождение", new SolidColorBrush(Color.FromArgb(255, 254, 241, 188)));
            holidayNamesToBrushes.Add("Новый Год", new SolidColorBrush(Color.FromArgb(255, 211, 249, 255)));
            holidayNamesToBrushes.Add("8 марта", new SolidColorBrush(Color.FromArgb(255, 240, 60, 60)));
            holidayNamesToBrushes.Add("23 февраля", new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));
            holidayNamesToBrushes.Add("14 февраля", new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));
            holidayNamesToBrushes.Add("1 сентября", new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));
            holidayNamesToBrushes.Add("1 мая", new SolidColorBrush(Color.FromArgb(255, 0, 141, 255)));
            holidayNamesToBrushes.Add("День Народного Единства", new SolidColorBrush(Color.FromArgb(255, 0, 3, 149)));
            holidayNamesToBrushes.Add("День Победы", Brushes.White);
            holidayNamesToBrushes.Add("Рождество", Brushes.White);
            holidayNamesToBrushes.Add("Крещение", Brushes.White);
            holidayNamesToBrushes.Add("День Учителя", Brushes.White);
            holidayNamesToBrushes.Add("День Семьи, Любви и Верности", Brushes.White);

            return holidayNamesToBrushes;
        }
    }
}
