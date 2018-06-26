using System;

namespace SIE.Auxiliary
{
    public static class ColorExtensions
    {
        public static string RandomColorHex() => $"#{new Random().Next(0x1000000):X6}";
    }
}
