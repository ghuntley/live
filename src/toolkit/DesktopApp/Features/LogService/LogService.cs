using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Serilog.Events;
using Toolbox.Common.Features.Logging;

namespace DesktopApp.Features.Logging
{
    public class LogService : ILogService
    {
        public IObservable<LogEvent> Entries => throw new NotImplementedException();
    }
}
