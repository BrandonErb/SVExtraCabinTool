using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StardewValleyExtraSlots;

namespace StardewValleyExtraSlots.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod()]
        public void CreateTimestampTest()
        {
            StardewValleyExtraSlots.AddSlots addSlot = new StardewValleyExtraSlots.AddSlots();
            List<string> timestamp = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                Int64 testNum = addSlot.CreateTimestamp();
                Debug.WriteLine(testNum.ToString());
                timestamp.Add(testNum.ToString());
            }

            foreach(string ts in timestamp)
            {
                Assert.AreEqual(18, ts.Length);
            }
        }
    }
}
