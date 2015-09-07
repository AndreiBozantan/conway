using Microsoft.VisualStudio.TestTools.UnitTesting;
using Conway.DataModel;

namespace Tests
{
    [TestClass]
    public class GridSizeTest
    {
        [TestMethod]
        public void GridSizeEmpty()
        {
            int count;

            GridSize s = new GridSize();
            
            count = 0;
            for (var i = s.Left; i <= s.Right; i++, count++) ;
            Assert.AreEqual(count, 0, "empty size 1");

            count = 0;
            for (var i = s.Left; i <= s.Right + 1; i++, count++) ;
            Assert.AreEqual(count, 0, "empty size 2");

            count = 0;
            for (var i = s.Top; i <= s.Bottom; i++, count++) ;
            Assert.AreEqual(count, 0, "empty size 3");

            count = 0;
            for (var i = s.Top; i <= s.Bottom + 1; i++, count++) ;
            Assert.AreEqual(count, 0, "empty size 4");

            Assert.AreEqual(s.Area, 0, "empty area");
        }


        [TestMethod]
        public void GridSizeWithData()
        {
            GridSize s = new GridSize();

            s.IncludePosition(new MatrixPosition(1, 1));
            Assert.AreEqual(s.Left, 1, "test 1 left");
            Assert.AreEqual(s.Right, 1, "test 1 right");
            Assert.AreEqual(s.Top, 1, "test 1 top");
            Assert.AreEqual(s.Bottom, 1, "test 1 bottom");
            Assert.AreEqual(s.Area, 1, "test 1 area");

            s.IncludePosition(new MatrixPosition(1, 2));
            Assert.AreEqual(s.Left, 1, "test 2 left");
            Assert.AreEqual(s.Right, 2, "test 2 right");
            Assert.AreEqual(s.Top, 1, "test 2 top");
            Assert.AreEqual(s.Bottom, 1, "test 2 bottom");
            Assert.AreEqual(s.Area, 2, "test 2 area");

            s.IncludePosition(new MatrixPosition(0, 1));
            Assert.AreEqual(s.Left, 1, "test 3 left");
            Assert.AreEqual(s.Right, 2, "test 3 right");
            Assert.AreEqual(s.Top, 0, "test 3 top");
            Assert.AreEqual(s.Bottom, 1, "test 3 bottom");
            Assert.AreEqual(s.Area, 4, "test 3 area");

            s.IncludePosition(new MatrixPosition(4, 1));
            Assert.AreEqual(s.Left, 1, "test 4 left");
            Assert.AreEqual(s.Right, 2, "test 4 right");
            Assert.AreEqual(s.Top, 0, "test 4 top");
            Assert.AreEqual(s.Bottom, 4, "test 4 bottom");
            Assert.AreEqual(s.Area, 10, "test 4 area");

            s.IncludePosition(new MatrixPosition(4, -1));
            Assert.AreEqual(s.Left, -1, "test 5 left");
            Assert.AreEqual(s.Right, 2, "test 5 right");
            Assert.AreEqual(s.Top, 0, "test 5 top");
            Assert.AreEqual(s.Bottom, 4, "test 5 bottom");
            Assert.AreEqual(s.Area, 20, "test 5 area");
        }
    }
}
