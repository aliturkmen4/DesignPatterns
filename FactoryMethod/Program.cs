using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            //günümüz design patternleri arasında en sık kullanılanlardan!

            //amaç:yazılımda değişimi kontrol altında tutmak

            CustomerManager customerManager = new CustomerManager(new LoggerFactory());

            customerManager.Save();

            Console.ReadLine();

        }
    }

    public class LoggerFactory:ILoggerFactory //bir class çıplak duruyorsa bu nesnellik açısından ileride sana sıkıntı çıkarır!
    {
        public ILogger CreateLogger() 
        {
            //Business to decide factory
            return new EdLogger();
        }
    }
    public class LoggerFactory2 : ILoggerFactory 
    {
        public ILogger CreateLogger()
        {
            //Business to decide factory
            return new LogNetLogger();
        }
    }
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }
    public interface ILogger
    {
        void Log();
    }
    public class EdLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with EdLogger");
        }
    }
    public class LogNetLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with LogNetLogger");
        }
    }
    public class CustomerManager
    {
        private ILoggerFactory _loggerFactory;

        public CustomerManager(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Save()
        {
            Console.WriteLine("Saved!");

            ILogger logger = _loggerFactory.CreateLogger();

            logger.Log();


        }
    }
}
