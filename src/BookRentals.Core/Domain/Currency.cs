using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Domain
{
    public class Currency : Enumeration
    {
        public static Currency CZK = new Currency(1, "Česká koruna", nameof(CZK), "Kč");
        public static Currency EUR = new Currency(2, "Euro", nameof(EUR), "€");
        public static Currency USD = new Currency(3, "Americký dolar", nameof(USD), "$");
        public static Currency AUD = new Currency(4, "Austral. dolar", nameof(AUD), "A$");
        public static Currency GBP = new Currency(5, "Britská libra", nameof(GBP), "Ł");
        public static Currency CHF = new Currency(6, "Švýcar. frank", nameof(CHF), "CHF");

        public Currency(int id, string name, string code, string symbol) : base(id, name)
        {
            Code = code;
            Symbol = symbol;
        }

        public string Code { get; }
        public string Symbol { get; }
    }
}
