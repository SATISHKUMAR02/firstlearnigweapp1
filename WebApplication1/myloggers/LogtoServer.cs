namespace WebApplication1.myloggers
{
    public class LogtoServer:IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
                
            Console.WriteLine("log to server");    
        }
    }
}
