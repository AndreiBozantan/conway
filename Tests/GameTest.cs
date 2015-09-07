using Microsoft.VisualStudio.TestTools.UnitTesting;
using Conway;

namespace Tests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameOscillator1()
        {
            Game g = new Game();

            g.InitGrid(TestData.Oscillator.Blinker.Cells1);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Oscillator.Blinker.String2);
            
            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Oscillator.Blinker.String1);
        }

        [TestMethod]
        public void GameOscillator2()
        {
            Game g = new Game();

            g.InitGrid(TestData.Oscillator.Beacon.Cells1);
            Assert.AreEqual(g.ToString(), TestData.Oscillator.Beacon.String1);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Oscillator.Beacon.String2);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Oscillator.Beacon.String1);
        }

        [TestMethod]
        public void GameStillLife1()
        {
            Game g = new Game();
            g.InitGrid(TestData.StillLife.Pond.Cells);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.StillLife.Pond.String);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.StillLife.Pond.String);
        }

        [TestMethod]
        public void GameSpaceShip1()
        {
            Game g = new Game();

            g.InitGrid(TestData.Spaceship.LWSS.Cells0);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Spaceship.LWSS.String1);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Spaceship.LWSS.String2);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Spaceship.LWSS.String3);

            g.Update();
            Assert.AreEqual(g.ToString(), TestData.Spaceship.LWSS.String4);
        }
    }
}
