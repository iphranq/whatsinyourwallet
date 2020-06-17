using CreditCardLibrary.Classes.Abstracts;
using CreditCardLibrary.Interfaces;


namespace CreditCardLibrary.Classes
{
    public class CreditCard : CardBase
    {
        #region Constructors

        public CreditCard(CreditCardType cardType)
            : base(cardType)
        {
        }

        public CreditCard(CreditCardType cardType, int interestRate)
            : base(interestRate)
        {
            CardType = cardType;
        }

        public CreditCard(CreditCardType cardType, int interestRate, double balance)
            : base(interestRate, balance)
        {
            CardType = cardType;
        }

        #endregion
    }
}
