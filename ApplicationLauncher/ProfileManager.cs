using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLauncher
{
    public class ProfileManager
    {
        private const string ProfilesDirectory = @"assets/profiles";
        private const string MasterFilePath = @"assets/master.csv";

        public List<Profile> LoadProfiles()
        {
            var profiles = new List<Profile>();
            var masterData = File.ReadAllText(MasterFilePath).Split(',');

            foreach (var profileName in masterData)
            {
                Debug.WriteLine($"{profileName}");
                var profilePath = Path.Combine(ProfilesDirectory, $"{profileName.Trim()}.csv");
                if (File.Exists(profilePath))
                {
                    profiles.Add(new Profile
                    {
                        Name = profileName.Trim(),
                        FilePath = profilePath
                    });
                }

            }

            return profiles;
        }
    }
}
