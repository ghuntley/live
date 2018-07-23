using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Genesis.Ensure;
using Serilog;

namespace System
{
    public static class SubscribeSafeExtension
    {
        public static IDisposable SubscribeSafe<T>(
            this IObservable<T> @this,
            [CallerMemberName]string callerMemberName = null,
            [CallerFilePath]string callerFilePath = null,
            [CallerLineNumber]int callerLineNumber = 0)
        {
            Ensure.ArgumentNotNull(@this, nameof(@this));

            return @this
                .Subscribe(
                    _ => { },
                    ex => {
                        Log.Logger.Error(ex, "An exception went unhandled. Caller member name: {CallerMemberName}, caller file path: {CallerFilePath}, caller line number: {CallerLineNumber}", callerMemberName, callerFilePath, callerLineNumber);

                        Debugger.Break();
                    });
        }
    }
}