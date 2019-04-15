using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Darwin_s_Lab.Simulation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Map map = new Map(0.8);
            for (int i=0 ; i < 100 ; i++)
            {
                Food newFood = new Food(map);
                Assert.AreEqual(
                    Map.DistanceBetweenTwoPoints(
                        newFood.Position, 
                        new Point(500,500)
                    ) < map.SafeZoneRadius,
                    true
                );
            }
        }
    }
}
