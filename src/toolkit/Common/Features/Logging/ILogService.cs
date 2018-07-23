using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toolbox.Common.Features.Logging
{
    public interface ILogService
    {
        /// <summary>
        /// Gets an observable that ticks a <see cref="LogEntry"/> whenever a new log entry comes in.
        /// </summary>
        IObservable<LogEvent> Entries
        {
            get;
        }

    }
}
