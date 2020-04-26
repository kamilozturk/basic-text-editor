using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyTextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TabManager TabManager { get; set; }

        private bool LockTabCreation = false;

        public MainWindow()
        {
            TabManager = new TabManager();
            TabManager.RecentFiles.CollectionChanged += RecentFiles_CollectionChanged;
            FillRecentFilesMenu();

            InitializeComponent();
        }

        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("OpenRecentFilesCommand", typeof(ICommand), typeof(MainWindow));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        private void RecentFiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FillRecentFilesMenu();
        }

        private void FillRecentFilesMenu()
        {
            var command = new OpenRecentFileCommand(ReOpenFileAsync, TabManager);

            TabManager.RecentFilesMenu.Clear();

            TabManager.RecentFilesMenu.Add(new MenuItem
            {
                Header = "Reopen Closed File (Ctrl+Shift+T)",
                IsEnabled = TabManager.HasRecentFiles,
                Command = command,
                CommandParameter = "open"
            });


            foreach (var item in TabManager.RecentFiles)
            {
                TabManager.RecentFilesMenu.Add(new MenuItem
                {
                    Header = item,
                    IsEnabled = TabManager.HasRecentFiles,
                    Command = command,
                    CommandParameter = item
                });
            }

            TabManager.RecentFilesMenu.Add(new MenuItem
            {
                Header = "Clear Items",
                IsEnabled = TabManager.HasRecentFiles,
                Command = command,
                CommandParameter = "clear"
            });
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var tabNo = (int)button.CommandParameter;
            var tab = TabManager.Tabs.FirstOrDefault(x => x.TabNo == tabNo);

            CloseTab(tab);
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            AddNewTab();
        }

        private void AddNewTab()
        {
            TabManager.AddNewTab();
            JumpToLastTab();
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            await OpenFileAsync();
        }

        private async Task OpenFileAsync()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "(*.txt)|*.txt|All Files (*.*)|*.*";

            var result = dialog.ShowDialog();

            if (result == true)
            {
                await TabManager.OpenFileAsync(dialog.FileName);
            }

            JumpToLastTab();
        }

        private void NewCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            AddNewTab();
        }

        private void JumpToLastTab()
        {
            tabControl1.SelectedIndex = TabManager.Tabs.Count - 1;
        }

        private void OpenCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private async void OpenCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await OpenFileAsync();
        }

        private void TabControl1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var tab = tabControl1.SelectedValue as MyTab;

            txtContent.Text = tab?.Content;
        }

        private void TxtContent_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (LockTabCreation)
                return;

            var tab = tabControl1.SelectedValue as MyTab;

            if (tab == null)
            {
                if (TabManager.Tabs.Count == 0)
                {
                    TabManager.AddNewTab();

                    tab = TabManager.Tabs.Last();
                    tab.Content = txtContent.Text;
                    JumpToLastTab();
                }
                else
                {
                    tab = TabManager.Tabs.First();
                    tab.Content = txtContent.Text;
                    tabControl1.SelectedIndex = 0;
                }
            }

            tab.Content = txtContent.Text;
        }

        private void CloseCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var tab = tabControl1.SelectedValue as MyTab;

            CloseTab(tab);
        }

        private void CloseTab(MyTab tab)
        {
            if (tab == null)
                return;

            LockTabCreation = true;

            if (tab.Modified)
            {
                var result = MessageBox.Show("New file has been modified, save changes?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (tab.Path == null)
                    {
                        SaveAsFile(tab);
                    }
                    else
                    {
                        SaveFile(tab, tab.Path);
                    }
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            TabManager.CloseTab(tab);

            JumpToLastTab();

            LockTabCreation = false;
        }

        private void CloseCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = tabControl1.SelectedIndex != -1;
        }

        private void SaveCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = tabControl1.SelectedIndex != -1;
        }

        private void SaveCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (tabControl1.SelectedValue is MyTab tab)
            {
                if (tab.Path != null)
                {
                    SaveFile(tab, tab.Path);
                }
                else
                {
                    SaveAsFile(tab);
                }
            }
        }

        private void SaveAsFile(MyTab tab)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "(*.txt)|*.txt|All Files (*.*)|*.*";
            var result = dialog.ShowDialog();

            if (result == true)
            {
                SaveFile(tab, dialog.FileName);
            }
        }

        private void SaveFile(MyTab tab, string path)
        {
            tab.Content = txtContent.Text;
            tab.Path = path;
            using var writer = new StreamWriter(path, false);
            writer.Write(tab.Content);
            tab.Modified = false;
            tab.Title = Path.GetFileName(path);
        }

        private void SaveAsCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = tabControl1.SelectedIndex != -1;
        }

        private void SaveAsCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (tabControl1.SelectedValue is MyTab tab)
            {
                SaveAsFile(tab);
            }
        }

        private async void ReOpenButton_Click(object sender, RoutedEventArgs e)
        {
            await ReOpenLastFileAsync();
        }

        private async Task ReOpenLastFileAsync()
        {
            var path = TabManager.RecentFiles.Last();

            await ReOpenFileAsync(path);
        }

        private async Task ReOpenFileAsync(string path)
        {
            if (path != null && File.Exists(path))
            {
                await TabManager.OpenFileAsync(path);
            }

            JumpToLastTab();
        }

        private async void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.T)
            {
                await ReOpenLastFileAsync();
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.W)
            {
                if (tabControl1.SelectedValue is MyTab tab)
                {
                    CloseTab(tab);
                }
            }
            else if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.W)
            {
                CloseAllFiles();
            }
        }

        private void ClearRecentItemsButton_Click(object sender, RoutedEventArgs e)
        {
            TabManager.RecentFiles.Clear();
        }

        private void CloseAllFiles()
        {
            foreach (var tab in TabManager.Tabs.ToList())
            {
                CloseTab(tab);
            }
        }

        private void CloseAllFilesButton_Click(object sender, RoutedEventArgs e)
        {
            CloseAllFiles();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            CloseAllFiles();
            Close();
        }
    }
}
