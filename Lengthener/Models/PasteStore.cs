using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shortener.Data;

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
}