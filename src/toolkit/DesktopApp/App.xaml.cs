using Autofac;
using DesktopApp.Features.Logging;
using DesktopApp.Features.OnAirLight;
using ReactiveUI;
using Serilog;
using Serilog.Events;
using Splat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Toolbox.Common.Features.Dashboard;
using Toolbox.Common.Features.OnAirLight;
using Toolbox.Common.Features.State;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IContainer Container;

        public App()
        {
            this.Startup += this.OnStartup;
            this.Dispatcher.UnhandledException += OnUnhandledException;
            this.Exit += this.OnExit;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.RollingFile("! log-{Date}.txt", retainedFileCountLimit: 7)
                .CreateLogger();

            var builder = new ContainerBuilder();

            builder.Register(c => new StateService(null))
                .As<IStateService>()
                .SingleInstance();

            builder.Register(c => new ActiveProcessesService(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler))
                .As<IActiveProcessesService>()
                .SingleInstance();

            var appKey = "ccrKHarp9CriyKKxIb7MBlBybY6WuHJCI2Ihkm5c";
            var ipAddress = "10.0.0.18";

            builder.Register(c => new PhillipsHueService(appKey, ipAddress, RxApp.MainThreadScheduler))
                .As<IPhillipsHueService>()
                .SingleInstance();

            builder.Register(c => new OnAirService(c.Resolve<IActiveProcessesService>(), c.Resolve<IPhillipsHueService>()))
                 .As<IOnAirService>()
                 .SingleInstance();

            Container = builder.Build();

            var logger = new DebugLogger();
            Locator.CurrentMutable.RegisterConstant(logger, typeof(Splat.ILogger));

            var test = new DashboardViewModel(Container.Resolve<IOnAirService>());
        }

        private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.CloseAndFlush();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            Log.CloseAndFlush();
        }
    }
}
