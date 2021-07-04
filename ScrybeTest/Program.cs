using ScrybeLogger;

namespace ScrybeTest
{
    class Program
    {
        static void Main()
        {
            var s = ScrybeBuilder.BuildScrybe<ScrybeTagTester>();
            s.LogInfo("Info");
            s.LogFatal("Fat");
        }
    }


    // [Scrybe("A")]
    public class ScrybeTagTester
    {

    }
}
