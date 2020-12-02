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
        [DataRow((byte)12, (byte)12)]
        [DataRow((byte)9, (byte)9)]
        [DataRow((byte)15, (byte)15)]
        public void ConstructorWith2DefaultParams(byte h, byte expectedH){
            Time t = new Time(h);
            AssertTime(t, expectedH, 0, 0);
        }
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow((byte)12, (byte)43,(byte)12,(byte)43)]
        [DataRow((byte)9, (byte)57,(byte) 9,(byte)57)]
        [DataRow((byte)15,(byte)13, (byte)15,(byte) 13)]
        public void ConstructorWithDefaultParam(byte h, byte m, byte expectedH,  byte expectedM){
            Time t = new Time(h,  m);
            AssertTime(t, expectedH, expectedM, 0);
        }

        [TestMethod, TestCategory("Time Constructors")]
        [DataRow((byte)11, (byte)22,(byte)9,(byte)11, (byte)22,(byte)9)]
        [DataRow((byte)1, (byte)2, (byte)3,(byte)1,(byte) 2,(byte) 3)]
        [DataRow((byte)18,(byte)13,(byte) 2,(byte) 18,(byte)13,(byte) 2)]
        public void ConstructorWith3Params(byte h, byte m, byte s,byte expectedH, byte expecetedM, byte expecetedS)
        {
            Time t = new Time(h,m,s);
            AssertTime(t,expectedH, expecetedM,expecetedS);
        }
        #endregion

        #region TimePeriod
        [TestMethod, TestCategory("TimePeriod Constructors")]
        public void DefaultConstructorForTimePeriod()
        {
            TimePeriod t = new TimePeriod();
            Assert.AreEqual(0, t.Seconds);
        }
        [TestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow((ulong)1, (byte)34,(ulong)1, (byte)34)]
        [DataRow((ulong)9, (byte)56,(ulong) 9,(byte)56)]
        [DataRow((ulong)15,(byte)15,(ulong) 15,(byte)15)]
        public void ConstructorWithDefaultParamForTimePeriod(ulong h, byte m, ulong expectedH, byte expectedM){
            TimePeriod time = new TimePeriod( h, m);
            AssertTime(time,  expectedH,  expectedM, 0);
        }
        

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
        [DataRow((ulong)5, 50, 20, (ulong)1, 9, 40)]
        public void CheckOperatorPlusingTime(ulong hour, int minute, int second, ulong hour2, int minute2, int second2)
        {
            Time t1 = new Time((byte) hour, (byte)minute,(byte) second);
            TimePeriod t2 = new TimePeriod(hour2, (byte)minute2, (byte)second2);
            Time t3 = t1 + t2;
            Assert.AreEqual( t1 + t2, t3 );
        }


        #endregion
    }
}