using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Common.Features.OnAirLight;

namespace Tests.Features.OnAirLight
{
    public sealed class OnAirServiceBuilder : IBuilder
    {
        private IActiveProcessesService _activeProcessesService;
        private IPhillipsHueService _phillipsHueService;

        public OnAirServiceBuilder WithActiveProcessesService(IActiveProcessesService activeProcessesService) => this.With(ref _activeProcessesService, activeProcessesService);

        public OnAirServiceBuilder WithPhillipsHueService(IPhillipsHueService phillipsHueService) => this.With(ref _phillipsHueService, phillipsHueService);


        public OnAirService Build()
        {
            return new OnAirService(_activeProcessesService, _phillipsHueService);
        }

        public static implicit operator OnAirService(OnAirServiceBuilder builder) => builder.Build();
    }
}
