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
using Microsoft.Win32;

namespace GameTimeTracker
{
    /// <summary>
    /// Interaction logic for FolderSelectionWindow.xaml
    /// </summary>
    public partial class FolderSelectionWindow : Window
    {
        private GameFolderHandler _gameHandler; 
        private List<string> _folders;
        private List<Game> _gameList;

        public FolderSelectionWindow(List<string> folderPaths, List<Game> gameList, GameFolderHandler handler)
        {
            InitializeComponent();
            _folders = folderPaths;
            _gameList = gameList;
            _gameHandler = handler;

            foreach (string folderPath in folderPaths)
            {
                PathsListBox.Items.Add(folderPath);
            }
        }

        private void OnSelectFolder(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Select Game Folder",
            };
            dialog.ShowDialog();
            if (!_folders.Contains(dialog.FolderName))
            {
                _folders.Add(dialog.FolderName);
                PathsListBox.Items.Add(dialog.FolderName);
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (PathsListBox.SelectedItem  != null) 
                _folders.Remove(PathsListBox.SelectedItem as string);
            PathsListBox?.Items.Remove(PathsListBox.SelectedItem);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _gameHandler.UpdateGameList();
        }
    }
}