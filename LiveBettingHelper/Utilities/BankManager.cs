using CommunityToolkit.Mvvm.ComponentModel;
using LiveBettingHelper.Model;
using LiveBettingHelper.Repositories;

namespace LiveBettingHelper.Utilities
{
    public partial class BankManager : ObservableObject
    {
        public Bank MyBank { get; set; }
        private BaseRepository<Bank> BankRepo { get; set; } = new();


        public BankManager()
        {
            LoadMyBank();
        }

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

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                MyBank.Balance += amount;
                Update();
            }
        }

        public void Update()
        {
            BankRepo.UpdateItem(MyBank);
        }

        public double CalculatedDefaultBetStake => Math.Round((MyBank.Balance / 20) / 10, 0) * 10; // a bank 5%-a kerek 10-esre kerekítve

        private void LoadMyBank()
        {
            List<Bank> banks = BankRepo.GetItems();
            if (banks.Count > 1) App.Logger.Error("There cannot be more than 1 bank in the repository.");
            else if (banks.Count == 1) MyBank = banks[0];
            else
            {
                MyBank = new Bank();
                BankRepo.AddItem(MyBank);
            }
        }
    }
}
