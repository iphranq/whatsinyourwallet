using System.Collections.Generic;


namespace CreditCardLibrary.Interfaces
{
    public interface IWallet
    {
        List<ICreditCard> CreditCards { get; set; }

        void AddCard(ICreditCard card);

        double CalculateTotalInterest();
    }
}
