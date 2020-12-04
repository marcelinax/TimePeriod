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
        private void AssertTimePeriod(TimePeriod t ,long expectedSeconds)
        {
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

        [DataTestMethod, TestCategory("Time Constructors")]
        [DataRow( (byte)24)]
        [DataRow((byte)25)]
        [DataRow((byte)30)]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWith2DefaultParamsArgumentException(byte h)
        {
            Time t = new Time(h);
        }

        [DataTestMethod, TestCategory("Time Constructors")]
        [DataRow((byte) 24,(byte) 60)]
        [DataRow((byte) 25, (byte) 72)]
        [DataRow((byte) 30, (byte) 63)]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithDefaultParamArgumentException(byte h, byte m)
        {
            Time t = new Time(h,m);
        }
        [DataTestMethod, TestCategory("Time Constructors")]
        [DataRow((byte) 24,(byte) 60, (byte) 79)]
        [DataRow((byte) 28, (byte) 72, (byte)60)]
        [DataRow((byte) 35, (byte) 63, (byte)63)]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWith3ParamsArgumentException(byte h, byte m, byte s)
        {
            Time t = new Time(h,m,s);
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
        [DataRow((long) 2423,(long) 2423)]
        [DataRow((long) 1200,(long) 1200)]
        [DataRow((long) 6000,(long) 6000)]
       
        public void ConstructorWithSeconds(long s, long expectedS)
        {
            TimePeriod t = new TimePeriod(s);
            AssertTimePeriod(t, expectedS);
            
        }
        [TestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow("1:02:09",(long)3729)]
        [DataRow("22:43:12",(long)81792)]
        [DataRow("123:02:10",(long)442930)]
       
        public void StringConstructorTimePeriod(string t, long s)
        {
            TimePeriod timePeriodToString = new TimePeriod(t);
            AssertTimePeriod(timePeriodToString,s);
        }

        [TestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow((byte) 0, (byte) 10, (byte) 10,(byte) 0, (byte) 20, (byte) 10, (long) 600)]
        [DataRow((byte) 12, (byte) 52, (byte) 2,(byte) 15, (byte) 2, (byte) 50, (long) 7848)]
        [DataRow((byte) 23, (byte) 31, (byte) 22,(byte) 13, (byte) 42, (byte) 2, (long) 51040)]
        public void ConstructorWithTwoTime(byte h, byte m, byte s, byte h2, byte m2, byte s2, long s3)
        {
            Time t = new Time(h,m,s);
            Time t2 = new Time(h2,m2,s2);
            TimePeriod t3 = new TimePeriod(t,t2);
            AssertTimePeriod(t3, s3);
        }
        [TestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow((ulong) 12, (byte) 10, (long) 43800)]
        [DataRow((ulong) 9, (byte) 52, (long) 35520)]
        [DataRow((ulong) 22, (byte) 12, (long) 79920)]
        public void ConstructorWithDefaultParamForTimePeriod(ulong h, byte m, long expectedS)
        {
            TimePeriod t = new TimePeriod(h,m,0);
            AssertTimePeriod(t, expectedS);
        }
        [TestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow((ulong) 1, (byte) 10, (byte) 10, (long) 4210)]
        [DataRow((ulong) 15, (byte) 42, (byte) 2, (long) 56522)]
        [DataRow((ulong) 10, (byte) 50, (byte) 32, (long) 39032)]
        public void ConstructorWith3ParamsForTimePeriod(ulong h, byte m, byte s, long expectedS)
        {
            TimePeriod t = new TimePeriod(h,m,s);
            AssertTimePeriod(t, expectedS);
        }
        [DataTestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow((ulong)24, (byte) 60)]
        [DataRow((ulong)25,(byte) 67)]
        [DataRow((ulong)30,(byte) 80)]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithDefaultParamForTimePeriodArgumentException(ulong h, byte m)
        {
            TimePeriod t = new TimePeriod(h,m,0);
        }
        [DataTestMethod, TestCategory("TimePeriod Constructors")]
        [DataRow((ulong)24, (byte) 60,(byte) 60)]
        [DataRow((ulong)25,(byte) 67,(byte) 90)]
        [DataRow((ulong)30,(byte) 80,(byte) 86)]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWith3ParamsForTimePeriodArgumentException(ulong h, byte m, byte s)
        {
            TimePeriod t = new TimePeriod(h,m,s);
        }
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
        public void CheckIfBothTimePeriodAreEqual(ulong hour, byte minute, byte second, ulong hour2, byte minute2, byte second2)
        {
            TimePeriod t1 = new TimePeriod( hour, minute, second);
            TimePeriod t2 = new TimePeriod(hour2, minute2, second2);
            Assert.AreEqual(true, t1 == t2);
        }

        #endregion

        #region CompareTo
        #region Time
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow((byte)2, (byte)50,(byte) 20,(byte) 3,(byte) 50,(byte) 20)]
        [DataRow((byte)5, (byte)20,(byte) 32,(byte) 13,(byte) 1,(byte) 20)]
        [DataRow((byte)22, (byte)10,(byte) 12,(byte) 22,(byte) 10,(byte) 20)]
        public void TimeSmallerThanOtherTime(byte h, byte m, byte s, byte h2, byte m2, byte s2)
        {
            Time t1 = new Time( h, m, s);
            Time t2 = new Time(h2, m2, s2);
            Assert.AreEqual(true, t1 < t2);
        }
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow((byte)15, (byte)50,(byte) 20,(byte) 13,(byte) 50,(byte) 20)]
        [DataRow((byte)5, (byte)13,(byte) 32,(byte) 1,(byte) 12,(byte) 20)]
        [DataRow((byte)22, (byte)32,(byte) 44,(byte) 22,(byte) 10,(byte) 20)]
        public void TimeGreaterThanOtherTime(byte h, byte m, byte s, byte h2, byte m2, byte s2)
        {
            Time t1 = new Time( h, m, s);
            Time t2 = new Time(h2, m2, s2);
            Assert.AreEqual(true, t1 > t2);
        }
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow((byte)2, (byte)50,(byte) 20,(byte) 2,(byte) 50,(byte) 20)]
        [DataRow((byte)15, (byte)20,(byte) 32,(byte) 18,(byte) 1,(byte) 20)]
        [DataRow((byte)9, (byte)32,(byte) 45,(byte) 9,(byte) 32,(byte) 45)]
        public void TimeSmallerThanOrEqualOtherTime(byte h, byte m, byte s, byte h2, byte m2, byte s2)
        {
            Time t1 = new Time( h, m, s);
            Time t2 = new Time(h2, m2, s2);
            Assert.AreEqual(true, t1 <= t2);
        }
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow((byte)2, (byte)52,(byte) 20,(byte) 2,(byte) 50,(byte) 20)]
        [DataRow((byte)15, (byte)20,(byte) 32,(byte) 15,(byte) 20,(byte) 32)]
        [DataRow((byte)9, (byte)32,(byte) 45,(byte) 5,(byte) 22,(byte) 13)]
        public void TimeGreaterThanOrEqualOtherTime(byte h, byte m, byte s, byte h2, byte m2, byte s2)
        {
            Time t1 = new Time( h, m, s);
            Time t2 = new Time(h2, m2, s2);
            Assert.AreEqual(true, t1 >= t2);
        }
        #endregion

        #region TimePeriod
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow((ulong)123, (byte)32,(byte) 20,(ulong) 123,(byte) 50,(byte) 20)]
        [DataRow((ulong)15, (byte)20,(byte) 52,(ulong) 17,(byte) 1,(byte) 20)]
        [DataRow((ulong)22, (byte)10,(byte) 12,(ulong) 32,(byte) 12,(byte) 54)]
        public void TimePeriodSmallerThanOtherTimePeriod(ulong h, byte m, byte s, ulong h2, byte m2, byte s2)
        {
            TimePeriod t1 = new TimePeriod( h, m, s);
            TimePeriod t2 = new TimePeriod(h2, m2, s2);
            Assert.AreEqual(true, t1 < t2);
        }
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow((ulong)24, (byte)12,(byte) 53,(ulong) 12,(byte) 50,(byte) 20)]
        [DataRow((ulong)56, (byte)20,(byte) 52,(ulong) 54,(byte) 1,(byte) 1)]
        [DataRow((ulong)22, (byte)10,(byte) 12,(ulong) 2,(byte) 12,(byte) 54)]
        public void TimePeriodGreaterThanOtherTimePeriod(ulong h, byte m, byte s, ulong h2, byte m2, byte s2)
        {
            TimePeriod t1 = new TimePeriod( h, m, s);
            TimePeriod t2 = new TimePeriod(h2, m2, s2);
            Assert.AreEqual(true, t1 > t2);
        }
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow((ulong)123, (byte)32,(byte) 20,(ulong) 123,(byte) 32,(byte) 20)]
        [DataRow((ulong)32, (byte)12,(byte) 2,(ulong) 39,(byte) 1,(byte) 20)]
        [DataRow((ulong)2, (byte)10,(byte) 12,(ulong) 2,(byte) 52,(byte) 54)]
        public void TimePeriodSmallerThanOrEqualOtherTimePeriod(ulong h, byte m, byte s, ulong h2, byte m2, byte s2)
        {
            TimePeriod t1 = new TimePeriod( h, m, s);
            TimePeriod t2 = new TimePeriod(h2, m2, s2);
            Assert.AreEqual(true, t1 <= t2);
        }
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow((ulong)123, (byte)32,(byte) 20,(ulong) 123,(byte) 32,(byte) 20)]
        [DataRow((ulong)233, (byte)12,(byte) 2,(ulong) 123,(byte) 1,(byte) 20)]
        [DataRow((ulong)42, (byte)10,(byte) 12,(ulong) 42,(byte) 2,(byte) 54)]
        public void TimePeriodGreaterThanOrEqualOtherTimePeriod(ulong h, byte m, byte s, ulong h2, byte m2, byte s2)
        {
            TimePeriod t1 = new TimePeriod( h, m, s);
            TimePeriod t2 = new TimePeriod(h2, m2, s2);
            Assert.AreEqual(true, t1 >= t2);
        }
        

        #endregion
        #endregion

        #region PlusMetod

        [TestMethod, TestCategory("Plus operator")]
        [DataRow((ulong) 3, (byte) 20, (byte) 20,(byte) 1, (byte) 10, (byte) 50,(byte) 4, (byte)31, (byte)10)]
        [DataRow((ulong) 13, (byte) 12, (byte) 1,(byte) 5, (byte) 53, (byte) 12,(byte) 19, (byte)5, (byte)13)]
        [DataRow((ulong) 15, (byte) 45, (byte) 23,(byte) 2, (byte) 12, (byte) 21,(byte) 17, (byte)57, (byte)44)]
       
        public void CheckOperatorPlusingTime(ulong h, byte m, byte s, byte h2, byte m2, byte s2, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t1 = new Time(h2,m2,s2);
            TimePeriod t2 = new TimePeriod(h, m, s);
            Time t3 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual( t1+t2, t3 );
        }
        [TestMethod, TestCategory("Plus operator")]
        [DataRow((byte) 3, (byte) 10, (byte) 10,(long) 3600, (byte) 4,(byte) 10, (byte)10)]
        [DataRow((byte) 5, (byte) 22, (byte) 12,(long) 6524, (byte) 7,(byte) 10, (byte)56)]
        [DataRow((byte) 13, (byte) 2, (byte) 56,(long) 8200, (byte) 15,(byte) 19, (byte)36)]
        public void CheckOperatorPlusingTimeAndTimePeriod(byte h, byte m, byte s, long s2, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t1 = new Time(h,m,s);
            TimePeriod t2 = new TimePeriod(s2);
            Time t3 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual( t1+t2, t3 );
        }

        [TestMethod, TestCategory("Plus operator")]
        [DataRow((long) 2000, (long) 1000, (long) 3000)]
        [DataRow((long) 3600, (long) 1234, (long) 4834)]
        [DataRow((long) 2000, (long) 52322, (long) 54322)]
        public void CheckOperatorPlusingTimePeriod(long s, long s2, long expectedS)
        {
            TimePeriod t = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            TimePeriod t3 = new TimePeriod(expectedS);
            AssertTimePeriod(t+t2, t3.Seconds);
        }


        #endregion
    }
}