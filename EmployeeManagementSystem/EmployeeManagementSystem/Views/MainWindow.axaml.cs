using Avalonia.Controls;

namespace EmployeeManagementSystem.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Current.MainVM;
        }
    }
}