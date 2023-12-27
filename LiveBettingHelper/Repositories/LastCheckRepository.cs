using LiveBettingHelper.Model;
using LiveBettingHelper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Repositories
{
    public class LastCheckRepository : BaseRepository<LastCheck>
    {
        /// <summary>
        /// Lement egy megadott tipusú LastCheck-et, és törli az előző ilyen tipusút, hogy mindig csak 1 legyen 1 féléből
        /// </summary>
        public void SetLastCheck(CheckType checkType)
        {
            try
            {
                DeleteItems(GetItems(x => x.checkType == checkType));
                _conn.Insert(new LastCheck { checkType = checkType, checkDate = DateTime.Now });
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Error in {nameof(LastCheck)} - {MethodBase.GetCurrentMethod()}: ");
            }
        }
        /// <summary>
        /// Vissza addja az megadott tipusú LastCheck-et
        /// </summary>
        /// <returns>
        /// Ha nincs ilyen null-t add vissza
        /// </returns>
        public LastCheck GetLastCheck(CheckType checkType)
        {
            try
            {
                return GetItem(x => x.checkType == checkType);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, $"Error in {nameof(LastCheck)} - {MethodBase.GetCurrentMethod()}: ");
                return null;
            }
        }
    }
}
