using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using System.IO;
using System.Runtime.InteropServices;


namespace ApplicationLauncher
{
    /// <summary>
    /// Interaction logic for SequenceEditor.xaml
    /// </summary>
    public partial class SequenceEditor : UserControl
    {
        public event Action<int> RequestViewSwitch;
        public Profile Profile { get; set; }
        private ProfileManager profileManager;
        public List<MyApp> appList;
        
        public SequenceEditor()
        {
            InitializeComponent();
            profileManager = new ProfileManager();
        }

        public void LoadAppsIntoView(Profile profile)
        {
            appList = profileManager.LoadApps(profile);

            appsPanel.Items.Clear();
            MyApp last = appList.Last();
            foreach (var app in appList)
            {
                var template = new AppTemplate(app);
                
                if (app == last)
                {
                    template.BorderBrush = Brushes.Black;
                    template.BorderThickness = new Thickness(3, 3, 3, 3);
                }
                else
                {
                    template.BorderBrush= Brushes.Black;
                    template.BorderThickness = new Thickness(3, 3, 3, 0);
                }
                appsPanel.Items.Add(template);
            }
        }

        public void ClearAppList()
        {
            appList = new List<MyApp>();
            appsPanel.Items.Clear();
        }

        private void SwitchView(int num)
        {
            RequestViewSwitch?.Invoke(num);
        }


        private void AddAppButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Executable files (*.exe)|*.exe|Shortcut files (*.lnk)|*.lnk",
                Multiselect = true,
            };
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                List<MyApp> myApps = ProcessSelectedFiles(openFileDialog.FileNames);
                // Do something with the list of MyApp objects
            }
        }

        private List<MyApp> ProcessSelectedFiles(string[] fileNames)
        {
            List<MyApp> apps = new List<MyApp>();
            foreach (string file in fileNames)
            {
                string filePath = file;
                string appName = System.IO.Path.GetFileNameWithoutExtension(file);

                if (System.IO.Path.GetExtension(file).ToLower() == ".lnk")
                {
                    // Use the Shell32 method to resolve the shortcut target
                    filePath = GetShortcutTarget(file);
                    if (filePath != null)
                    {
                        appName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                    }
                }

                apps.Add(new MyApp(appName, filePath, 0));
            }
            return apps;
        }

        public static string GetShortcutTarget(string shortcutPath)
        {
            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
            dynamic folder = shell.NameSpace(System.IO.Path.GetDirectoryName(shortcutPath));
            dynamic item = folder.ParseName(System.IO.Path.GetFileName(shortcutPath));

            if (item != null)
            {
                dynamic link = item.GetLink;
                return link.Path;
            }

            return null; // Return null if the path couldn't be resolved
        }
        private void SaveSequenceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchView(3);
        }
    }
}
