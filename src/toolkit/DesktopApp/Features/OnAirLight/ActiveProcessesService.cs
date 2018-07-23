using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using Genesis.Ensure;
using ReactiveUI;
using Toolbox.Common.Features.OnAirLight;

namespace DesktopApp.Features.OnAirLight
{
    public class ActiveProcessesService : IActiveProcessesService
    {
        private IScheduler _scheduler;
        private TimeSpan _refreshInterval;
        private SourceList<string> _processNames = new SourceList<string>();
        private SourceList<string> _windowTitles = new SourceList<string>();

        public ActiveProcessesService(TimeSpan refreshInterval, IScheduler scheduler)
        {
            Ensure.ArgumentNotNull(refreshInterval, nameof(refreshInterval));
            Ensure.ArgumentNotNull(scheduler, nameof(scheduler));

            _scheduler = scheduler;
            _refreshInterval = refreshInterval;

            _scheduler.SchedulePeriodic(_refreshInterval, () =>
            {
                var processes = Process.GetProcesses();

                _processNames.Edit(innerList =>
                {
                    innerList.Clear();
                    innerList.AddRange(processes.Select(x => x.ProcessName));
                });

                _processNames.Edit(innerList =>
                {
                    innerList.Clear();
                    innerList.AddRange(processes.Select(x => x.MainWindowTitle));
                });
            });

        }

        public IObservableList<string> ProcessNames => _processNames.AsObservableList();

        public IObservableList<string> WindowTitles => _windowTitles.AsObservableList();
    }
}