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
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow("11:22:09",(byte)11, (byte)22,(byte)9)]
        [DataRow("01:02:03",(byte)1, (byte)2, (byte)3)]
        [DataRow("18:13:02",(byte)18,(byte)13,(byte) 2)]
        public void StringConstructor(string t,byte h, byte m, byte s)
        {
            Time timeToString = new Time(t);
            AssertTime(timeToString,h,m,s);
        }
        #endregion

        #region TimePeriod
        /*[TestMethod, TestCategory("TimePeriod Constructors")]
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
        }*/
        

        #endregion
        #endregion

        #region ToStringTests

        [TestMethod, TestCategory("String representation")]
        public void DefaultToString()
        {
            var t = new Time(12,30);
            string expectedString = "12:30:00";
            Assert.AreEqual(expectedString, t.ToString());
        }

        [TestMethod, TestCategory("String representation")]
        [DataRow((byte)18,(byte)13,(byte) 2, "18:13:02")]
        [DataRow((byte)2,(byte)27,(byte) 59, "02:27:59")]
        [DataRow((byte)22,(byte)2,(byte) 23, "22:02:23")]
        public void ToString(byte h, byte m, byte s, string expectedString)
        {
            var t = new Time(h,m,s);
            Assert.AreEqual(expectedString, t.ToString());
        }
        [TestMethod, TestCategory("String representation")]
        public void DefaultToStringForTimePeriod()
        {
            var t = new TimePeriod(129,30, 23);
            string expectedString = "129:30:23";
            Assert.AreEqual(expectedString, t.ToString());
        }
        [TestMethod, TestCategory("String representation")]
        [DataRow((ulong)18,(byte)13,(byte) 2, "18:13:02")]
        [DataRow((ulong)2,(byte)27,(byte) 59, "2:27:59")]
        [DataRow((ulong)22,(byte)2,(byte) 23, "22:02:23")]
        public void ToStringForTimePeriod(ulong h, byte m, byte s, string expectedString)
        {
            var t = new TimePeriod(h,m,s);
            Assert.AreEqual(expectedString, t.ToString());
        }

        #endregion

        #region Equals

        [TestMethod, TestCategory("Equals Time")]
        [DataRow((byte)2, (byte)50,(byte) 20,(byte) 2,(byte) 50, (byte)20)]
        [DataRow((byte)12, (byte)32,(byte) 02, (byte)12, (byte)32,(byte) 02)]
        [DataRow((byte)22, (byte)01,(byte) 23,(byte) 22,(byte) 01,(byte) 23)]
        [DataRow((byte)9, (byte)12, (byte)52,(byte) 9, (byte)12,(byte) 52)]
        public void CheckIfBothTimeAreEqual(byte hour, byte minute, byte second, byte hour2, byte minute2, byte second2)
        {
            Time t1 = new Time( hour, minute, second);
            Time t2 = new Time(hour2, minute2, second2);
            Assert.AreEqual(true, t1 == t2);
        }
        [TestMethod, TestCategory("Equals TimePeriod")]
        [DataRow((ulong)22, (byte)12,(byte) 2, (ulong)22,(byte) 12,(byte) 2)]
        [DataRow((ulong)13, (byte)1, (byte)56,(ulong) 13, (byte)1, (byte)56)]
        [DataRow((ulong)1, (byte)16,(byte) 23,(ulong) 1, (byte)16,(byte) 23)]
        [DataRow((ulong)9, (byte)11,(byte) 52,(ulong) 9, (byte)11,(byte) 52)]
        public void CheckIfBothTimeAreEqualForTimePeriod(ulong hour, byte minute, byte second, ulong hour2, byte minute2, byte second2)
        {
            TimePeriod t1 = new TimePeriod( hour, minute, second);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);
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