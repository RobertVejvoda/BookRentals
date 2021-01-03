using System.ComponentModel;

namespace BookRentals.Core.Types
{
    public enum SysUnitEnum : int
    {
        Unknown = 0,

        [Description("kg")]
        Kg = 1,            /*kg*/

        [Description("ks")]
        Piece = 2,         /*ks*/

        [Description("pl.")]
        MetalSheet = 3,    /*pl.*/

        [Description("m")]
        Meter = 4,         /*m*/

        [Description("bm")]
        Bm = 5,            /*bm*/

        [Description("m2")]
        SquareMeter = 6,   /*m2*/

        [Description("m3")]
        CubicMeter = 7,    /*m3*/

        [Description("l")]
        Liter = 8,         /*l*/

        [Description("g")]
        Gram = 9,          /*g*/

        [Description("mm")]
        Milimeter = 10,    /*mm*/

        [Description("dm2")]
        SquareDecimeter = 11     /*dm2*/
    }
}
