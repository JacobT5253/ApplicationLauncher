using System;
using System.Collections.Generic;
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

namespace ApplicationLauncher
{
    /// <summary>
    /// Interaction logic for ProfileTemplate.xaml
    /// </summary>
    public partial class ProfileTemplate : UserControl
    {

        private Profile profile;


        public ProfileTemplate(Profile profile)
        {
            InitializeComponent();
            this.profile = profile;
            this.ProfileNameLabel.Content = profile.Name;
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
