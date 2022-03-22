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
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;

namespace A02___WPF
{
    public partial class MainWindow : Window
    {
        myText currentTXT = new myText(); // Object to help keep tracking of the changes in the text editor

        public MainWindow()
        {
            InitializeComponent();
        }

        /*  
        Name	:   private void txtEditor_SelectionChanged

        Purpose :   It is used to keep track of how many characters have been typed.
                    Taken and partially adapted from: https://wpf-tutorial.com/common-interface-controls/statusbar-control/
                    Original by: WPF Tutorial
        Inputs	:	NONE
        Outputs	:	NONE
        Returns	:	NONE
        */

        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int row = txtEditor.GetLineIndexFromCharacterIndex(txtEditor.CaretIndex);
            int col = txtEditor.CaretIndex - txtEditor.GetCharacterIndexFromLineIndex(row);
            lblCursorPosition.Text = "Char " + (col + 1); // Updates the status bar showing the current count of characters 
        }

        /*  
        Name	:   MenuNew_Click
        Purpose :   Allows the user to create a new document. It looks for changes in the text box/file before creating a new file to ask the user if they want 
                    to save their previous file.
        Inputs	:	NONE
        Outputs	:	Open file dialog
        Returns	:	NONE
        */

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            bool changes = currentTXT.HasTextChanged; /// Track the changes

            if (changes == true) // Changes exist
            {
                DoYouWishToSave();
            }
            else
            {
                txtEditor.Text = "";
            }
        }

        /*  
        Name	:   MenuOpen_Click
        Purpose :   Allows the user to open a document. It looks for changes in the text box/file before opening a file to ask the user if they want 
                    to save their previous file.
        Inputs	:	NONE
        Outputs	:	NONE
        Returns	:	NONE
        */

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openNewFile = new OpenFileDialog(); /// File dialog to open a new file
            bool changes = currentTXT.HasTextChanged;

            openNewFile.Filter = "TXT Files|*.txt| All Files|*.*";

            if (changes == true)  /// Do we have changes that need to be save?
            {
                DoYouWishToSave();
            }

            bool? result = openNewFile.ShowDialog();

            if (result == true) // User decides to open a file
            {
                txtEditor.Text = System.IO.File.ReadAllText(openNewFile.FileName);
                currentTXT.HasTextChanged = false;
            }
        }

        /*  
        Name	:   Window_Closing
        Purpose :   Allows the user to close the application. It looks for changes in the text box/file before closing the application to ask the user if they want 
                    to save their previous file.
        Inputs	:	NONE
        Outputs	:	Message box to ask if the changes need to be save before closing
        Returns	:	NONE
        */

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            bool lookingForChanges = currentTXT.HasTextChanged;
            bool oneClose = currentTXT.Close;

            if (oneClose != true)
            {
                if (!string.IsNullOrEmpty(txtEditor.Text) && lookingForChanges == true) // Does the text box has anything?
                {
                    DoYouWishToSave();
                }
            }


        }

        /*  
        Name	:   SaveAs_Click
        Purpose :   Allows the user to save their current work.
        Inputs	:	NONE
        Outputs	:	Save dialog to allow the user to save or not their file
        Returns	:	NONE
        */

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog(); /// Save document dialog 
            saveFile.FileName = "New text File";
            saveFile.DefaultExt = ".txt";
            saveFile.Filter = "TXT Files|*.txt| All Files|*.*";

            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, txtEditor.Text);
            }
        }

        /*  
        Name	:   Close_click
        Purpose :   Allows the user to close the application. It looks for changes in the text box/file before closing the application to ask the user if they want 
            to save their previous file.
        Inputs	:	NONE
        Outputs	:	Message box to ask the user if they want to save before closing
        Returns	:	NONE
        */

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            bool lookingForChanges = currentTXT.HasTextChanged;

            if (!string.IsNullOrEmpty(txtEditor.Text) && lookingForChanges == true) // Does the text box has anything?
            {
                DoYouWishToSave();
            }
            currentTXT.Close = true;
            System.Windows.Application.Current.Shutdown(); // Close the application
            
        }

        /*  
        Name	:   MenuAbout_Click
        Purpose :   Allows the user to open a second window to learn about the notepad application.
        Inputs	:	NONE
        Outputs	:	NONE
        Returns	:	NONE
        */

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            aboutWindow about = new aboutWindow(); // Allows he about box to open
            about.ShowInTaskbar = false;
            about.Owner = Application.Current.MainWindow;
            about.ShowDialog();
        }

        /*  
        Name	:   txtEditor_TextChanged
        Purpose :   Allows the application to track if changes have been made in order to properly ask the user if they want to save their current file
        Inputs	:	NONE
        Outputs	:	NONE
        Returns	:	NONE
        */

        private void txtEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool changes_exist = currentTXT.HasTextChanged;

            if (changes_exist == false)
            {
                currentTXT.HasTextChanged = true;
            }
        }

        /*  
        Name	:   DoYouWishToSave
        Purpose :   To ask the user in the necessary events if they wish to save their work. It offers three options, Yes, No, Cancel.
        Inputs	:	NONE
        Outputs	:	Message box that will ask the user if they want to save their work.
        Returns	:	NONE
        */

        private void DoYouWishToSave()
        {
            if (!string.IsNullOrEmpty(txtEditor.Text))
            {
                MessageBoxResult newFile = MessageBox.Show("Do you want to save this file?", "Andrea's Notepad", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (newFile)
                {
                    case MessageBoxResult.Yes: // Switch to handle the result of the message box
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.FileName = "New text File";
                        saveFile.DefaultExt = ".txt";
                        saveFile.Filter = "TXT Files|*.txt| All Files|*.*";

                        if (saveFile.ShowDialog() == true) // User wants to save
                        {
                            File.WriteAllText(saveFile.FileName, txtEditor.Text);
                        }
                        txtEditor.Text = "";
                        currentTXT.HasTextChanged = false;
                        break;
                    case MessageBoxResult.No: // User does not want to save
                        txtEditor.Text = "";
                        currentTXT.HasTextChanged = false;
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }
    }
}
