using System.Runtime.ConstrainedExecution;

namespace WebApplication1.myloggers
{
    public class Logtofile:IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("log to file");
        }
    }
}
