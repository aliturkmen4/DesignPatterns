using System;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prototype : Amaç => nesne üretim maliyetlerini minimize etmek! (sadece ihtiyaçlar dahilinde kullanılabilir!)

            Customer customer1 = new Customer { FirstName = "Ali", LastName = "Türkmen", City = "Antalya", Id = 1 };

            Customer customer2 =(Customer)customer1.Clone();

            customer2.FirstName = "Salih";

            Console.WriteLine(customer1.FirstName);

            Console.WriteLine(customer2.FirstName);

            //customer1 ile customer2 aynı nesneler değil! aynı referansı tutmuyorlar, buradaki kazancım yeni referans oluşturma maliyetinden kurtarmak!

            Console.ReadLine();


        }

        public abstract class Person //prototype'im ana nesne!
        {
            public abstract Person Clone(); //clone türünde abstract methodu olmalı prototype 'ını oluşturabilmek için!

            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public class Customer:Person
        {
            public string City { get; set; } //sadece customer'a ait özellik!

            public override Person Clone() //implemente ettiğim clone methodu customer'ı clonelamama yarar!
            {
                return (Person)MemberwiseClone(); //bu method yardımıyla clonelamayı .net de yapabiliyorum!
            }
        }
        public class Employee : Person
        {
            public decimal Salary { get; set; } //sadece customer'a ait özellik!

            public override Person Clone() //implemente ettiğim clone methodu customer'ı clonelamama yarar!
            {
                return (Person)MemberwiseClone(); //bu method yardımıyla clonelamayı .net de yapabiliyorum!
            }
        }
    }
}
