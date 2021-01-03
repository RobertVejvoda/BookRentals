using BookRentals.Core.Types;
using System;


namespace BookRentals.Core.Domain
{
    public readonly struct Quantity : IEquatable<Quantity>
    {
        public Quantity(decimal amount, SysUnitEnum unit)
        {
            Amount = amount;
            Unit = unit;
        }

        public decimal Amount { get; }
        public SysUnitEnum Unit { get; }

        public int GetPieces()
        {
            if (Unit != SysUnitEnum.Piece)
                throw new InvalidOperationException("Množství není v kusech.");

            return (int)Amount;
        }

        public int GetNonNegativePieces()
        {
            var pieces = GetPieces();
            EnsureNotNegative();
            return pieces;
        }

        public void EnsureNotNegative()
        {
            if (IsNegative())
                throw new InvalidOperationException("Množství je záporné.");
        }

        public Quantity Negate()
        {
            return new Quantity(-Amount, Unit);
        }

        public Quantity Reset()
        {
            return new Quantity(default, Unit);
        }

        public bool IsNegative()
        {
            return Amount < 0;
        }

        public bool IsPositive()
        {
            return Amount > 0;
        }

        public bool IsZero()
        {
            return Amount == 0;
        }

        public Quantity Abs()
        {
            return new Quantity(Math.Abs(Amount), Unit);
        }

        public override string ToString()
        {
            return $"{Amount} {Abbreviation}";
        }

        public string Abbreviation
        {
            get
            {
                switch (Unit)   // maybe it would be better to use a static class EnumHelper and the method GetEnumDescription
                {
                    case SysUnitEnum.Bm: return "bm";
                    case SysUnitEnum.CubicMeter: return "m3";
                    case SysUnitEnum.Gram: return "g";
                    case SysUnitEnum.Kg: return "kg";
                    case SysUnitEnum.Liter: return "l";
                    case SysUnitEnum.MetalSheet: return "pl.";
                    case SysUnitEnum.Meter: return "m";
                    case SysUnitEnum.Milimeter: return "mm";
                    case SysUnitEnum.Piece: return "ks";
                    case SysUnitEnum.SquareMeter: return "m2";
                    case SysUnitEnum.SquareDecimeter: return "dm2";
                    default:
                        return "";
                }
            }
        }

        private int GetDefaultRoundingDigits(int? roundDigits)
        {
            switch (Unit)
            {
                case SysUnitEnum.Piece:
                case SysUnitEnum.MetalSheet:
                case SysUnitEnum.Gram:
                case SysUnitEnum.Milimeter:
                case SysUnitEnum.SquareDecimeter:
                    return roundDigits ?? 0;
                case SysUnitEnum.Kg:
                case SysUnitEnum.Liter:
                    return roundDigits ?? 2;
                default:
                    return roundDigits ?? 4;
            }
        }

        public static Quantity Zero => new Quantity(0, SysUnitEnum.Piece);

        public static Quantity One => new Quantity(1, SysUnitEnum.Piece);

        /// <summary>
        /// Returns a new instance of Quantity with the same unit as the current one, but with a specified amount.
        /// Defaults to 0.
        /// </summary>
        public Quantity OfSameUnit(decimal amount = 0) => new Quantity(amount, Unit);

        public static Quantity Kg(decimal amount, int? roundDigits = default)
        {
            return new Quantity(amount, SysUnitEnum.Kg).Round(roundDigits);
        }

        public static Quantity Pieces(int amount)
        {
            return new Quantity(amount, SysUnitEnum.Piece);
        }

        public static Quantity Pieces(decimal amount)
        {
            return new Quantity((int)amount, SysUnitEnum.Piece);
        }

        public static Quantity SquareMeters(decimal amount, int roundDigits = 4)
        {
            return new Quantity(amount, SysUnitEnum.SquareMeter).Round(roundDigits);
        }

        public static Quantity CubicMeters(decimal amount, int? roundDigits = default)
        {
            return new Quantity(amount, SysUnitEnum.CubicMeter).Round(roundDigits);
        }

        public static Quantity Liters(decimal amount, int? roundDigits = default)
        {
            return new Quantity(amount, SysUnitEnum.Liter).Round(roundDigits);
        }

        public static Quantity Milimeters(decimal amount, int? roundDigits = default)
        {
            return new Quantity(amount, SysUnitEnum.Milimeter).Round(roundDigits);
        }
        public static Quantity Meters(decimal amount, int? roundDigits = default)
        {
            return new Quantity(amount, SysUnitEnum.Meter).Round(roundDigits);
        }

        public static Quantity Grams(decimal amount, int? roundDigits = default)
        {
            if (roundDigits.HasValue) amount = Math.Round(amount, roundDigits.Value, MidpointRounding.AwayFromZero);
            return new Quantity(amount, SysUnitEnum.Gram);
        }

        public static Quantity Bms(decimal amount, int? roundDigits = default)
        {
            return new Quantity(amount, SysUnitEnum.Bm).Round(roundDigits);
        }

        public static Quantity MetalSheets(int amount)
        {
            return new Quantity(amount, SysUnitEnum.MetalSheet);
        }

        public static Quantity SquareDecimeters(decimal amount, int? roundDigits = default)
        {
            return new Quantity(amount, SysUnitEnum.SquareDecimeter).Round(roundDigits);
        }

        public Quantity Round(int? digits = default)
        {
            return new Quantity(Math.Round(Amount, GetDefaultRoundingDigits(digits), MidpointRounding.AwayFromZero), Unit);
        }

        public Quantity Add(decimal amount)
        {
            return new Quantity(decimal.Add(Amount, amount), Unit).Round();
        }

        public Quantity Subtract(decimal amount)
        {
            return new Quantity(decimal.Subtract(Amount, amount), Unit).Round();
        }

        public Quantity Multiply(decimal amount)
        {
            return new Quantity(decimal.Multiply(Amount, amount), Unit).Round();
        }

        public Quantity Divide(decimal amount)
        {
            if (amount == 0) return new Quantity(1, Unit);
            return new Quantity(decimal.Divide(Amount, amount), Unit).Round();
        }

        public bool Equals(Quantity other)
        {
            return Unit == other.Unit && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            if (obj is Quantity other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Amount, Unit).GetHashCode();
        }

        public void EnsureUnit(Quantity other)
        {
            if (Unit != other.Unit)
                throw new ArgumentException("Jednotky množství musí být stejné.");
        }

        public static Quantity operator -(Quantity x, Quantity y)
        {
            if (x.Unit != y.Unit)
                throw new ArgumentException("Jednotky odečítaných množství musí být stejné.");

            return new Quantity(x.Amount - y.Amount, y.Unit).Round();
        }

        public static Quantity operator +(Quantity x, Quantity y)
        {
            if (x.Unit != y.Unit)
                throw new ArgumentException("Jednotky sčítaných množství musí být stejné.");

            return new Quantity(x.Amount + y.Amount, x.Unit).Round();
        }

        public static Quantity operator /(Quantity x, Quantity y)
        {
            if (x.Unit != y.Unit)
                throw new ArgumentException("Jednotky dělených množství musí být stejné.");
            return new Quantity(x.Amount / y.Amount, x.Unit).Round();
        }

        public static Quantity operator *(Quantity x, decimal y)
        {
            return new Quantity(x.Amount * y, x.Unit).Round();
        }

        public static bool operator ==(Quantity x, Quantity y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Quantity x, Quantity y)
        {
            return !(x == y);
        }

        public static bool operator <(Quantity x, Quantity y)
        {
            if (x.Unit != y.Unit)
                throw new ArgumentException("Jednotky porovnávaných množství musí být stejné.");

            return x.Amount < y.Amount;
        }

        public static bool operator <=(Quantity x, Quantity y)
        {
            if (x.Unit != y.Unit)
                throw new ArgumentException("Jednotky porovnávaných množství musí být stejné.");

            return x.Amount <= y.Amount;
        }

        public static bool operator >(Quantity x, Quantity y)
        {
            if (x.Unit != y.Unit)
                throw new ArgumentException("Jednotky porovnávaných množství musí být stejné.");

            return x.Amount > y.Amount;
        }

        public static bool operator >=(Quantity x, Quantity y)
        {
            if (x.Unit != y.Unit)
                throw new ArgumentException("Jednotky porovnávaných množství musí být stejné.");

            return x.Amount >= y.Amount;
        }
    }
}
