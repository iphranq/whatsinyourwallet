using CreditCardLibrary.Interfaces;
using System.Collections.Generic;


namespace CreditCardLibrary.Classes
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }

        string FullName { get; }

        List<IWallet> Wallets { get; set; }

        void AddWallet(IWallet wallet);

        double CalculateTotalWalletInterest();
    }
}
