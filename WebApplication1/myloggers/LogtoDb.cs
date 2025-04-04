namespace WebApplication1.myloggers
{
    public class LogtoDb:IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogtoDB");
        }
    }
}
