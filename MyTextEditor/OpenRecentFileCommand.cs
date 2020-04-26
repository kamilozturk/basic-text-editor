using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyTextEditor
{
    class OpenRecentFileCommand : ICommand
    {
        private readonly Func<string, Task> reOpenFileAsync;
        private readonly TabManager tabManager;

        public OpenRecentFileCommand(Func<string, Task> reOpenFileAsync, TabManager tabManager)
        {
            this.reOpenFileAsync = reOpenFileAsync;
            this.tabManager = tabManager;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (!(parameter is string cmd))
                return;

            if (cmd == "open" && tabManager.HasRecentFiles)
            {
                var path = tabManager.RecentFiles.Last();

                if (path != null && File.Exists(path))
                {
                    await reOpenFileAsync(path);
                }
            }
            else if (cmd == "clear")
            {
                tabManager.RecentFiles.Clear();
            }
            else if (cmd != null && File.Exists(cmd))
            {
                await reOpenFileAsync(cmd);
            }
        }
    }
}
