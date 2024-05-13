using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
            // profiles is the list of profiles read in from the csv file
            var profiles = new List<Profile>();
            // masterdata is the raw csv data split by commas
            var masterData = File.ReadAllText(MasterFilePath).Split(',');

            // iterate throught the csv file
            foreach (var profileName in masterData)
            {
                Debug.WriteLine($"PROFILE NAME = {profileName}");
                var profilePath = Path.Combine(ProfilesDirectory, $"{profileName.Trim()}.csv");
                // make sure the profile actually exists
                if (File.Exists(profilePath))
                {
                    // create new profile and add to the list
                    profiles.Add(new Profile(profileName.Trim(), profilePath));
                }
                else
                {
                    // code to remove the profile that does not have a file from the csv file
                }

            }

            return profiles;
        }

        public void SaveProfiles()
        {

        }

        public List<MyApp> LoadApps(Profile profile)
        {
            string csvFilePath = profile.FilePath;
            var apps = new List<MyApp>();

            // Check if the file exists
            if (!File.Exists(csvFilePath))
            {
                throw new FileNotFoundException($"The file at {csvFilePath} was not found.");
            }

            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvFilePath);

            // Process each line except the header
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(','); // Split the line into parts separated by commas

                if (parts.Length == 3) // Ensure there are exactly three parts: Name, Path, and Delay
                {
                    string name = parts[0];
                    string path = parts[1];
                    if (int.TryParse(parts[2], out int delay))
                    {
                        Debug.WriteLine($"Adding app {name} with path = {path} to the app list");
                        apps.Add(new MyApp(name, path, delay));
                    }
                    else
                    {
                        throw new FormatException($"Invalid delay value on line {i + 1}: {parts[2]}");
                    }
                }
                else
                {
                    throw new FormatException($"Invalid data format on line {i + 1}");
                }
            }

            return apps;
        }
    }
}
