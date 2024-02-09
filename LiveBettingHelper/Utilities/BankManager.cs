using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;

namespace LiveBettingHelper.Utilities
{
    public partial class BankManager : ObservableObject
    {
        /// <summary>
        /// A user bankja
        /// </summary>
        public Bank MyBank { get; set; }
        private BaseRepository<Bank> _bankRepo { get; set; } = new();
        private BaseRepository<BankTransactionRecord> _bankTransactionRecordRepo { get; set; } = new();


        public BankManager()
        {
            LoadMyBank();
        }
        /// <summary>
        /// Pénz levétel a bankból
        /// </summary>
        public bool Withdraw(double amount)
        {
            if (amount > 0 && MyBank.Balance >= amount)
            {
                MyBank.Balance -= amount;
                Update();
                return true;
            }
            else
            {
                App.Logger.Error("An error occurred while trying to withdraw from bank.");
                return false; // Sikertelen tranzakció, nem elegendő fedezet
            }
        }
        /// <summary>
        /// Pénz feltöltés a bankba
        /// </summary>
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                MyBank.Balance += amount;
                Update();
            }
        }
        /// <summary>
        /// Banki tranzakció lementése az adatbázisba
        /// </summary>
        public void AddBankTransactionRecord(double changeAmount)
        {
            try
            {
                List<BankTransactionRecord> bankTransactionRecords = _bankTransactionRecordRepo.GetItems();
                double lastBalance = bankTransactionRecords.Count == 0 ? MyBank.Balance : bankTransactionRecords.OrderBy(x => x.Date).Last().BalanceAfterTransaction;
                _bankTransactionRecordRepo.AddItem(new BankTransactionRecord { BalanceAfterTransaction = lastBalance + changeAmount, ChangeAmount = changeAmount, Date = DateTime.Now });
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "An error occurred while trying to add banktransaction record.");
            }
        }
        /// <summary>
        /// Vissza adja az összes eddigi banki tranzakciót
        /// </summary>
        public List<BankTransactionRecord> GetRecords()
        {
            return _bankTransactionRecordRepo.GetItems().OrderBy(x => x.Date).ToList();
        }
        /// <summary>
        /// Közvetlenül frissíti a bank aktuális állását az adatbázisba
        /// </summary>
        public void Update()
        {
            _bankRepo.UpdateItem(MyBank);
        }
        /// <summary>
        /// Az alapértelmezett fogadási egység az egyenlegünkhöz visszonyítva
        /// </summary>
        public double CalculatedDefaultBetStake => Math.Round((MyBank.Balance / 20) / 10, 0) * 10; // a bank 5%-a kerek 10-esre kerekítve

        private void LoadMyBank()
        {
            List<Bank> banks = _bankRepo.GetItems();
            if (banks.Count > 1) App.Logger.Error("There cannot be more than 1 bank in the repository.");
            else if (banks.Count == 1) MyBank = banks[0];
            else
            {
                MyBank = new Bank();
                MyBank.Balance = 10000;
                _bankRepo.AddItem(MyBank);
            }
        }
    }
}
