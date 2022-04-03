using System;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            //fecade deseni: uygulaması en basit tasarım desenlerindendir! kelime anlamı cephe,dış görünüş!
            //amaç: her bir sınıfa tek tek ulaşmaktansa bunları bir cephede toplayıp erişimi kolaylaştırmak!

            CustomerManager customerManager = new CustomerManager();

            customerManager.Save();

            Console.ReadLine();

        }
        class Logging:ILogging
        {
            public void Log()
            {
                Console.WriteLine("Logged!");
            }
        }

        interface ILogging //default hali internal olduğu için yazmaya gerek yok!
        {
            void Log();
        }

        class Caching:ICaching
        {
            public void Cache()
            {
                Console.WriteLine("Cached!");
            }
        }

        interface ICaching
        {
            void Cache();
        }

        class Validation:IValidate
        {
            public void Validate()
            {
                Console.WriteLine("Validated!");
            }
        }

       interface IValidate
        {
            void Validate();
        }

        class Authorize : IAuthorize
        {
            public void CheckUser()
            {
                Console.WriteLine("User Checked!");
            }
        }

        interface IAuthorize
        {
            void CheckUser();
        }

        class CustomerManager //amacım içinde loglama,caching,authorize ı kullanmak! bunların hepsini crosscuttingconcernsfacade den almak daha derli toplu durmasını sağlayacak!
        {
            private CrossCuttingConcernsFacade _concerns;
            public CustomerManager() //injection yapıldı!
            {
                _concerns = new CrossCuttingConcernsFacade();
            }

            public void Save()
            {
                _concerns.Caching.Cache();
                _concerns.Logging.Log();
                _concerns.Authorize.CheckUser();
                _concerns.Validate.Validate();
                Console.WriteLine("Saved!");
            }
        }

        class CrossCuttingConcernsFacade
        {
            public ILogging Logging;

            public ICaching Caching;

            public IAuthorize Authorize;

            public IValidate Validate;

            public CrossCuttingConcernsFacade()
            {
                Logging = new Logging();

                Caching = new Caching();

                Authorize = new Authorize();

                Validate = new Validation();
            }
        }
    } 
}
