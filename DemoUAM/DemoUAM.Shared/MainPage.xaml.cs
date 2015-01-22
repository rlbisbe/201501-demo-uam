using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DemoUAM
{
    public sealed partial class MainPage : Page
    {
        private TaskListViewModel viewModel;
        public MainPage()
        {
            this.InitializeComponent();
            viewModel = new TaskListViewModel();
            this.DataContext = viewModel;
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
        }
    }
}
