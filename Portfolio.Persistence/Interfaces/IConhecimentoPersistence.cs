using Portfolio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Interfaces
{
    public interface IConhecimentoPersistence
    {
        Task<Conhecimento> GetConhecimentoAsync(int id);
        Task<bool?> EditConhecimentoAsync(int id, Conhecimento model);
    }
}
