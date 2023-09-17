using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shortener.Data;

namespace Shortener.Models
{
    public interface IPasteStore
    {
        public Task<bool> Save(Paste paste);
    }

    public class ConsoleStore : IPasteStore
    {
        private readonly ILogger<ConsoleStore> _logger;

        public ConsoleStore(ILogger<ConsoleStore> logger)
        {
            _logger = logger;
        }

        public Task<bool> Save(Paste paste)
        {
            _logger.LogInformation($"Saved shortlink '{paste.Shortlink}' with content '{paste.Content}'");
            return Task.FromResult(true);
        }
    }
}