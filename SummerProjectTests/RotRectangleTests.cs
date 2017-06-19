using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace SummerProject.Tests
{
    [TestClass()]
    public class RotRectangleTests
    {
        [TestMethod()]
        public void IntersectsTest()
        {
            RotRectangle rect1 = new RotRectangle(new Rectangle(1, -1, 2, 2), 0);
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateAxesTest()
        {
            Assert.Fail();
        }
    }
}