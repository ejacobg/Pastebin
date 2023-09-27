using System;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Lengthener.Models
{
    public interface IPasteStore
    {
        public Task<Paste> GetByShortlink(string shortlink);
    }

    public class SqlServerStore : IPasteStore
    {
        private readonly PasteContext _context;

        public SqlServerStore(PasteContext context)
        {
            _context = context;
        }

        public async Task<Paste> GetByShortlink(string shortlink)
        {
            return await _context.Pastes
                .Where(paste => paste.Shortlink == shortlink)
                .SingleOrDefaultAsync();
        }
    }

    public class EchoStore : IPasteStore
    {
        public Task<Paste> GetByShortlink(string shortlink)
        {
            return Task.FromResult(new Paste
            {
                Content = shortlink,
                Created = DateTime.Now,
                Expires = 60,
                PasteId = 1,
                Shortlink = shortlink
            });
        }
    }
}