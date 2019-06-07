using SeCongratulator.Helper_classes;
using SeCongratulator.Models;
using SeCongratulator.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SeCongratulator.ViewModels
{
    class СonstructionWindowVM : Models.ModelBase
    {
        Congratulation profile = new Congratulation();

        string pathBackgroundTheme = "..\\Images\\Themes\\0.jpg";
        string pathPanelTheme = "..\\Images\\Themes\\0.jpg";
        string pathRefreshImage = "..\\Images\\Items\\refresh.png";

        ObservableCollection<string> pathCongratulationImage = new ObservableCollection<string>();
        ObservableCollection<bool> isChooseImage = new ObservableCollection<bool>() { false, false, false, false };

        bool isAddImage = false;

        bool isAddPoem = false;
        bool isAddСliche = false;
        bool isNotEmptyPoem = true;
        bool isNotEmptyCliche = true;

        string poemText = string.Empty;
        string clicheText = string.Empty;
        int counterPoem = 0;
        int counterCliche = 0;

        Brush colorHeader = Brushes.Black;
        Dictionary<int,string> listImagesCongratulation = new Dictionary<int, string>();
        Dictionary<int, string> listPoemsCongratulation = new Dictionary<int, string>();
        Dictionary<int, string> listClicheCongratulation = new Dictionary<int, string>();

        public СonstructionWindowVM()
        {
            Profile = new Congratulation();
            Profile.Age = "34";
            Profile.Holiday = "Новый Год";
            Profile.Sex = 1;
            listImagesCongratulation = getCongratulationImagesFromFolder(Profile);
            for (int i = 0; i < 4; i++)
            {
                PathCongratulationImage.Add(listImagesCongratulation[i]);
            }
            ColorHeader = ProfileConsts.GetHolidayNamesToBrushes().FirstOrDefault(htob => htob.Key.Equals(profile.Holiday)).Value;
            PathBackgroundTheme = "..\\Images\\Themes\\" + Profile.Holiday + ".jpg";
            PathPanelTheme = "..\\Images\\Themes\\" + Profile.Holiday + ".png";
        }
        public СonstructionWindowVM(Congratulation profile)
        {
            Profile = profile;
            ApplicationContext db = new ApplicationContext();
            try
            {
                db.Congratulations.Load();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(1);
            }
            int counter = 0;
            foreach(var el in db.Congratulations.Where(x => x.Kind== "Клише" && (x.Holiday==profile.Holiday|| (x.Holiday == string.Empty && x.Interest == profile.Interest)) && (x.Sex==profile.Sex || x.Sex == 2)))
            {
                if (checkAge(profile.Age, el.Age))
                {
                    listClicheCongratulation.Add(counter, el.Content);
                    counter++;
                }
            }
            counter = 0;
            foreach (var el in db.Congratulations.Where(x => x.Kind == "Поэма" && (x.Holiday == profile.Holiday|| (x.Holiday == string.Empty && x.Interest==profile.Interest)) && (x.Sex == profile.Sex || x.Sex == 2)))
            {
                if (checkAge(profile.Age, el.Age))
                {
                    listPoemsCongratulation.Add(counter, el.Content.Replace('/','\n'));
                    counter++;
                }
            }
            if (listClicheCongratulation.Count == 0)
            {
                MessageBox.Show("По данному запросу поздравления в форме клише не удалось найти!");
                IsNotEmptyCliche = false;
            }
            if (listPoemsCongratulation.Count == 0)
            {
                MessageBox.Show("По данному запросу поздравления в стихотворной форме не удалось найти!");
                IsNotEmptyPoem = false;
            }
            listImagesCongratulation = getCongratulationImagesFromFolder(profile);
            if (listImagesCongratulation.Count != 0 && listImagesCongratulation.Count >= 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    PathCongratulationImage.Add(listImagesCongratulation[i]);
                }
            }
            if (listImagesCongratulation.Count != 0 && listImagesCongratulation.Count < 3)
            {
                for (int i = 0; i < listImagesCongratulation.Count; i++)
                {
                    PathCongratulationImage.Add(listImagesCongratulation[i]);
                }
            }
            if (listClicheCongratulation.Count != 0) ClicheText = listClicheCongratulation[0];
            if (listPoemsCongratulation.Count != 0) PoemText = listPoemsCongratulation[0]; 
            ColorHeader = ProfileConsts.GetHolidayNamesToBrushes().FirstOrDefault(htob => htob.Key.Equals(profile.Holiday)).Value;
            PathBackgroundTheme = "..\\Images\\Themes\\" + profile.Holiday + ".jpg";
            PathPanelTheme = "..\\Images\\Themes\\" + profile.Holiday + ".png";
        }

        private bool checkAge(string age, string dbAge)
        {
            age = age.Replace('+', ' ');
            if (dbAge.Contains('-'))
            {
                string[] numbers = dbAge.Split('-');
                if (Convert.ToInt32(numbers[0]) <= Convert.ToInt32(age) && Convert.ToInt32(age) <= Convert.ToInt32(numbers[1]))
                {
                    return true;
                }
            }
            if (dbAge==string.Empty)
            {
                return true;
            }
            return false;
        }

        public ICommand RefreshImage
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    try
                    {
                        if (PathCongratulationImage.Count > 3)
                        {
                            int checkKey = listImagesCongratulation.Where(x => x.Value.Equals(PathCongratulationImage.Last())).First().Key;
                            for (int i = 0; i < PathCongratulationImage.Count; i++)
                            {
                                if (listImagesCongratulation.ContainsKey(checkKey + 1)) checkKey++;
                                else checkKey = 0;
                                PathCongratulationImage[i] = listImagesCongratulation[checkKey];
                            }
                        }
                    }catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        for(int i = 0; i < PathCongratulationImage.Count; i++)
                        {
                            PathCongratulationImage[i]= "/SeCongratulator;component/Images/Items/NoPhoto.jpg";
                        }
                    }
                }));
            }
        }

        public ICommand RefreshPoem
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    if (listPoemsCongratulation.ContainsKey(counterPoem + 1)) counterPoem++;
                    else counterPoem = 0;
                    PoemText = ProfileConsts.nameProfile + ", " + listPoemsCongratulation[counterPoem];
                }));
            }
        }

        public ICommand RefreshCliche
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    if (listClicheCongratulation.ContainsKey(counterCliche + 1)) counterCliche++;
                    else counterCliche = 0;
                    ClicheText = ProfileConsts.nameProfile + ", " + listClicheCongratulation[counterCliche];
                }));
            }
        }

        public ICommand ClickAddImageToCongratulation
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    if (!IsAddImage)
                    {
                        for (int i = 0; i < IsChooseImage.Count; i++)
                        {
                            IsChooseImage[i] = false;
                        }
                    }
                }));
            }
        }

        public ICommand ClickAddPoemToCongratulation
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                }));
            }
        }

        public ICommand ClickAddClicheToCongratulation
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                }));
            }
        }
        public ICommand ClickExit
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    Environment.Exit(1);
                }));
            }
        }

        public ICommand ClickAccept
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    Result result = new Result();
                    if (IsAddImage)
                    {
                        for (int i = 0; i < IsChooseImage.Count; i++)
                        {
                            if (IsChooseImage[i])
                            {
                                result.Img = PathCongratulationImage[i];
                                break;
                            }
                        }
                    }
                    else result.Img = string.Empty;
                    if (IsAddPoem) result.Poem = PoemText;
                    if (IsAddСliche) result.Cliche = ClicheText;
                    if (IsAddPoem || IsAddСliche || (IsAddImage && IsChooseImage.Where(x => x==true).FirstOrDefault())) 
                    {
                        ResultView window = new ResultView();
                        window.DataContext = new ResultWindowVM(result, Profile);
                        window.Show();
                        Application.Current.Windows[0].Close();
                    }
                    else MessageBox.Show("Выберите хотя бы один вид поздравления!");
                }));
            }
        }

        public ICommand ClickReturn
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    ProfileWindow window = new ProfileWindow();
                    window.DataContext = new ProfileWindowVM(Profile);
                    window.Show();
                    Application.Current.Windows[0].Close();
                }));
            }
        }

        private string chooseDirectoryCongratilation(string path, string age, int sex)
        {
            string result = "0";
            if (Directory.GetDirectories(path).Length != 0)
            {
                string[] ages = Directory.GetDirectories(path);
                age = age.Replace('+', ' ');
                for (int i = 0; i < ages.Length; i++)
                {
                    ages[i] = ages[i].Split('\\').Last();
                    if (ages[i].Contains('-'))
                    {
                        string[] numbers = ages[i].Split('-');
                        if (Convert.ToInt32(numbers[0]) <= Convert.ToInt32(age) && Convert.ToInt32(age) <= Convert.ToInt32(numbers[1]))
                        {
                            result = ages[i];
                            break;
                        }
                    }
                    if (ages[i].Contains("55+"))
                    {
                        result = ages[i];
                        break;
                    }
                    if (ages[i].Contains("М") || ages[i].Contains("Ж"))
                    {
                        if (sex == 1)
                        {
                            result = "M";
                        }
                        else
                        {
                            result = "Ж";
                        }
                        break;
                    }
                }
                path = chooseDirectoryCongratilation(path + result + '\\', age, sex);
            }
            return path;
        }



        private Dictionary<int, string> getCongratulationImagesFromFolder(Congratulation profile)
        {
            Dictionary<int, string> listImiges = new Dictionary<int, string>();
            int i = 0;
            foreach (var el in Directory.GetFiles(chooseDirectoryCongratilation("..\\..\\..\\Images\\Holidays\\" + profile.Holiday + "\\", profile.Age, profile.Sex)))
            {
                var t = "/SeCongratulator;component/" + el.Substring(el.LastIndexOf("Images")).Replace('\\', '/');
                listImiges.Add(i,t);
                i++;
            }
            return listImiges;

        }
        public string PathBackgroundTheme { get => pathBackgroundTheme; set => SetField(ref pathBackgroundTheme, value); }
        public string PathRefreshImage { get => pathRefreshImage; set => SetField(ref pathRefreshImage, value); }
        public Brush ColorHeader { get => colorHeader; set => SetField(ref colorHeader, value); }
        public bool IsAddImage { get => isAddImage; set => SetField(ref isAddImage, value); }
        public string PathPanelTheme { get => pathPanelTheme; set => SetField(ref pathPanelTheme, value); }
        public bool IsAddPoem { get => isAddPoem; set => SetField(ref isAddPoem, value); }
        public bool IsAddСliche { get => isAddСliche; set => SetField(ref isAddСliche, value); }
        public string PoemText { get => poemText; set => SetField(ref poemText, value); }
        public string ClicheText { get => clicheText; set => SetField(ref clicheText, value); }
        internal Congratulation Profile { get => profile; set => SetField(ref profile, value); }
        public bool IsNotEmptyPoem { get => isNotEmptyPoem; set => SetField(ref isNotEmptyPoem, value); }
        public bool IsNotEmptyCliche { get => isNotEmptyCliche; set => SetField(ref isNotEmptyCliche, value); }
        public ObservableCollection<string> PathCongratulationImage { get => pathCongratulationImage; set => SetField(ref pathCongratulationImage, value); }
        public ObservableCollection<bool> IsChooseImage { get => isChooseImage; set => SetField(ref isChooseImage, value); }
    }
}
