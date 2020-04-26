using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyTextEditor
{
    public class TabManager : DependencyObject
    {
        public ObservableCollection<MyTab> Tabs { get; private set; }
        public ObservableCollection<string> RecentFiles { get; private set; }

        public ObservableCollection<MenuItem> RecentFilesMenu { get; set; }

        public bool HasRecentFiles
        {
            get { return (bool)GetValue(HasRecentFilesProperty); }
            set { SetValue(HasRecentFilesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasRecentFiles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasRecentFilesProperty =
            DependencyProperty.Register("HasRecentFiles", typeof(bool), typeof(TabManager), new PropertyMetadata(false));

        public bool HasTabs
        {
            get { return (bool)GetValue(HasTabsProperty); }
            set { SetValue(HasTabsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasTabs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasTabsProperty =
            DependencyProperty.Register("HasTabs", typeof(bool), typeof(TabManager), new PropertyMetadata(false));



        public TabManager()
        {
            Tabs = new ObservableCollection<MyTab>();
            RecentFiles = new ObservableCollection<string>();
            RecentFilesMenu = new ObservableCollection<MenuItem>();
            RecentFiles.CollectionChanged += RecentFiles_CollectionChanged;
            Tabs.CollectionChanged += Tabs_CollectionChanged;
            AddNewTab();
        }

        private void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HasTabs = Tabs.Count > 0;
        }

        private void RecentFiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HasRecentFiles = RecentFiles.Count > 0;
        }

        public async Task OpenFileAsync(string path)
        {
            var fileName = Path.GetFileName(path);
            using var reader = File.OpenText(path);
            var content = await reader.ReadToEndAsync();

            AddNewTab(fileName, content, path);
            CloseInitialTabIfNoNeed();
        }

        public void AddNewTab(string fileName = null, string content = null, string path = null)
        {
            var index = GetNextIndex();

            var tab = new MyTab()
            {
                Title = fileName ?? "Tab" + index,
                Content = content,
                TabNo = index,
                Path = path
            };

            Tabs.Add(tab);
        }

        public void CloseTab(MyTab tab)
        {
            if (!string.IsNullOrEmpty(tab.Path) && !RecentFiles.Contains(tab.Path))
                RecentFiles.Add(tab.Path);

            Tabs.Remove(tab);
        }

        private int GetNextIndex()
        {
            if (Tabs.Count == 0)
                return 1;

            return Tabs.Max(x => x.TabNo) + 1;
        }

        private void CloseInitialTabIfNoNeed()
        {
            if (Tabs.Count == 2)
            {
                var tab = Tabs.First();

                if (string.IsNullOrEmpty(tab.Content))
                {
                    Tabs.RemoveAt(0);
                }

                Tabs.First().TabNo = 1;
            }
        }
    }
}
