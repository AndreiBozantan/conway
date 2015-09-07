using Microsoft.VisualStudio.TestTools.UnitTesting;
using Conway.DataModel;

namespace Tests
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void CellStates()
        {
            // test a new cell
            Cell cell = new Cell();
            Assert.AreEqual(cell.IsAlive, false, "new cell - IsAlive"); // not alive until commit
            Assert.AreEqual(cell.IsAliveOrNew, true, "new cell - IsAliveOrNew");
            Assert.AreEqual(cell.IsNew, true, "new cell - IsNew");

            // test a saved cell
            cell.Commit();
            Assert.AreEqual(cell.IsAlive, true, "alive cell - IsAlive");
            Assert.AreEqual(cell.IsAliveOrNew, true, "alive cell - IsAliveOrNew");
            Assert.AreEqual(cell.IsNew, false, "alive cell - IsNew");
        
            // test a killed cell
            cell.Kill();
            Assert.AreEqual(cell.IsAlive, true, "killed cell - IsAlive"); // cell still alive until commit
            Assert.AreEqual(cell.IsAliveOrNew, true, "killed cell - IsAliveOrNew");
            Assert.AreEqual(cell.IsNew, false, "killed cell - IsNew");

            // test a dead cell
            cell.Commit();
            Assert.AreEqual(cell.IsAlive, false, "dead cell - IsAlive");
            Assert.AreEqual(cell.IsAliveOrNew, false, "dead cell - IsAliveOrNew");
            Assert.AreEqual(cell.IsNew, false, "dead cell - IsNew");

            // test a resurected cell
            cell.Resurect();
            Assert.AreEqual(cell.IsAlive, false, "resurected cell - IsAlive"); // not alive until commit
            Assert.AreEqual(cell.IsAliveOrNew, true, "resurected cell - IsAliveOrNew");
            Assert.AreEqual(cell.IsNew, true, "resurected cell - IsNew");

            // test alive cell after resurection
            cell.Commit();
            Assert.AreEqual(cell.IsAlive, true, "second life - IsAlive");
            Assert.AreEqual(cell.IsAliveOrNew, true, "second life - IsAliveOrNew");
            Assert.AreEqual(cell.IsNew, false, "second life - IsNew");
        }
    }
}
