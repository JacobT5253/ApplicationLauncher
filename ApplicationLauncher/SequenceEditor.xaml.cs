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


        private void SwitchView(int num)
        {
            RequestViewSwitch?.Invoke(num);
        }


        private void AddAppButton_Click(object sender, RoutedEventArgs e)
        {

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
