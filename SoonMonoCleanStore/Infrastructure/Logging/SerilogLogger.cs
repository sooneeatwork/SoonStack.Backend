using Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logger
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger? _logger;
        public void LogError(string message, Exception ex)
        {
            _logger?.Error(ex, message);
        }

        public void LogInformation(string message)
        {
            _logger?.Information(message);
        }

        public void LogWarning(string message)
        {
            _logger?.Warning(message);
        }
    }
}
