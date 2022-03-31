using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            //AbstractFactory: toplu nesne kullanımında nesnelerin kullanımını kolaylaştırmak hem de standart nesnelere ihtiyaç duyuluyorsa onlar için bir mantık oluşturmakta kullanılır!

            //temel olarak 2 nesne oluşturdum! bunu inherit eden istediğim kadar nesne oluşturabilirim!

            //business katmanında yoğun olarak kullanılır!!

            //bu methodların kombinasyonlarını yapacak bir fabrikaya yani abstract facory'e ihtiyacım var!

            ProductManager productManager = new ProductManager(new Factory1());

            productManager.GetAll();

            Console.ReadLine();
        }
    }
    public abstract class Logging
    {
        public abstract void Log(string message);
    }

    public class Log4NetLogger : Logging //Logging i inherit eden bir sınıf oluşturdum!
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with log4net");
        }
    }

    public class NLogger : Logging //Logging i inherit eden bir sınıf oluşturdum!
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with Nlogger");
        }
    }
    public abstract class Caching
    {
        public abstract void Cache(string data);
    }
    public class MemCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with MemCache");
        }
    }
    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with Redis");
        }
    }
    public abstract class CrossCuttingConcernsFactory //class soyut içindekiler soyutsa burdan yeni fabrikalar üretebilirim!!!!
    {
        public abstract Logging CreateLogger();

        public abstract Caching CreateCaching();
    }
    public class Factory1 : CrossCuttingConcernsFactory //bunu yaptığım anda ikisi için method üretmeiş oluyorum!!!
    {
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new Log4NetLogger();
        }
    }
    public class Factory2 : CrossCuttingConcernsFactory //bunu yaptığım anda ikisi için method üretmeiş oluyorum!!!
    {
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new NLogger();
        }
    }
    public class ProductManager //kullanıcının kullanacayı factory'i kullanıcıya göre belirlemiş oldum!
    {

        CrossCuttingConcernsFactory _crossCuttingConcernsFactory;

        private Logging _logging;

        private Caching _caching;

        public ProductManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory)
        {
            this._crossCuttingConcernsFactory = crossCuttingConcernsFactory;

            this._logging = _crossCuttingConcernsFactory.CreateLogger();

            this._caching = _crossCuttingConcernsFactory.CreateCaching();
        }

        public void GetAll()
        {
            _logging.Log("Logged!");

            _caching.Cache("Data");

            Console.WriteLine("Products listed!");
        }

    }
}
