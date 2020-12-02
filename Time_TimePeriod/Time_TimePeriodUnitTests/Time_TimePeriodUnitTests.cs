using System;
using System.Data.SqlTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_TimePeriod;
using System.Globalization;
using System.Threading;

namespace Time_TimePeriodUnitTests
{
    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    [TestClass]
    public class UnitTestsTime_TimePeriodConstructors
    {
        private void AssertTime(Time t, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Assert.AreEqual(expectedHours, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }


        #region Constructor tests

        #region Time
        [TestMethod, TestCategory("Time Constructors")]
        public void DefaultConstructor()
        {
            Time t = new Time();
            Assert.AreEqual(0, t.Hours);
            Assert.AreEqual(0, t.Minutes);
            Assert.AreEqual(0, t.Seconds);
        }
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow(12, 12)]
        [DataRow(9, 9)]
        [DataRow(15, 15)]
        public void ConstructorWith2DefaultParams(int h, int expectedH){
            Time t = new Time((byte)h);
            AssertTime(t,(byte) expectedH, 0, 0);
        }
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow(12, 43,12,43)]
        [DataRow(9, 57, 9,57)]
        [DataRow(15,13, 15, 13)]
        public void ConstructorWithDefaultParam(int h, int m, int expectedH,  int expectedM){
            Time t = new Time((byte)h, (byte) m);
            AssertTime(t,(byte) expectedH, (byte)expectedM, 0);
        }

        [TestMethod, TestCategory("Time Constructors")]
        [DataRow(11, 22,9,11, 22,9)]
        [DataRow(1, 2, 3,1, 2, 3)]
        [DataRow(18,13, 2, 18,13, 2)]
        public void ConstructorWith3Params(int h, int m, int s,int expectedH, int expecetedM, int expecetedS)
        {
            Time t = new Time((byte)h,(byte)m,(byte)s);
            AssertTime(t,(byte)expectedH, (byte)expecetedM,(byte)expecetedS);
        }
        #endregion

        #region TimePeriod
        [TestMethod, TestCategory("TimePeriod Constructors")]
        public void DefaultConstructorForTimePeriod()
        {
            TimePeriod t = new TimePeriod();
            Assert.AreEqual(0, t.Seconds);
        }
        /*[TestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow(1, 34,1, 34)]
        [DataRow(9, 56, 9,56)]
        [DataRow(15,15, 15,15)]
        public void ConstructorWithDefaultParamForTimePeriod(ulong h, int m, ulong expectedH, int expectedM){
            Time t = new Time((byte) h, (byte) m);
            AssertTime(t, (byte) expectedH, (byte) expectedM, 0);
        }*/
        

        #endregion
        #endregion

        #region ToStringTests

        /*[TestMethod, TestCategory("String representation")]*/

        #endregion

        #region Equals

        [TestMethod, TestCategory("Equals Time")]
        [DataRow(2, 50, 20, 2, 50, 20)]
        [DataRow(12, 32, 02, 12, 32, 02)]
        [DataRow(22, 01, 23, 22, 01, 23)]
        [DataRow(9, 12, 52, 9, 12, 52)]
        public void CheckIfBothTimeAreEqual(int hour, int minute, int second, int hour2, int minute2, int second2)
        {
            Time t1 = new Time((byte) hour, (byte)minute,(byte) second);
            Time t2 = new Time((byte)hour2, (byte)minute2, (byte)second2);
            Assert.AreEqual(true, t1 == t2);
        }
        [TestMethod, TestCategory("Equals TimePeriod")]
        [DataRow(22, 12, 2, 22, 12, 2)]
        [DataRow(13, 1, 56, 13, 1, 56)]
        [DataRow(1, 16, 23, 1, 16, 23)]
        [DataRow(9, 11, 52, 9, 11, 52)]
        public void CheckIfBothTimeAreEqualForTimePeriod(int hour, int minute, int second, int hour2, int minute2, int second2)
        {
            TimePeriod t1 = new TimePeriod((ulong) hour, (byte)minute,(byte) second);
            TimePeriod t2 = new TimePeriod((ulong)hour2, (byte)minute2, (byte)second2);
            Assert.AreEqual(true, t1 == t2);
        }

        #endregion

        #region CompareTo

        [TestMethod, TestCategory("CompareTo")]
        [DataRow(2, 50, 20, 3, 50, 20)]
        public void CompareTwoTime(int hour, int minute, int second, int hour2, int minute2, int second2)
        {
            Time t1 = new Time((byte) hour, (byte)minute,(byte) second);
            Time t2 = new Time((byte)hour2, (byte)minute2, (byte)second2);
            Assert.AreEqual(true, t1 < t2);
        }


        #endregion

        #region PlusMetod

        [TestMethod, TestCategory("Plus operator")]
        [DataRow(5, 50, 20, 1, 9, 40)]
        public void CheckOperatorPlusingTime(int hour, int minute, int second, int hour2, int minute2, int second2)
        {
            Time t1 = new Time((byte) hour, (byte)minute,(byte) second);
            TimePeriod t2 = new TimePeriod((byte)hour2, (byte)minute2, (byte)second2);
            Time t3 = t1 + t2;
            Assert.AreEqual( t1 + t2, t3 );
        }


        #endregion
    }
}