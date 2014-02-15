﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helios.Util.Concurrency
{
    /// <summary>
    /// Internal factory class for spawning Task instances
    /// </summary>
    internal static class TaskRunner
    {
        public const int DefaultConcurrency = 0;

        public static TaskFactory GetTaskFactory(int concurrencyLevel = DefaultConcurrency)
        {
            if (concurrencyLevel <= DefaultConcurrency)
                return new TaskFactory(TaskScheduler.Default);
            return new TaskFactory(new FixedSizeThreadPoolTaskScheduler(concurrencyLevel));
        }

        public static Task Run(Action a)
        {
#if !NET35
            return Task.Run(a);
#else
            return Task.Factory.StartNew(a);
#endif

        }

        public static Task Run(Action a, CancellationToken c)
        {
#if !NET35
            return Task.Run(a, c);
#else
            return Task.Factory.StartNew(a, c);
#endif

        }

        public static Task<T> Run<T>(Func<T> f)
        {
#if !NET35
            return Task.Run(f);
#else
            return Task.Factory.StartNew(f);
#endif

        }

        public static Task<T> Run<T>(Func<T> f, CancellationToken c)
        {
#if !NET35
            return Task.Run(f, c);
#else
            return Task.Factory.StartNew(f,c);
#endif

        }
    }
}
