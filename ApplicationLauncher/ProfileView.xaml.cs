using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public event Action<int> RequestViewSwitch;
        public event Action<Profile> ProfileEdited;

        public List<Profile> profiles;
        private ProfileManager profileManager;
        public ProfileView()
        {
            InitializeComponent();
            profileManager = new ProfileManager();
        }


        public void LoadProfiles()
        {
            profiles = profileManager.LoadProfiles();

            LoadProfilesIntoView(profiles);

        }


        private void LoadProfilesIntoView(IEnumerable<Profile> profileList)
        {
            profilesPanel.Items.Clear();
            foreach (var profile in profiles)
            {
                var template = new ProfileTemplate(profile);
                template.EditProfile += OnProfileEdited;  // Subscribe to the EditProfile event
                profilesPanel.Items.Add(template);
            }
        }

        private void OnProfileEdited(Profile profile)
        {
            ProfileEdited?.Invoke(profile);
        }

        private void SwitchView(int profile)
        {
            RequestViewSwitch?.Invoke(profile);
        }


        private void NewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchView(1);
        }

    }
}
