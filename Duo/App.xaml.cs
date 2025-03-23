using System;
using Microsoft.UI.Xaml;
using Microsoft.Extensions.Configuration;
using Duo.Data;
using Duo.Repositories;
using System.IO;
using DuolingoNou;
using DuolingoNou.Views;
using Duo.Models;

namespace Duo
{
    public partial class App : Application
    {
        private static IConfiguration _configuration;
        private static DataLink _dataLink;
        public static UserRepository userRepository;
        public static User CurrentUser { get; set; }

        public static Window MainAppWindow { get; private set; }

        public App()
        {
            this.InitializeComponent();
            _configuration = InitializeConfiguration();
            _dataLink = new DataLink(_configuration);
            userRepository = new UserRepository(_dataLink);
        }

        private IConfiguration InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainAppWindow = new MainWindow(); // store reference
            MainAppWindow.Content = new ShellPage();
            MainAppWindow.Activate();
        }

        private Window? m_window;
    }
}