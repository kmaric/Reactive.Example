using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Reactive.Example.Common.Interfaces.DAL;

namespace Reactive.Example.Processor.Services
{
    public class TaskRunnerService: IHostedService
    {
        private readonly ITestRepository _testRepository;

        public TaskRunnerService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting task runner...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping task runner...");
            return Task.CompletedTask;
        }
    }
}