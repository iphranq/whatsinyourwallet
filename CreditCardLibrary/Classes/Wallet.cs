using CreditCardLibrary.Interfaces;
using System.Collections.Generic;


namespace CreditCardLibrary.Classes
{
    public class Wallet : IWallet
    {
        #region Constructors

        public Wallet() 
        {
            CreditCards = new List<ICreditCard>();
        }

        #endregion

        #region Properties

        public List<ICreditCard> CreditCards { get; set; }

        #endregion

        #region Methods

        public void AddCard(ICreditCard card)
        {
            CreditCards.Add(card);
        }

        public double CalculateTotalInterest()
        {
            double result = 0.00;

            CreditCards.ForEach(cc => result += cc.GetMonthlyInterestStatement());

            return result;
        }

        #endregion
    }
}
