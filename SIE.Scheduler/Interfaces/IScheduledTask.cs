﻿using System.Threading;
using System.Threading.Tasks;

namespace SIE.Scheduler.Interfaces
{
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
