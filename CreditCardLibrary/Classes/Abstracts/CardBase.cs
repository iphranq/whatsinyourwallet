using CreditCardLibrary.Interfaces;


namespace CreditCardLibrary.Classes.Abstracts
{
    /// <summary>
    /// Base class is define to provide the common function
    /// for each card. It is closed for modification but
    /// open for extension.
    /// If, in the future, any business rules
    /// change for calculating interest, the derived class
    /// can override and put in the new rule(s).
    /// </summary>
    public abstract class CardBase : ICreditCard
    {
        #region Constructors

        /// <summary>
        /// All credit cards require a CreditCardType to be defined
        /// when they are instantiated. 
        /// </summary>
        /// <param name="cardType"></param>
        public CardBase(CreditCardType cardType) 
        {
            CardType = cardType;
        }

        /// <summary>
        /// Interest rate can be preloaded at instantiation.
        /// </summary>
        /// <param name="interestRate"></param>
        public CardBase(int interestRate)
        {
            _interestRate = interestRate;
        }

        /// <summary>
        /// Balance can be preloaded at instantiation.
        /// </summary>
        /// <param name="interestRate"></param>
        /// <param name="balance"></param>
        public CardBase(int interestRate, double balance)
            : this(interestRate)
        {
            _balance = balance;
        }

        #endregion

        #region Fields

        protected int _interestRate;
        protected double _balance;

        #endregion

        #region Properties

        public CreditCardType CardType { get; protected set; }
        public int InterestRate { 
            get { return _interestRate; } 
            set { _interestRate = value; } 
        }

        public double Balance { 
            get { return _balance; } 
            set { _balance = value; } 
        }

        /// <summary>
        /// The string name of the card.
        /// </summary>
        public string CardName { get { return CardType.ToString(); } }

        #endregion

        #region Methods

        public virtual double GetMonthlyInterestStatement()
        {
            return CalculateInterestByNumberOfMonths(1);
        }

        /// <summary>
        /// This base is a simple interest calculator. If a derived class has a new
        /// rule, it can override and apply any new rules.
        /// </summary>
        /// <param name="noOfMonths"></param>
        /// <returns></returns>
        protected virtual double CalculateInterestByNumberOfMonths(int numberOfMonths)
        {
            return ((_balance * (_interestRate * 0.01)) * numberOfMonths);
        }

        #endregion
    }

}
