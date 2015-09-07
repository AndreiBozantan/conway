using Conway;
using System;
using System.Linq;

namespace ConsoleGame
{
    static class GameRenderer
    {
        public static void Render(this Game g)
        {
            for (int row = g.GridSize.Top; row <= g.GridSize.Bottom; row++)
            {
                for (int col = g.GridSize.Left; col <= g.GridSize.Right; col++)
                {
                    if (g[row, col].IsAlive)
                    {
                        Console.Write('■');
                    }
                    else
                    {
                        Console.Write('·');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game g1 = new Game();
            Game g2 = new Game();
            Game g3 = new Game();
            Game g4 = new Game();

            g1.InitGrid(TestData.StillLife.Pond.Cells);
            g2.InitGrid(TestData.Oscillator.Blinker.Cells1);
            g3.InitGrid(TestData.Spaceship.Glider.Cells);
            g4.InitGrid(TestData.Spaceship.HammerHead.RCells);

            while (true)
            {
                Console.Clear();

                //g1.Render();
                //g2.Render();
                //g3.Render();
                g4.Render();

                System.Threading.Thread.Sleep(500);

                //g1.Update();
                //g2.Update();
                //g3.Update();
                g4.Update();
            }
        }
    }
}
