using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public class SqlServerStore : IPasteStore
    {
        private readonly PasteContext _context;

        public SqlServerStore(PasteContext context)
        {
            _context = context;
        }

        public async Task<bool> Save(Paste paste)
        {
            // I know that this makes two database calls, but I think that this solution is a little clearer
            // and simpler than trying to unpack a database exception or setting up some SQL Server rules.
            if (await _context.Pastes.AnyAsync(p => p.Shortlink == paste.Shortlink))
            {
                return false;
            }

            paste.Created = DateTime.Now;
            _context.Pastes.Add(paste);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}