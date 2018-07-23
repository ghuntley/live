using Q42.HueApi;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Common.Features.OnAirLight
{
    public interface IPhillipsHueService
    {
        ReactiveList<Light> Lights { get; }
        Task SendCommandAsync(LightCommand command, IEnumerable<string> targetLights);
    }
}
