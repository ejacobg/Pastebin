using System;
using System.Threading.Tasks;
using Shortener.Data;

namespace Shortener.Models
{
    public interface IPasteStore
    {
        public Task<bool> Save(Paste paste);
    }


}