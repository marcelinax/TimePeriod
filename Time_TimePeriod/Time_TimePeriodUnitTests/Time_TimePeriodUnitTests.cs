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
            Assert.AreEqual(expectedSeconds, t._Seconds);
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
            Assert.AreEqual(0, t._Seconds);
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
        [TestMethod, TestCategory("Equals Time")]
        [DataRow("12:43:56","12:43:56")]
        [DataRow("23:12:22","23:12:22")]
        public void CheckIfBothTimeAreEqualStringsOnly(string s, string s2)
        {
            Time t1 = new Time( s);
            Time t2 = new Time(s2);
            Assert.AreEqual(true, t1 == t2);
        }
        [TestMethod, TestCategory("Equals Time")]
        [DataRow("00:45:12",(byte)0, (byte) 45, (byte) 12)]
        [DataRow("13:22:09",(byte)13, (byte) 22, (byte) 9)]
        public void CheckIfBothTimeAreEqualStringAnd3Params(string s, byte h, byte m,byte s2)
        {
            Time t1 = new Time( s);
            Time t2 = new Time(h,m,s2);
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
        [TestMethod, TestCategory("Equals TimePeriod")]
        [DataRow("123:12:56","123:12:56")]
        [DataRow("0:59:12","0:59:12")]
       
        public void CheckIfBothTimePeriodAreEqualStringsOnly(string s, string s2)
        {
            TimePeriod t1 = new TimePeriod( s);
            TimePeriod t2 = new TimePeriod(s2);
            Assert.AreEqual(true, t1 == t2);
        }
        [TestMethod, TestCategory("Equals TimePeriod")]
        [DataRow("36:24:30",(ulong) 36, (byte) 24, (byte) 30)]
        [DataRow("25:13:57",(ulong) 25, (byte) 13, (byte) 57)]
       
        public void CheckIfBothTimePeriodAreEqualStringAnd3Params(string s, ulong h, byte m, byte s2)
        {
            TimePeriod t1 = new TimePeriod( s);
            TimePeriod t2 = new TimePeriod(h,m,s2);
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
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow("12:32:11", "16:17:56")]
        [DataRow("00:23:44", "18:09:12")]
        public void TimeSmallerThanOtherTimeStringsOnly(string s, string s2)
        {
            Time t1 = new Time(s);
            Time t2 = new Time(s2);
            Assert.AreEqual(true, t1 < t2);
        }
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow("19:26:33", "16:17:56")]
        [DataRow("23:59:32", "18:09:12")]
        public void TimeGreaterThanOtherTimeStringsOnly(string s, string s2)
        {
            Time t1 = new Time(s);
            Time t2 = new Time(s2);
            Assert.AreEqual(true, t1 > t2);
        }
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow("12:32:56",(byte)22, (byte)12,(byte) 2)]
        [DataRow("18:22:09",(byte)23, (byte)16,(byte) 32)]
        public void TimeSmallerThanOtherTimeStringAnd3Params(string s, byte h, byte m, byte s2)
        {
            Time t1 = new Time(s);
            Time t2 = new Time(h,m,s2);
            Assert.AreEqual(true, t1 < t2);
        }
        [TestMethod, TestCategory("CompareTo Time")]
        [DataRow("12:32:56",(byte)2, (byte)52,(byte) 20)]
        [DataRow("18:22:09",(byte)15, (byte)20,(byte) 32)]
        public void TimeGreaterThanOtherTimeStringAnd3Params(string s, byte h, byte m, byte s2)
        {
            Time t1 = new Time(s);
            Time t2 = new Time(h,m,s2);
            Assert.AreEqual(true, t1 > t2);
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
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow("112:45:12", "112:45:12")]
        [DataRow("52:47:13", "62:12:11")]
        public void TimePeriodSmallerThanOrEqualOtherTimePeriodStringsOnly(string s, string s2)
        {
            TimePeriod t1 = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            Assert.AreEqual(true, t1 <= t2);
        }
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow("65:36:13", "12:45:14")]
        [DataRow("22:06:32", "22:06:32")]
        public void TimePeriodGreaterThanOrEqualOtherTimePeriodStringsOnly(string s, string s2)
        {
            TimePeriod t1 = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            Assert.AreEqual(true, t1 >= t2);
        }
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow("13:54:36", (long) 6987458)]
        [DataRow("22:06:32", (long) 79592)]
        public void TimePeriodSmallerThanOrEqualOtherTimePeriodStringAndSeconds(string s, long s2)
        {
            TimePeriod t1 = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            Assert.AreEqual(true, t1 <= t2);
        }
        [TestMethod, TestCategory("CompareTo TimePeriod")]
        [DataRow("65:36:13", (long) 69878)]
        [DataRow("22:06:32", (long) 79592)]
        public void TimePeriodGreaterThanOrEqualOtherTimePeriodStringAndSeconds(string s, long s2)
        {
            TimePeriod t1 = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            Assert.AreEqual(true, t1 >= t2);
        }
        

        #endregion
        #endregion

        #region PlusMetod

        #region Time
      
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
        [DataRow("12:12:12", "5:13:23", "17:25:35")]
        [DataRow("01:52:36", "6:47:13", "08:39:49")]
        public void CheckOperatorPlusingTimeStringsOnly(string s, string s2, string expectedString)
        {
            Time t1 = new Time(s);
            TimePeriod t2 = new TimePeriod( s2);
            Time t3 = new Time( expectedString);
            Assert.AreEqual( t1+t2, t3 );
        }
        [TestMethod, TestCategory("Plus operator")]
        [DataRow("15:32:17", (long) 7420, "17:35:57")]
        [DataRow("23:12:14", (long) 4578, "00:28:32")]
        public void CheckOperatorPlusingTimeStringAndSeconds(string s, long s2, string expectedString)
        {
            Time t1 = new Time(s);
            TimePeriod t2 = new TimePeriod( s2);
            Time t3 = new Time( expectedString);
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
        #endregion
        #region  TimePeriod
        [TestMethod, TestCategory("Plus operator")]
        [DataRow((long) 2000, (long) 1000, (long) 3000)]
        [DataRow((long) 3600, (long) 1234, (long) 4834)]
        [DataRow((long) 2000, (long) 52322, (long) 54322)]
        public void CheckOperatorPlusingTimePeriodSeconds(long s, long s2, long expectedS)
        {
            TimePeriod t = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            TimePeriod t3 = new TimePeriod(expectedS);
            Assert.AreEqual(t3, t+t2);
        }
        [TestMethod, TestCategory("Plus operator")]
        [DataRow((ulong) 36, (byte) 20, (byte) 50, (long) 3600, (long)134450 )]
        [DataRow((ulong) 12, (byte) 32, (byte) 1, (long) 5466, (long)50587 )]
        public void CheckOperatorPlusingTimePeriodWith3ParamAndSeconds(ulong h, byte m, byte s, long s2, long expectedS)
        {
            TimePeriod t = new TimePeriod(h,m,s);
            TimePeriod t2 = new TimePeriod(s2);
            TimePeriod t3 = new TimePeriod(expectedS);
            Assert.AreEqual(t3, t+t2);
        }
        [TestMethod, TestCategory("Plus operator")]
        [DataRow("12:45:30", "56:12:03", "68:57:33")]
        [DataRow("2:34:08", "14:57:11", "17:31:19")]
        public void CheckOperatorPlusingTimePeriodStringsOnly(string s, string s2, string expectedString)
        {
            TimePeriod t = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            TimePeriod t3 = new TimePeriod(expectedString);
            Assert.AreEqual(t3, t+t2);
        }
        [TestMethod, TestCategory("Plus operator")]
        [DataRow("4:12:12", (long) 2600, "4:55:32")]
        [DataRow("44:57:10", (long) 4005, "46:03:55")]
        
        public void CheckOperatorPlusingTimePeriodStringAndSeconds(string s, long s2, string expectedString)
        {
            TimePeriod t = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(s2);
            TimePeriod t3 = new TimePeriod(expectedString);
            Assert.AreEqual(t3, t+t2);
        }
        #endregion


        #endregion

        #region MinusMetod

        #region  TimePeriod
        [TestMethod, TestCategory("Minus operator")]
        [DataRow((long) 3600, (long) 1200, (long) 2400)]
        [DataRow((long) 52322, (long) 2000, (long) 50322)]
        [DataRow((long) 100, (long) 5476, (long) -5376)]
        
        public void OperatorMinusTimePeriod(long s,long s2, long expectedS)
        {
          TimePeriod t = new TimePeriod(s);
          TimePeriod t2 = new TimePeriod(s2);
          TimePeriod t3 = new TimePeriod(expectedS);
          Assert.AreEqual(t3, t-t2);
        }
        [TestMethod, TestCategory("Minus operator")]
        [DataRow("123:00:20", "96:20:12", "26:40:08")]
        [DataRow("65:14:02", "11:23:56", "53:50:06")]
        [DataRow("14:51:11", "2:11:43", "12:39:28")]
        public void OperatorMinusTimePeriodWithString(string timePeriod, string timePeriod2 , string expectedString)
        {
            TimePeriod t = new TimePeriod(timePeriod);
            TimePeriod t2 = new TimePeriod(timePeriod2);
            TimePeriod t3 = new TimePeriod(expectedString);
            Assert.AreEqual(t-t2, t3);
        }
        [TestMethod, TestCategory("Minus operator")]
        [DataRow("20:55:13", (long) 1234, (long)74079)]
        [DataRow("11:12:13", (long) 35000, (long)5333)]
        [DataRow("1:34:57", (long) 245, (long) 5452)]
        public void OperatorMinusTimePeriodOnStringAndSeconds(string timePeriod, long s , long expectedS)
        {
            TimePeriod t = new TimePeriod(timePeriod);
            TimePeriod t2 = new TimePeriod(s);
            TimePeriod t3 = new TimePeriod(expectedS);
            Assert.AreEqual(t-t2, t3);
        }
        [TestMethod, TestCategory("Minus operator")]
        [DataRow((long) 45287, "12:10:02", "0:24:45")]
        [DataRow((long) 89898, "12:10:02", "12:48:16")]
        [DataRow((long) 230241, "12:10:02", "51:47:19")]
        public void OperatorMinusTimePeriodOnStringAndSeconds2(long s,string timePeriod , string expectedString)
        {
            TimePeriod t = new TimePeriod(s);
            TimePeriod t2 = new TimePeriod(timePeriod);
            TimePeriod t3 = new TimePeriod(expectedString);
            Assert.AreEqual(t-t2, t3);
        }
        [TestMethod, TestCategory("Minus operator")]
        [DataRow((ulong) 13, (byte) 52, (byte) 12, (ulong) 2, (byte) 13, (byte) 54, (long) 41898)]
        [DataRow((ulong) 33, (byte) 12, (byte) 53, (ulong) 22, (byte) 3, (byte) 43, (long) 40150)]
        [DataRow((ulong) 1, (byte) 45, (byte) 59, (ulong)1, (byte) 13, (byte) 2, (long) 1977)]
        public void OperatorMinusTimePeriodWith3Params(ulong h, byte m, byte s, ulong h2, byte m2, byte s2, long expectedS)
        {
            TimePeriod t = new TimePeriod(h,m,s);
            TimePeriod t2 = new TimePeriod(h2,m2,s2);
            TimePeriod t3 = new TimePeriod(expectedS);
            Assert.AreEqual(t-t2, t3);
        }
        #endregion
        #region Time
        [TestMethod, TestCategory("Minus operator")]
        [DataRow((byte) 3, (byte) 10, (byte) 10,(long) 3600, (byte) 2,(byte) 10, (byte)10)]
        [DataRow((byte) 13, (byte) 20, (byte) 52,(long) 12344, (byte) 9,(byte) 55, (byte)08)]
        [DataRow((byte) 21, (byte) 2, (byte) 37,(long) 1490, (byte) 20,(byte) 37, (byte)47)]
        public void OperatorMinusTimeAndTimePeriod(byte h, byte m, byte s, long s2, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t1 = new Time(h,m,s);
            TimePeriod t2 = new TimePeriod(s2);
            Time t3 = new Time(expectedH, expectedM, expectedS);
            Assert.AreEqual( t3, t1-t2 );
        }
        [TestMethod, TestCategory("Minus operator")]
        [DataRow("05:11:40", "2:11:39","03:00:01")]
        [DataRow("21:48:56", "12:26:45","09:22:11")]
       
        public void OperatorMinusTimeAndTimePeriodWithStrings(string time, string timePeriod, string expectedString)
        {
            Time t1 = new Time(time);
            TimePeriod t2 = new TimePeriod(timePeriod);
            Time t3 = new Time(expectedString);
            Assert.AreEqual( t3, t1-t2 );
        }
        [TestMethod, TestCategory("Minus operator")]
        [DataRow((byte) 22,(byte) 58,(byte) 22, (long) 3600,"21:58:22")]
        [DataRow((byte) 5,(byte) 12,(byte) 32, (long) 45896,"16:27:36")]
       
        public void OperatorMinusTimeAndTimePeriodAsString(byte h, byte m, byte s, long s2, string expectedString)
        {
            Time t1 = new Time(h,m,s);
            TimePeriod t2 = new TimePeriod(s2);
            Time t3 = new Time(expectedString);
            Assert.AreEqual( t3, t1-t2 );
        }
        [TestMethod, TestCategory("Minus operator")]
        [DataRow("03:14:12",(long) 54678,(byte) 12,(byte) 02,(byte) 54)]
        [DataRow("16:59:01",(long) 12478,(byte) 13,(byte) 31,(byte) 03)]
        public void OperatorMinusTimeAndTimePeriodWithStringAndParams(string s, long s2, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t1 = new Time(s);
            TimePeriod t2 = new TimePeriod(s2);
            Time t3 = new Time(expectedH,expectedM,expectedS);
            Assert.AreEqual( t3, t1-t2 );
        }
        #endregion
        #endregion
    }
}