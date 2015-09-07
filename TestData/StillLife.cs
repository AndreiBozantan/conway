using System;

namespace TestData.StillLife
{
    public class Pond
    {
        public static readonly string[] Cells = 
        {
            ".OO.",
            "O..O",
            "O..O",
            ".OO."
        };

        public static readonly string String = String.Join(Environment.NewLine, Cells);
    }
}