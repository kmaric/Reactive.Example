using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Hosting;
using Reactive.Example.Common.Interfaces.DAL;

namespace Reactive.Example.Processor.Services
{
    public class TaskRunnerServiceAlternative: IHostedService
    {
        private readonly ITestRepository _testRepository;
        private System.Timers.Timer _timer;
        public TaskRunnerServiceAlternative(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting task runner...");
            _timer = new System.Timers.Timer(10);
            _timer.Elapsed += async (sender, args) => await OnTimerElapsed(sender, args); 
            _timer.Enabled = true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping task runner...");
            return Task.CompletedTask;
        }

        private async Task OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            var x = new Random().Next();
            var y = new Random().Next();
//            await _testRepository.Insert(x, y);

            await _testRepository.GetTopCatalogues();
        }
    }
}