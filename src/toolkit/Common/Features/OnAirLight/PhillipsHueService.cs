using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using Genesis.Ensure;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using ReactiveUI;
using Toolbox.Common.Features.State;
using System.Reactive;
using DynamicData;

namespace Toolbox.Common.Features.OnAirLight
{
    public class PhillipsHueService : IPhillipsHueService
    {
        private IScheduler _scheduler;
        private ILocalHueClient _client;
        private ISourceList<Light> _lights = new SourceList<Light>();

        public PhillipsHueService(string appKey, string ipAddress, IScheduler scheduler)
        {
            Ensure.ArgumentNotNull(appKey, nameof(appKey));
            Ensure.ArgumentNotNull(ipAddress, nameof(ipAddress));
            Ensure.ArgumentNotNull(scheduler, nameof(scheduler));

            _client = new LocalHueClient(ipAddress);
            _client.Initialize(appKey);

            _scheduler = scheduler;
        }

        public IObservableList<Light> Lights => _lights.AsObservableList();

        public async Task SendCommandAsync(LightCommand command, IEnumerable<string> targetLights)
        {
            Ensure.ArgumentNotNull(command, nameof(command));
            Ensure.ArgumentNotNull(targetLights, nameof(targetLights));

            await _client.SendCommandAsync(command, targetLights).ContinueOnAnyContext();
        }
    }
}
