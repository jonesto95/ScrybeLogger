using ScrybeLogger;

namespace ScrybeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = ScrybeBuilder.BuildScrybe<ScrybeTagTester>();
            s.LogMethodStart(args);
            s.LogInfo("Info");
            s.LogFatal("Fat");
            s.LogMethodEnd(5);
        }
    }


    // [Scrybe("A")]
    public class ScrybeTagTester
    {

    }
}
