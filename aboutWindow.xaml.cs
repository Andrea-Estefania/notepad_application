
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
using System.Windows.Shapes;

namespace A02___WPF
{

    public partial class aboutWindow : Window
    {
        public aboutWindow()
        {
            InitializeComponent();
        }

        /*  
        Name	:   okClose_Click
        Purpose :   Allows the user to close the about box.
        Inputs	:	NONE
        Outputs	:	NONE
        Returns	:	NONE
        */

        private void okClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
