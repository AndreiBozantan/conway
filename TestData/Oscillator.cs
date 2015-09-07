using System;

namespace TestData.Oscillator
{
    public class Blinker
    {
        public static readonly string[] Cells1 = 
        {
            ".O.",
            ".O.",
            ".O.",
        };

        public static readonly string[] Cells2 = 
        {
            "...",
            "OOO",
            "...",
        };

        public static readonly string String1 = String.Join(Environment.NewLine, Cells1);
        public static readonly string String2 = String.Join(Environment.NewLine, Cells2);
    }


    public class Beacon
    {

        public static readonly string[] Cells1 = 
        {
            "OO..",
            "O...",
            "...O",
            "..OO",
        };

        public static readonly string[] Cells2 = 
        {
            "OO..",
            "OO..",
            "..OO",
            "..OO",
        };

        public static readonly string String1 = String.Join(Environment.NewLine, Cells1);
        public static readonly string String2 = String.Join(Environment.NewLine, Cells2);
    }
}
