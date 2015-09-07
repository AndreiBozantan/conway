using System;

namespace TestData.Spaceship
{
    public class Glider
    {
        public static readonly string[] Cells =
        {
            ".....",
            "..O..",
            "...O.",
            ".OOO.",
            ".....",
        };
    }

    public class LWSS
    {
        public static readonly string[] Cells0 = 
        {
            ".O..O",
            "O....",
            "O...O",
            "OOOO."
        };

        public static readonly string[] Cells1 = 
        {
            "......",
            ".OO...",
            "OO.OO.",
            ".OOOO.",
            "..OO.."
        };

        public static readonly string[] Cells2 = 
        {
            "......",
            "OOOO..",
            "O...O.",
            "O.....",
            ".O..O."
        };

        public static readonly string[] Cells3 = 
        {
            "..OO...",
            ".OOOO..",
            "OO.OO..",
            ".OO....",
            "......."
        };

        public static readonly string[] Cells4 = 
        {
            ".O..O..",
            "O......",
            "O...O..",
            "OOOO...",
            "......."
        };

        public static readonly string String0 = String.Join(Environment.NewLine, Cells0);
        public static readonly string String1 = String.Join(Environment.NewLine, Cells1);
        public static readonly string String2 = String.Join(Environment.NewLine, Cells2);
        public static readonly string String3 = String.Join(Environment.NewLine, Cells3);
        public static readonly string String4 = String.Join(Environment.NewLine, Cells4);
    }


    public class HammerHead
    {
        public static readonly string[] Cells=
        {
            "OOOOO.............",
            "O....O.......OO...",
            "O...........OO.OOO",
            ".O.........OO.OOOO",
            "...OO...OO.OO..OO.",
            ".....O....O..O....",
            "......O.O.O.O.....",
            ".......O..........",
            ".......O..........",
            "......O.O.O.O.....",
            ".....O....O..O....",
            "...OO...OO.OO..OO.",
            ".O.........OO.OOOO",
            "O...........OO.OOO",
            "O....O.......OO...",
            "OOOOO.............",
        };

        public static readonly string[] RCells =
        {
            ".............OOOOO",
            "...OO.......O....O",
            "OOO.OO...........O",
            "OOOO.OO.........O.",
            ".OO..OO.OO...OO...",
            "....O..O....O.....",
            ".....O.O.O.O......",
            "..........O.......",
            "..........O.......",
            ".....O.O.O.O......",
            "....O..O....O.....",
            ".OO..OO.OO...OO...",
            "OOOO.OO.........O.",
            "OOO.OO...........O",
            "...OO.......O....O",
            ".............OOOOO",
        };
    }
}
