using Genesis.Ensure;
using System;
using System.Collections.Generic;
using System.Text;
using Toolbox.Common.Features.OnAirLight;

namespace Toolbox.Common.Features.Dashboard
{
    public class DashboardViewModel
    {
        private IOnAirService _onAirService;

        public DashboardViewModel(IOnAirService onAirService)
        {
            Ensure.ArgumentNotNull(onAirService, nameof(onAirService));

            _onAirService = onAirService;
        }
    }
}
