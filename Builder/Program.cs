using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            //Builder =>
            //amaç: arka arkayta işlemler sonucu bir nesne örneği çıkarmak. genellikle business katmanında!
            //birbiri arkasına atılacak adımların sırayla işlenmesi sonucu ortaya çıkar.
            //genellikle iş katmanlarında ve arayüz katmanlarında kodları if ile yazmak yerine ilgili üreticinin enjekte edilmesi ve ona göre ortaya bir nesnenin çıkarılması şeklinde örneklendirilir!
            //amaç bir model üretip o modeli de birbiri arkası gelecek işlemlere göre oluşturma eylemi!

            ProductDirector director = new ProductDirector();
            var builder = new NewCustomerProdcutBuilder();
            director.GenerateProduct(builder); //yeni müşteri için yapmak istediğimi söyledim!
            var model = builder.GetViewModel();
            Console.WriteLine(model.Id);
            Console.WriteLine(model.CategoryName);
            Console.WriteLine(model.DiscountApplied);
            Console.WriteLine(model.DiscountedPrice);
            Console.WriteLine(model.ProductName);
            Console.WriteLine(model.UnitPrice);

            Console.ReadLine();
        }

        class ProductViewModel
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal DiscountedPrice { get; set; }
            public bool DiscountApplied { get; set; }

        }

        abstract class ProductBuilder //yukarıdakileri üreteceğim business katmanı gibi düşünülebilir! 
                                      //hepsi aynı namespace altında olduğu için public yapmaya gerek yok!
        {
            public abstract void GetProductData();
            public abstract void ApplyDiscount();
            public abstract ProductViewModel GetViewModel();
        }

        class NewCustomerProdcutBuilder : ProductBuilder
        {
            ProductViewModel model = new ProductViewModel();

            public override void ApplyDiscount()
            {
                model.DiscountedPrice = model.UnitPrice*(decimal)0.90; //product data nın çalıştığı varsayılarak bu yapılır!
                model.DiscountApplied = true; //indirim uygulandı mı ? => true yukarıda uyguladım!
            }

            public override void GetProductData()
            {
                model.Id = 1;
                model.CategoryName = "Beverages";
                model.ProductName = "Chai";
                model.UnitPrice = 20;
            }

            public override ProductViewModel GetViewModel()
            {
                return model; //işlemler sonucu ürettiğim modeli döndürüyorum!
            }
        }

        class OldCustomerProductBuilder : ProductBuilder
        {
            ProductViewModel model = new ProductViewModel();
            public override void ApplyDiscount()
            {
                model.DiscountedPrice = model.UnitPrice; //indirim yok demek istedim!
                model.DiscountApplied = false; //indirim uygulandı mı ? => false  yukarıda uygulamadım!
            }

            public override void GetProductData()
            {
                model.Id = 1;
                model.CategoryName = "Beverages";
                model.ProductName = "Chai";
                model.UnitPrice = 20;
            }

            public override ProductViewModel GetViewModel()
            {
                return model; //işlemler sonucu ürettiğim modeli döndürüyorum!
            }
        }

        class ProductDirector
        {
            public void GenerateProduct(ProductBuilder productBuilder) //burada amacım hangi builder'ı kullanacağımı göndermek! //base'i gönderdim!
            {
                productBuilder.GetProductData();
                productBuilder.ApplyDiscount();
            }
        }
    }
}
