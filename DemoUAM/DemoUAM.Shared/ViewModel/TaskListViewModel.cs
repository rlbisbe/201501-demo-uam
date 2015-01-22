using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DemoUAM
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        public TaskModel NewTask { get; set; }

        public ObservableCollection<TaskModel> TaskList { get; set; }

        public TaskListViewModel()
        {
            NewTask = new TaskModel();
            TaskList = new ObservableCollection<TaskModel>();
        }

        public void Save()
        {
            this.TaskList.Add(NewTask);
            NewTask = new TaskModel();
            NotifyPropertyChanged("NewTask");
        }


        private void NotifyPropertyChanged(string param)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                changed(this, new PropertyChangedEventArgs(param));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
