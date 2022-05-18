using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessInterface;
public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
}
