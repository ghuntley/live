using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using Genesis.Ensure;
using ReactiveUI;
using Toolbox.Common.Features.OnAirLight;

namespace DesktopApp.Features.OnAirLight
{
    public class ActiveProcessesService : IActiveProcessesService
    {
        private IScheduler _scheduler;
        private TimeSpan _refreshInterval;


        public ActiveProcessesService(TimeSpan refreshInterval, IScheduler scheduler)
        {
            Ensure.ArgumentNotNull(refreshInterval, nameof(refreshInterval));
            Ensure.ArgumentNotNull(scheduler, nameof(scheduler));

            _scheduler = scheduler;
            _refreshInterval = refreshInterval;

            _scheduler.SchedulePeriodic(_refreshInterval, () =>
            {
                var processes = Process.GetProcesses();

                using (ProcessNames.SuppressChangeNotifications())
                {
                    ProcessNames.Clear();
                    ProcessNames.AddRange(processes.Select(x => x.ProcessName));
                }

                using (WindowTitles.SuppressChangeNotifications())
                {
                    WindowTitles.Clear();
                    WindowTitles.AddRange(processes.Select(x => x.MainWindowTitle));
                }
            });

        }
        public ReactiveList<string> ProcessNames { get; private set; } = new ReactiveList<string>();
        public ReactiveList<string> WindowTitles { get; private set; } = new ReactiveList<string>();
    }
}