using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Repository
{
    internal interface IRepository<T>
    {
        List<T> GetAll();
        int insert(T value);
        int update(int id, T value);
    }
}
