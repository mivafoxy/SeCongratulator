using SeCongratulator.Helper_classes;
using SeCongratulator.Models;
using SeCongratulator.Views;
using System;
using System.Collections.Generic;
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

        string pathCongratulationImage_1 = " ";
        string pathCongratulationImage_2 = " ";
        string pathCongratulationImage_3 = " ";
        string pathCongratulationImage_4 = " ";

        bool isAddImage = false;

        bool isChooseImage_1 = false;
        bool isChooseImage_2 = false;
        bool isChooseImage_3 = false;
        bool isChooseImage_4 = false;

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
            if (listImagesCongratulation.Count != 0 && listImagesCongratulation.Count > 3)
            {
                PathCongratulationImage_1 = listImagesCongratulation[0];
                PathCongratulationImage_2 = listImagesCongratulation[1];
                PathCongratulationImage_3 = listImagesCongratulation[2];
                PathCongratulationImage_4 = listImagesCongratulation[3];

            }
            if (listImagesCongratulation.Count != 0 && listImagesCongratulation.Count < 3)
            {
                for (int i = 0; i < listImagesCongratulation.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            PathCongratulationImage_1 = listImagesCongratulation[0];
                            break;
                        case 1:
                            PathCongratulationImage_2 = listImagesCongratulation[1];
                            break;
                        case 2:
                            PathCongratulationImage_3 = listImagesCongratulation[2];
                            break;
                        case 3:
                            PathCongratulationImage_4 = listImagesCongratulation[3];
                            break;
                    } 
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
                        if (!(pathCongratulationImage_1.Contains(" ") ||
                        pathCongratulationImage_2.Contains(" ")) ||
                        pathCongratulationImage_3.Contains(" ") ||
                        pathCongratulationImage_4.Contains(" "))
                        {
                            int checkKey = listImagesCongratulation.Where(x => x.Value.Equals(PathCongratulationImage_4)).First().Key;
                            for (int i = 0; i < 4; i++)
                            {
                                if (listImagesCongratulation.ContainsKey(checkKey + 1))
                                {
                                    checkKey++;
                                }
                                else
                                {
                                    checkKey = 0;
                                }
                                switch (i)
                                {
                                    case 0:
                                        PathCongratulationImage_1 = listImagesCongratulation[checkKey];
                                        break;
                                    case 1:
                                        PathCongratulationImage_2 = listImagesCongratulation[checkKey];
                                        break;
                                    case 2:
                                        PathCongratulationImage_3 = listImagesCongratulation[checkKey];
                                        break;
                                    case 3:
                                        PathCongratulationImage_4 = listImagesCongratulation[checkKey];
                                        break;
                                }
                            }
                        }
                    }catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        PathCongratulationImage_1 = "/SeCongratulator;component/Images/Items/NoPhoto.jpg";
                        PathCongratulationImage_2 = "/SeCongratulator;component/Images/Items/NoPhoto.jpg";
                        PathCongratulationImage_3 = "/SeCongratulator;component/Images/Items/NoPhoto.jpg";
                        PathCongratulationImage_4 = "/SeCongratulator;component/Images/Items/NoPhoto.jpg";
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
                        IsChooseImage_1 = false;
                        IsChooseImage_1 = false;
                        IsChooseImage_1 = false;
                        IsChooseImage_1 = false;
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
                    /*if (!IsAddPoem)
                    {
                        PoemText = string.Empty;
                    }*/
                }));
            }
        }

        public ICommand ClickAddClicheToCongratulation
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    /*if (!IsAddСliche)
                    {
                        ClicheText = string.Empty;
                    }*/
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
                        if (IsChooseImage_1 == true) result.Img = PathCongratulationImage_1;
                        if (IsChooseImage_2 == true) result.Img = PathCongratulationImage_2;
                        if (IsChooseImage_3 == true) result.Img = PathCongratulationImage_3;
                        if (IsChooseImage_4 == true) result.Img = PathCongratulationImage_4;
                    }
                    else result.Img = string.Empty;
                    if (IsAddPoem) result.Poem = PoemText;
                    if (IsAddСliche) result.Cliche = ClicheText;
                    if (IsAddPoem || IsAddСliche || (IsAddImage && (IsChooseImage_1 || IsChooseImage_2 || IsChooseImage_3 || IsChooseImage_4))) 
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
        public string PathCongratulationImage_1 { get => pathCongratulationImage_1; set => SetField(ref pathCongratulationImage_1, value); }
        public string PathCongratulationImage_2 { get => pathCongratulationImage_2; set => SetField(ref pathCongratulationImage_2, value); }
        public string PathCongratulationImage_3 { get => pathCongratulationImage_3; set => SetField(ref pathCongratulationImage_3, value); }
        public string PathCongratulationImage_4 { get => pathCongratulationImage_4; set => SetField(ref pathCongratulationImage_4, value); }
        public bool IsAddImage { get => isAddImage; set => SetField(ref isAddImage, value); }
        public bool IsChooseImage_1 { get => isChooseImage_1; set => SetField(ref isChooseImage_1, value); }
        public bool IsChooseImage_2 { get => isChooseImage_2; set => SetField(ref isChooseImage_2, value); }
        public bool IsChooseImage_3 { get => isChooseImage_3; set => SetField(ref isChooseImage_3, value); }
        public bool IsChooseImage_4 { get => isChooseImage_4; set => SetField(ref isChooseImage_4, value); }
        public string PathPanelTheme { get => pathPanelTheme; set => SetField(ref pathPanelTheme, value); }
        public bool IsAddPoem { get => isAddPoem; set => SetField(ref isAddPoem, value); }
        public bool IsAddСliche { get => isAddСliche; set => SetField(ref isAddСliche, value); }
        public string PoemText { get => poemText; set => SetField(ref poemText, value); }
        public string ClicheText { get => clicheText; set => SetField(ref clicheText, value); }
        internal Congratulation Profile { get => profile; set => SetField(ref profile, value); }
        public bool IsNotEmptyPoem { get => isNotEmptyPoem; set => SetField(ref isNotEmptyPoem, value); }
        public bool IsNotEmptyCliche { get => isNotEmptyCliche; set => SetField(ref isNotEmptyCliche, value); }
    }
}
