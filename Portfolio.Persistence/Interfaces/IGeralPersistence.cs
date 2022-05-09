using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Interfaces
{
    public interface IGeralPersistence
    {
        void Add<T> (T model) where T: class;
        void Update<T> (T model) where T: class;
        void Delete<T> (T model) where T: class;
        Task<bool> SaveChangesAsync();
    }
}
