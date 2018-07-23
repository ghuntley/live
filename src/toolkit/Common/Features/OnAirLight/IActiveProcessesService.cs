using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Toolbox.Common.Features.OnAirLight
{
    public interface IActiveProcessesService
    {
        ReactiveList<string> ProcessNames { get; }
        ReactiveList<string> WindowTitles { get; }

    }
}
