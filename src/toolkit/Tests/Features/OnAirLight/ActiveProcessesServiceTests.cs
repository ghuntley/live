using DesktopApp.Features.OnAirLight;
using Microsoft.Reactive.Testing;
using ReactiveUI.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Tests.Features.OnAirLight
{
    public class ActiveProcessesServiceTests
    {
        [Fact]
        public void ProcessesAreRefereshedEveryInterval()
        {
            var refreshInterval = TimeSpan.FromSeconds(1);

            new TestScheduler().With(scheduler => {

                var sut = new ActiveProcessesServiceBuilder()
                    .WithScheduler(scheduler)
                    .WithRefreshInterval(refreshInterval)
                    .Build();

                // ensure collections are initialized by default
                sut.ProcessNames.ShouldBeEmpty();
                sut.WindowTitles.ShouldBeEmpty();

                // service should refresh the collections as per the interval
                scheduler.AdvanceBy(refreshInterval.Ticks);
                sut.ProcessNames.ShouldNotBeEmpty();
                sut.WindowTitles.ShouldNotBeEmpty();

                // wipe the colleciton
                sut.ProcessNames.Clear();
                sut.WindowTitles.Clear();
                sut.ProcessNames.ShouldBeEmpty();
                sut.WindowTitles.ShouldBeEmpty();

                // prove the collections are refreshed after the interval
                scheduler.AdvanceBy(refreshInterval.Ticks);
                sut.ProcessNames.ShouldNotBeEmpty();
                sut.WindowTitles.ShouldNotBeEmpty();
            });
        }
    }
}
