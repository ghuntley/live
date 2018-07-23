using Genesis.Ensure;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Serilog;
using System.Threading.Tasks;
using System.Linq;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.HSB;

namespace Toolbox.Common.Features.OnAirLight
{
    public class OnAirService : ReactiveObject, IOnAirService
    {
        private IActiveProcessesService _activeProcessesService;
        private IPhillipsHueService _phillipsHueService;

        public OnAirService(IActiveProcessesService activeProcessesService, IPhillipsHueService phillipsHueService)
        {
            Ensure.ArgumentNotNull(activeProcessesService, nameof(activeProcessesService));
            Ensure.ArgumentNotNull(phillipsHueService, nameof(phillipsHueService));

            _activeProcessesService = activeProcessesService;
            _phillipsHueService = phillipsHueService;

            // note: throwing away subscription reference because this is a single activation singleton (ie. memory leak isn't a concern)
            this.WhenAnyValue(x => x._activeProcessesService.ProcessNames, x => x._activeProcessesService.WindowTitles)
                .Do(tuple => Log.Verbose("The following {Processes} and {WindowTitles} are currently active", tuple.Item1, tuple.Item2))
                .Where(tuple => ProcessesMatch(tuple.Item1) || WindowTitlesMatch(tuple.Item2))
                .Select(tuple =>
                {
                    if (tuple.Item1.Count > 0 || tuple.Item2.Count > 0)
                    {
                        Log.Information("{Matches} detected which means the light will turn on", tuple);
                        return true;
                    }
                    return false;
                })
                .Do(boolean => Log.Information("The IsOnAir light is now {Status}", boolean))
                .ToProperty(this, x => x.IsOnAir, out _isOnAir);


            // note: throwing away subscription reference because this is a single activation singleton (ie. memory leak isn't a concern)
            this.WhenAnyValue(vm => vm.IsOnAir, vm => vm._phillipsHueService.Lights)
                .Subscribe(async _ =>
                {
                    try
                    {
                        await ActivateOfficeDoorLight(IsOnAir, _phillipsHueService.Lights);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed change the lighting state");
                    }
                });
        }

        private bool ProcessesMatch(ReactiveList<string> processes)
        {
            if (processes == null) return false;

            return processes.Any(proc => proc.Contains("vMix64"));
        }

        private bool WindowTitlesMatch(ReactiveList<string> windowtitles)
        {
            if (windowtitles == null) return false;

            return windowtitles.Any(proc => proc.Contains("Zoom Participant"));
        }

        private async Task ActivateOfficeDoorLight(bool isOnAir, ReactiveList<Light> lights)
        {
            var command = new LightCommand();
            if (isOnAir)
            {
                command.TurnOn().SetColor(new RGBColor("#FF0000")); // red
            }
            else
            {
                command.TurnOn().SetColor(new RGBColor("#008000")); // green 
            }

            //var officeDoorLight = lights.Single(x => x.Name == "Office Door");
            var officeDoorLight = new Light() { Id = "5" };

            await _phillipsHueService.SendCommandAsync(command, new List<string> { officeDoorLight.Id });
        }

        private readonly ObservableAsPropertyHelper<bool> _isOnAir;
        public bool IsOnAir => _isOnAir.Value;

        private readonly ObservableAsPropertyHelper<Light> _officeDoorLight;
        public Light OfficeDoorLight => _officeDoorLight.Value;
    }
}
