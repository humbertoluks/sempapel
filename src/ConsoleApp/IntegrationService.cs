using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Backup.Service.Helpers;
using Backup.Service.Helpers.Interfaces;
using ConsoleApp.Helpers;
using ConsoleApp.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using NLog;
using NLog.Config;

namespace ConsoleApp
{
    public class IntegrationService : IHostedService//, IDisposable
    {
        private static ILogger _logger;
        public IntegrationService()
        {
            SetUpNLog();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Application {applicationEvent} at {datetime}", "Started", DateTime.UtcNow);
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                
                var scheduler = await factory.GetScheduler();
                var serviceProvider = GetConfiguredServiceProvider();
                scheduler.JobFactory = new CustomJobFactory(serviceProvider);
                await scheduler.Start();
                await ConfigureDailyJob(scheduler);
            }
            catch (Exception ex)
            {
                _logger.Error(new CustomConfigurationException(ex.Message));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Application {applicationEvent} at {datetime}", "Ended", DateTime.UtcNow);
            return Task.CompletedTask;
        }

        private async Task ConfigureDailyJob(IScheduler scheduler)
        {
            var dailyJob = GetDailyJob();
            if (await scheduler.CheckExists(dailyJob.Key))
            {
                await scheduler.ResumeJob(dailyJob.Key);
                _logger.Info($"The job key {dailyJob.Key} was already existed, thus resuming the same");
            }
            else
            {
                await scheduler.ScheduleJob(dailyJob, GetDailyJobTrigger());
            }
        }
        private static async Task<IScheduler> GetScheduler()
        {
            var config = (NameValueCollection)ConfigurationManager.GetSection("quartz");
            var factory = new StdSchedulerFactory(config);

            var scheduler = await factory.GetScheduler();
            return scheduler;
        }

        private IServiceProvider GetConfiguredServiceProvider()
        {
            var services = new ServiceCollection()
                .AddScoped<IDailyJob, DailyJob>()
                // .AddScoped<IWeeklyJob, WeeklyJob>()
                // .AddScoped<IMonthlyJob, MonthlyJob>()
                .AddScoped<IHelperService, HelperService>();
            return services.BuildServiceProvider();
        }
        private IJobDetail GetDailyJob()
        {
            return JobBuilder.Create<IDailyJob>()
                .WithIdentity("dailyjob", "dailygroup")
                .Build();
        }

        private ITrigger GetDailyJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("dailytrigger", "dailygroup")
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInHours(24)
                     .RepeatForever())
                 .Build();
        }

        private void SetUpNLog()
        {
            var config = new LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "backupclientlogfile.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;

            _logger = NLog.LogManager.GetCurrentClassLogger();
        }
    }
}