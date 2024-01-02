using LiveBettingHelper.Abstractions;
using LiveBettingHelper.Utilities;
using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace LiveBettingHelper.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel, new()
    {
        /// <summary>
        /// Az SQLite adatbázis kapcsolat
        /// </summary>
        protected SQLiteConnection _conn;
        /// <summary>
        /// Konstruktor
        /// </summary>
        public BaseRepository()
        {
            _conn = new SQLiteConnection(Static.DatabasePath, Static.DBFlags);
            _conn.CreateTable<T>();
        }
        /// <summary>
        /// Hozzá adja az adatbázishozz a megadott objektumot
        /// </summary>
        public void AddItem(T item)
        {
            try
            {
                _conn.Insert(item);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
        }
        /// <summary>
        /// Hozzá adja az adatbázishozz a megadott objektumot
        /// </summary>
        public void AddItems(List<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    _conn.Insert(item);
                }
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
        }
        /// <summary>
        /// Törli a megadott objektumot
        /// </summary>
        public void DeleteItem(T item)
        {
            try
            {
                _conn.Delete(item);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
        }
        /// <summary>
        /// Törli a megadott objektumokat
        /// </summary>
        public void DeleteItems(List<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    _conn.Delete(item);
                }
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
        }
        /// <summary>
        /// Lezárja az SQLite kapcsolatot
        /// </summary>
        public void Dispose()
        {
            _conn.Close();
        }
        /// <summary>
        /// Vissza add egy objektumot id alapján
        /// </summary>
        /// <returns>
        /// Ha nincs ilyen null-t add vissza
        /// </returns>
        public T GetItem(int id)
        {
            try
            {
                return _conn.Table<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
            return null;
        }
        /// <summary>
        /// Vissza addja az első olyan objektumot ami megfelel a predikátumnak 
        /// </summary>
        /// <returns>
        /// Ha nincs ilyen null-t add vissza
        /// </returns>
        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _conn.Table<T>().Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
            return null;
        }
        /// <summary>
        /// Vissza addja az összes objektumot
        /// </summary>
        /// <returns>
        /// Ha nincs ilyen üres listávval tér vissza
        /// </returns>
        public List<T> GetItems()
        {
            try
            {
                return _conn.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
            return new List<T>();
        }
        /// <summary>
        /// Vissza addja az összes objektumot
        /// </summary>
        /// <returns>
        /// Ha nincs ilyen üres listávval tér vissza
        /// </returns>
        public List<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _conn.Table<T>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
            return new List<T>();
        }
        /// <summary>
        /// Frissíti a megadott objektumot
        /// </summary>
        public void UpdateItem(T item)
        {
            try
            {
                _conn.Update(item);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
        }
        /// <summary>
        /// Frissíti a megadott objektumokat
        /// </summary>
        public void UpdateItems(List<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    _conn.Update(item);
                }
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Exception in {typeof(T)}  - {MethodBase.GetCurrentMethod()}: ");
            }
        }
    }
}
