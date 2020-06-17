

namespace CreditCardLibrary.Interfaces
{
    public interface ICreditCard
    {
        CreditCardType CardType { get; }
        int InterestRate { get; set; }
        double Balance { get; set; }
        string CardName { get; }

        double GetMonthlyInterestStatement();
    }
}
