using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Abstractions
{
    public interface IBaseRepository<T> : IDisposable where T : BaseModel, new()
    {
        void AddItem(T item);
        void AddItems(List<T> items);
        void UpdateItem(T item);
        T GetItem(int id);
        T GetItem(Expression<Func<T, bool>> predicate);
        List<T> GetItems();
        List<T> GetItems(Expression<Func<T, bool>> predicate);
        void DeleteItem(T item);
        void DeleteItems(List<T> items);
    }
}
