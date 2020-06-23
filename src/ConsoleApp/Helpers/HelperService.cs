using System;
using System.Threading.Tasks;
using ConsoleApp.Helpers;
using ConsoleApp.Helpers.Interfaces;
using NLog;
using Repository.Interfaces;
public class HelperService : IHelperService
{
    private static ILogger _logger;
    private readonly IGuiaRepository _guiaRepository;
    public HelperService(IGuiaRepository guiaRepository)
    {
        SetUpNLog();
        _guiaRepository = guiaRepository;
    }

    public async Task PerformService(string schedule)
    {
        try
            {
                _logger.Info($"{DateTime.Now}: The PerformService() is called with {schedule} schedule");
            
                if (!string.IsNullOrWhiteSpace(schedule))
                {
                    var guias = await _guiaRepository.GetAsync();
                    //await UploadToAzureBlobStorage(schedule, path, fileName);
                    _logger.Info($"{DateTime.Now}: The PerformService() is finished with {schedule} schedule");
                }
            }
            catch (Exception ex)
            {
                _logger.Info($"{DateTime.Now}: Exception is occured at PerformService(): {ex.Message}");
                throw new CustomConfigurationException(ex.Message);
            }
    }

    private void SetUpNLog()
    {
        var config = new NLog.Config.LoggingConfiguration();

        // Targets where to log to: File and Console
        var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "backupclientlogfile_helperservice.txt" };
        var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

        // Rules for mapping loggers to targets            
        config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
        config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);

        // Apply config           
        LogManager.Configuration = config;

        _logger = LogManager.GetCurrentClassLogger();
    }
}