using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            //**Bir nesne örneğinden sadece bir kere üretilip,bu nesne örneğinin her zaman kullanılmasını öngören bir patterndir!

            //**Ne zaman kullanmamalıyız? => Singleton üretmeden önce gerçekten herkes kullanacak mı diye düşünmeliyiz!

            //**Nadir kullanılan nesneyi Singleton olarak üretmemelisin!

            //**Önce PRIVATE BIR CONSTRUCTOR oluşturursun! akabinde yapılması gereken singleton örneğini oluşturacak STATIC METOD!!!

            var customerManager = CustomerManager.CreateAsSingleton(); //bu şekilde nesnenin sadece 1 kere üretildiğinden emin olmuş oluyoruz!
            customerManager.Save();

            //**Factory design patterniyle ortak çalışma gerçekleştirip, direkt o nesne üzerinden singleton üretme işlemi yapabiliriz!

            //çok nadir de olsa multi tasking çalışılan ortamlarda aynı nesne aynı anda oluşturulursa 2 tane üretilmiş olur bunun için THREAD SAFE SINGLETON! LOCKLAMA İŞLEMİ YAPILIR BUNUN ÖNÜNE GEÇEBİLMEK ADINA!
        }

        class CustomerManager
        {
            private static CustomerManager _customerManager; //field de static olacak!

            static object _lockObject = new object(); //static bir obje ürettim!

            private CustomerManager()
            {

            }
            public static CustomerManager CreateAsSingleton() //bir bak customer manager daha önce oluşturulmuş mu? oluşturulmamışsa yenisini döndürür, oluşturulmuşsa zaten _customerManger'ı if bloğuna girmeden döndürür!
            {
                lock (_lockObject)
                {
                    if (_customerManager == null)
                    {
                        _customerManager = new CustomerManager();

                    }
                }
                return _customerManager;
            }
            public void Save() //business metodunu bu şekilde yazmayı tercih ederiz!
            {
                Console.WriteLine("Saved");
            }
        }
    }
}
