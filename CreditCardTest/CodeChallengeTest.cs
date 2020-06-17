using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditCardLibrary.Classes;
using CreditCardLibrary.Interfaces;
using System;
using System.Collections.Generic;


namespace CreditCardTest
{
    [TestClass]
    public class CodeChallengeTest
    {
        private CreditCard _visa;
        private CreditCard _masterCard;
        private CreditCard _discover;

        [TestInitialize]
        public void Initialize()
        {
            //  Initializing global variables for this test since each card has a prescribed interest rate
            //  and a prescribed balance and this doesn't change in this iteration of testing.
            _visa = new CreditCard(cardType: CreditCardType.Visa, interestRate: 10, balance: 100.00);
            _masterCard = new CreditCard(cardType: CreditCardType.MasterCard, interestRate: 5, balance: 100.00);
            _discover = new CreditCard(cardType: CreditCardType.Discover, interestRate: 1, balance: 100.00);
        }

        [TestMethod]
        public void FirstTest()
        {
            var person = new Person("First", "Person");

            var wallet = new Wallet();
            wallet.AddCard(_visa);
            wallet.AddCard(_masterCard);
            wallet.AddCard(_discover);

            person.AddWallet(wallet);

            double totalInterest = 0.00;
            //  Sum the total interest per card per wallet.
            person.Wallets.ForEach(ww => totalInterest += ww.CalculateTotalInterest());

            Console.WriteLine($"Total interest for {person.FullName} is {totalInterest.ToString("C")}\n");

            var aggregateCardInterest = (_discover.GetMonthlyInterestStatement() +
                _masterCard.GetMonthlyInterestStatement() + _visa.GetMonthlyInterestStatement());
            Assert.IsTrue(totalInterest == aggregateCardInterest);

            person.Wallets.ForEach(ww => 
                ww.CreditCards.ForEach(cc => 
                {
                    //  Testing the expected interest return values per card.
                    switch (cc.CardType)
                    {
                        case CreditCardType.Discover:
                            Assert.AreEqual(cc.GetMonthlyInterestStatement(), _discover.GetMonthlyInterestStatement());
                            break;

                        case CreditCardType.MasterCard:
                            Assert.AreEqual(cc.GetMonthlyInterestStatement(), _masterCard.GetMonthlyInterestStatement());
                            break;

                        case CreditCardType.Visa:
                            Assert.AreEqual(cc.GetMonthlyInterestStatement(), _visa.GetMonthlyInterestStatement());
                            break;
                    }

                    Console.WriteLine($"{cc.CardName} has {cc.GetMonthlyInterestStatement().ToString("C")}\n");
                }));
        }

        [TestMethod]
        public void SecondTest()
        {
            var person = new Person("Second", "Person");

            var wallet1 = new Wallet();
            wallet1.AddCard(_visa);
            wallet1.AddCard(_discover);

            person.AddWallet(wallet1);

            var wallet2 = new Wallet();
            wallet2.AddCard(_masterCard);

            person.AddWallet(wallet2);

            //  Spec:
            //      1 Person, 2 wallets
            //          Wallet 1 - 1 Visa, 1 Discover
            //          Wallet 2 - 1 MC
            var totalPersonInterest = (_visa.GetMonthlyInterestStatement() + _discover.GetMonthlyInterestStatement() + 
                _masterCard.GetMonthlyInterestStatement());

            Assert.IsTrue(person.CalculateTotalWalletInterest() == totalPersonInterest,
                $"Total interest of both wallets for this person should be {totalPersonInterest.ToString("C")}");

            int walletCount = 0;
            person.Wallets.ForEach(ww => {

                walletCount++;

                //  Test total interest per wallet.
                if(walletCount == 1) Assert.IsTrue(ww.CalculateTotalInterest() == (_visa.GetMonthlyInterestStatement() + _discover.GetMonthlyInterestStatement()), 
                    $"Total interest in wallet 1 should be {(_visa.GetMonthlyInterestStatement() + _discover.GetMonthlyInterestStatement()).ToString("C")}");

                else Assert.IsTrue(ww.CalculateTotalInterest() == _masterCard.GetMonthlyInterestStatement(), 
                    $"Total interest in wallet 2 should be {_masterCard.GetMonthlyInterestStatement().ToString("C")}");

                ww.CreditCards.ForEach(cc =>
                {
                    switch (cc.CardType)
                    {
                        case CreditCardType.Discover:
                            Assert.IsTrue(walletCount == 1, 
                                "Making sure the Discover card is in wallet 1");
                            break;

                        case CreditCardType.MasterCard:
                            Assert.IsTrue(walletCount == 2,
                                "Making sure the MasterCard is in wallet 2");
                            break;

                        case CreditCardType.Visa:
                            Assert.IsTrue(walletCount == 1,
                                "Making sure the Visa is in wallet 1");
                            break;
                    }
                });
              }
            );
        }

        [TestMethod]
        public void ThirdTest()
        {
            var personOne = new Person("Person", "One");
            var wallet1 = new Wallet();

            /*
             * Spec says: "person 1 has 1 wallet , with 2 cards MC and visa".
             * I am interpreting this to be 2 cards MC and 1 card Visa since
             * the implied Visa count appears to be one though the spec for person
             * two explicitly states only one wallet and 1 MC and 1 Visa in the wallet. 
             * I would normally ask for clarification, though.          */
            wallet1.AddCard(_masterCard);
            wallet1.AddCard(_masterCard);

            wallet1.AddCard(_visa);

            personOne.AddWallet(wallet1);

            var personTwo = new Person("Person", "Two");
            var wallet2 = new Wallet();

            wallet2.AddCard(_visa);
            wallet2.AddCard(_masterCard);

            personTwo.AddWallet(wallet2);

            List<IPerson> people = new List<IPerson> { personOne, personTwo };

            int personCount = 0;
            foreach (var personItem in people)
            {
                personCount++;
                double totalInterestPerPerson = 0.00;

                int walletCount = 0;
                personItem.Wallets.ForEach(ww => {

                    walletCount++;
                    var totalPerWallet = ww.CalculateTotalInterest();

                    double cardSum = 0.00;
                    if (personCount == 1 && walletCount == 1) 
                    {
                        cardSum = (_masterCard.GetMonthlyInterestStatement() * 2) + _visa.GetMonthlyInterestStatement();
                    }
                    else if (personCount == 2 && walletCount == 1)
                    {
                        cardSum = _masterCard.GetMonthlyInterestStatement() + _visa.GetMonthlyInterestStatement();
                    }
                    Assert.IsTrue(totalPerWallet == cardSum, $"Total interest in wallet {walletCount} is {cardSum}");

                    totalInterestPerPerson += totalPerWallet; 
                });

                if (personCount == 1)
                {
                    Assert.IsTrue(totalInterestPerPerson == (_masterCard.GetMonthlyInterestStatement() * 2) + _visa.GetMonthlyInterestStatement(), 
                        $"Total interest in wallet {walletCount} is {((_masterCard.GetMonthlyInterestStatement() * 2) + _visa.GetMonthlyInterestStatement()).ToString("C")}");
                }
                else
                {
                    Assert.IsTrue(totalInterestPerPerson == _masterCard.GetMonthlyInterestStatement() + _visa.GetMonthlyInterestStatement(),
                        $"Total interest in wallet {walletCount} is {(_masterCard.GetMonthlyInterestStatement() + _visa.GetMonthlyInterestStatement()).ToString("C")}");
                }
            }
        }
    }
}
