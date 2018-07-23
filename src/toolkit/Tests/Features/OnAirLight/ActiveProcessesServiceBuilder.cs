using DesktopApp.Features.OnAirLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Features.OnAirLight
{
    public sealed class ActiveProcessesServiceBuilder : IBuilder
    {
        private TimeSpan _refreshInterval;
        private IScheduler _scheduler;

        public ActiveProcessesServiceBuilder()
        {
            _scheduler = CurrentThreadScheduler.Instance;
        }

        public ActiveProcessesServiceBuilder WithRefreshInterval(TimeSpan refreshInterval) => this.With(ref _refreshInterval, refreshInterval);

        public ActiveProcessesServiceBuilder WithScheduler(IScheduler scheduler) => this.With(ref _scheduler, scheduler);

        public ActiveProcessesService Build()
        {
            return new ActiveProcessesService(_refreshInterval, _scheduler);
        }

        public static implicit operator ActiveProcessesService(ActiveProcessesServiceBuilder builder) => builder.Build();
    }
}
