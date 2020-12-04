using System;
using System.Security.Cryptography;

namespace Time_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = new Time(3,50,25);
            var time2 = new Time("09:23:09");
            var time3 = new Time(15,50,25);
            var timePeriod = new TimePeriod(1290);
            var timePeriod2 = new TimePeriod(12,34,20);
            var timePeriod3 = new TimePeriod(time3,time2);
            
            
            Console.WriteLine(time);
            Console.WriteLine(time2);
            Console.WriteLine(timePeriod);
            Console.WriteLine(timePeriod2);
            Console.WriteLine(timePeriod.TimePeriodPlus(timePeriod2));
            Console.WriteLine(time.TimePlus(timePeriod));
            Console.WriteLine(time.Equals(time2));
            Console.WriteLine(time > time2);
            Console.WriteLine(timePeriod2.Equals(timePeriod));
            Console.WriteLine(timePeriod2 > timePeriod);
            Console.WriteLine(Time.TimeMinus(time3, time2));
            Console.WriteLine(timePeriod3);
        }
    }
}