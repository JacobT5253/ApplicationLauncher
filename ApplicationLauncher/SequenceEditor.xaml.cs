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
    /// Interaction logic for SequenceEditor.xaml
    /// </summary>
    public partial class SequenceEditor : UserControl
    {
        public event Action<int> RequestViewSwitch;


        public SequenceEditor()
        {
            InitializeComponent();
        }

        public void Refresh()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SwitchViews();
        }

        private void SwitchViews()
        {
            RequestViewSwitch?.Invoke(1);
        }


    }
}
