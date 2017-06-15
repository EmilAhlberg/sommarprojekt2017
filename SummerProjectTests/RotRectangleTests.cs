using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SummerProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.Tests
{
    [TestClass()]
    public class RotRectangleTests
    {
        [TestMethod()]
        public void IntersectsTest()
        {
            RotRectangle rect1 = new RotRectangle(new Rectangle(1,-1,2,2),0);
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateAxesTest()
        {
            Assert.Fail();
        }
    }
}