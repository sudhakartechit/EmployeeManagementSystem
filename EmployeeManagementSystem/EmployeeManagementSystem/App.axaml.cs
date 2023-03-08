using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EmployeeManagementSystem.ViewModels;
using EmployeeManagementSystem.Views;
using EmployeeRepository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeManagementSystem
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Services = ConfigureServices();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; private set; }

        //ConfigureServices here using Microsoft.Extensions.DependencyInjection
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection(); ;
            services.AddSingleton<IEmployeeClientRepository, EmployeeClientRepository>();
            services.AddSingleton<MainWindowViewModel>();
            return services.BuildServiceProvider();
        }

        public MainWindowViewModel MainVM => Services.GetService<MainWindowViewModel>();

    }
}