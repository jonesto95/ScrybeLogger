using ScrybeLogger;
using ScrybeLogger.Interface;
using ScrybeLogger.Extensions;
using System.Collections.Generic;

namespace ScrybeTest
{
    class Program
    {
        private static IScrybe<ScrybeTagTester> scrybe = new Scrybe<ScrybeTagTester>();
        
        static void Main(string[] args)
        {
            scrybe.LogMethodStart(args);
            int ggg = 10;
            scrybe.LogInfo("Info");
            scrybe.LogFatal("Fat");
            scrybe.LogVariableValue(nameof(ggg), ggg);
            scrybe.LogMethodEnd(5);
            Test(new Dictionary<int, int>(), string.Empty);
            System.Console.Read();
        }


        private static void Test(Dictionary<int, int> values, string ga)
        {
            scrybe.LogMethodStart(values);
        }
    }


    // [Scrybe("A")]
    public class ScrybeTagTester
    {

    }
}
