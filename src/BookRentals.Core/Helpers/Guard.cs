using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Helpers
{
    public static class Guard
    {
        public static void ArgumentNotNull(object value, string nameOfValue = default)
        {
            if (value is null)
                throw new ArgumentException($"Value of '{nameOfValue ?? nameof(value)}' can't be NULL.");
        }

        public static void ArgumentNotNullOrEmpty(string value, string nameOfValue = default)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"Value of '{nameOfValue ?? nameof(value)}' can't be NULL or empty string.");
        }

        public static void ArgumentPositive(int value, string nameOfValue = default)
        {
            if (value <= 0)
                throw new ArgumentException($"Value of '{nameOfValue ?? nameof(value)}' must be a positive number greater than zero.");
        }

        public static void ArgumentNotNegative(int value, string nameOfValue = default)
        {
            if (value <= 0)
                throw new ArgumentException($"Value of '{nameOfValue ?? nameof(value)}' must be a positive number greater than zero.");
        }
    }
}
