using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            //adapter design patterni : farklı sistemlerin kendi sistemlerimize entegre edilmesi durumunda,kendi sistemimizin bozulmadan farklı sistemin sanki bizim sistemimizmiş gibi sağlar!

            //ProductManager productManager = new ProductManager(new EdLogger());

            ProductManager productManager = new ProductManager(new Log4NetAdapter());

            //istediğim logger ı böylece new leyip kullanıyorum, dolasıyla nuget tan entegre ettiğim dll dosyası olanı da adapter lı versiyonuyla projemde kullanabilirim!

            productManager.Save();

            Console.ReadLine();
        }
    }

    class ProductManager
    {

        private ILogger _logger; //dependency injection ile ILogger a bağımlılığımı belirtmiş oldum!

        public ProductManager(ILogger logger)
        {
            this._logger = logger;
        }

        public void Save()
        {
            _logger.Log("User Data");
            Console.WriteLine("Saved!");
        }
    }

    interface ILogger
    {
        void Log(string message);
    }

    class EdLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("Logged! {0}",message);
        }
    }

    //NuGet ten indiridğimizi varsayalım! (buna dokunamıyorum dll olarak geldi!)
    class Log4Net
    {
        public void LogMessage(string message)
        {
            Console.WriteLine("Logged with Log4Net! {0}", message);
        }
    }

    class Log4NetAdapter : ILogger //yukarıdaki değiştiremediğim Log4Net yerine bunu kullanacağım!
    {
        public void Log(string message) //ILogger ımın Log, Log4Net için gerekli olan implementasyonu gerçekleştiriyor!
        {
            Log4Net log4Net = new Log4Net();
            log4Net.LogMessage(message);
        }
    }
}
