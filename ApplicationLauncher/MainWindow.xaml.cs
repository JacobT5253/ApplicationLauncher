using System.Diagnostics;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private Point initialMousePos;

        private UserControl profileView;
        private UserControl sequenceEditor;
        private UserControl activeView;

        public MainWindow()
        {
            InitializeComponent();
            // subscribe to the event handler for when the window state is changed
            this.StateChanged += new EventHandler(Window_StateChanged);

            // initialize the views
            profileView = new ProfileView();
            sequenceEditor = new SequenceEditor();

            // subscribe to the view switching button methods
            ((ProfileView)profileView).RequestViewSwitch += SwitchView;
            ((SequenceEditor)sequenceEditor).RequestViewSwitch += SwitchView;
            ((ProfileView)profileView).ProfileEdited += ProfileView_ProfileEdited;
            // initial view switch to display the profile view 1 = blank sequence editor, 2 = specific sequence editor, 3 = profile page
            SwitchView(3);
        }

        public void SwitchView(int viewNumber)
        {
            if (viewNumber == 1)
            {
                ((SequenceEditor)sequenceEditor).ClearAppList();
                contentControl.Content = sequenceEditor;
            }
            else if (viewNumber == 2)
            {
                contentControl.Content = sequenceEditor;
            }
            else
            {
                contentControl.Content = profileView;
                ((ProfileView)profileView).LoadProfiles();
            }
        }

        private void ProfileView_ProfileEdited(Profile profile)
        {
            ((SequenceEditor)sequenceEditor).LoadAppsIntoView(profile);
            Debug.WriteLine($"Profile {profile.Name} has been clicked and is now being edited");
            
            SwitchView(2);
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Remember mouse down position, relative to screen
                initialMousePos = this.PointToScreen(e.GetPosition(this));

                if (WindowState == WindowState.Maximized)
                {
                    // Temporarily store the size and location of the fully maximized window
                    var maxTop = this.Top;
                    var maxWidth = this.Width;
                    var maxHeight = this.Height;

                    // Restore the window
                    this.WindowState = WindowState.Normal;

                    // Adjust window position under cursor
                    this.Left = initialMousePos.X - (this.RestoreBounds.Width * 0.5);
                    this.Top = initialMousePos.Y - (this.RestoreBounds.Height * 0.1);

                    // Ensure window stays within screen bounds
                    this.Left = Math.Max(this.Left, 0);
                    this.Top = Math.Max(this.Top, 0);
                    this.Left = Math.Min(this.Left, SystemParameters.WorkArea.Width - this.RestoreBounds.Width);
                    this.Top = Math.Min(this.Top, SystemParameters.WorkArea.Height - this.RestoreBounds.Height);
                }

                this.DragMove();
            }
        }

        private void TitleBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Release the mouse capture if it's currently captured
            if (Mouse.Captured == sender as UIElement)
            {
                Mouse.Capture(null);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Closes the window
        }

        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            // Toggles the window state between normal and maximized
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                //MaximizeRestoreButton.Content = "\uE922";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                //MaximizeRestoreButton.Content = "\uE923";
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimizes the window
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                // Adjust for the system taskbar and window border overlap
                this.BorderThickness = new Thickness(7, 7, 7, 7);
                MaximizeRestoreButton.Content = "\uE923";
            }
            else
            {
                // Remove borders when window is not maximized
                this.BorderThickness = new Thickness(0);
                MaximizeRestoreButton.Content = "\uE922";
            }
        }
        
    }
}