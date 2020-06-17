using CreditCardLibrary.Interfaces;
using System;
using System.Collections.Generic;


namespace CreditCardLibrary.Classes
{
    public class Person : IPerson
    {
        #region Constructors

        public Person() 
        {
            Wallets = new List<IWallet>();
        }

        public Person(string firstName, string lastName)
            : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        #endregion

        #region Methods

        public List<IWallet> Wallets { get; set; }


        public void AddWallet(IWallet wallet)
        {
            Wallets.Add(wallet);
        }

        public double CalculateTotalWalletInterest()
        {
            double result = 0.00;

            foreach(var wallet in Wallets)
            {
                wallet.CreditCards.ForEach(cc => result += cc.GetMonthlyInterestStatement());
            }

            return result;
        }

        #endregion
    }
}
