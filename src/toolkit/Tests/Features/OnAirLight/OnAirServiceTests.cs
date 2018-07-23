using NSubstitute;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.HSB;
using ReactiveUI;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Common.Features.OnAirLight;
using Xunit;

namespace Tests.Features.OnAirLight
{
    public class OnAirServiceTests
    {
        [Theory]
        [InlineData("", "")]
        public void TheLightIsChangedWhenThereIsNoMatch(string processName, string windowName)
        {
            var activeProcessesService = Substitute.For<IActiveProcessesService>();
            activeProcessesService.ProcessNames.Returns(new ReactiveList<string>() { processName });
            activeProcessesService.ProcessNames.Returns(new ReactiveList<string>() { windowName });

            var phillipsHueService = Substitute.For<IPhillipsHueService>();

            var light = new Light()
            {
                Id = "5",
                Name = "Office Door",
            };

            phillipsHueService.Lights.Returns(new ReactiveList<Light>() { light });

            var sut = new OnAirServiceBuilder()
                .WithActiveProcessesService(activeProcessesService)
                .WithPhillipsHueService(phillipsHueService)
                .Build();

            // user interface is updated
            sut.IsOnAir.ShouldBeFalse();

            // the correct light is turned on
            phillipsHueService.Received().SendCommandAsync(
                Arg.Is<LightCommand>(x => x.On.Value == true),
                Arg.Is<IEnumerable<string>>(x => x.Contains(light.Id)));

            // TODO: assert that the light is set to the correct color
        }

        [Theory]
        // processes
        [InlineData("vMix64", "")]
        // window titles
        [InlineData("", "Zoom Participant")]
        public void TheLightIsChangedWhenThereIsAMatch(string processName, string windowName)
        {
            var activeProcessesService = Substitute.For<IActiveProcessesService>();
            activeProcessesService.ProcessNames.Returns(new ReactiveList<string>() { processName });
            activeProcessesService.WindowTitles.Returns(new ReactiveList<string>() { windowName });

            var phillipsHueService = Substitute.For<IPhillipsHueService>();

            var light = new Light()
            {
                Id = "5",
                Name = "Office Door",
            };
            phillipsHueService.Lights.Returns(new ReactiveList<Light>() { light });


            var sut = new OnAirServiceBuilder()
                .WithActiveProcessesService(activeProcessesService)
                .WithPhillipsHueService(phillipsHueService)
                .Build();

            // user interface is updated
            sut.IsOnAir.ShouldBeTrue();

            // the correct light is turned on
            phillipsHueService.Received().SendCommandAsync(
                Arg.Is<LightCommand>(x => x.On.Value == true),
                Arg.Is<IEnumerable<string>>(x => x.Contains(light.Id)));

            // TODO: assert that the light is set to the correct color
        }
    }
}
