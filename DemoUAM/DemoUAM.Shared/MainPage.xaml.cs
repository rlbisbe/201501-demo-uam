using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DemoUAM
{
    public sealed partial class MainPage : Page
    {
        private TaskSync taskSync;
        private TaskListViewModel viewModel;
        public MainPage()
        {
            this.InitializeComponent();

            viewModel = new TaskListViewModel();
            SyncTaskList(viewModel);
            this.DataContext = viewModel;
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            taskSync.UploadTask(viewModel.NewTask);
            viewModel.Save();
        }

        private async void SyncTaskList(TaskListViewModel viewModel)
        {
            taskSync = new TaskSync();
            var tasks = await taskSync.GetTasks();
            viewModel.TaskList = new ObservableCollection<TaskModel>(tasks);
        }
    }
}
