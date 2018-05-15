using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Asgn05
{
    class Program
    {
        static void Main(string[] args)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            int max;
            int min;

            //ask for max and min
            Console.WriteLine("What is the minimum price that you want to search for?");
              min = int.Parse(Console.ReadLine());


            Console.Write("What is the maximum price you want to search for?");
             max = int.Parse(Console.ReadLine());

            var prdcts = db.PRODUCTs.Where(p => p.P_PRICE > min && p.P_PRICE < max);

            Console.WriteLine($"These are the items priced between {min} and {max}");


            //write products to the screen
            foreach(var products in prdcts)
            {
               
                Console.WriteLine($"P.Code {products.P_CODE}, Desc: {products.P_DESCRIPT}," +
                    $" Price: {products.P_PRICE}, QOH:{products.P_QOH}, V_Code:{products.P_CODE}" +
                    $", Vendor Name:{products.VENDOR.V_NAME}, Vendor Contact {products.VENDOR.V_PHONE}");
            }

            //CREATE A NEW PRODUCT

            Console.WriteLine("Enter the new products name:");
            String name = Console.ReadLine();

            Console.WriteLine("Enter the new products description:");
            String desc = Console.ReadLine();

            Console.WriteLine("What date will the product be in stock?");
            System.DateTime date = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new products price:");
            Decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new products QOH:");
            Decimal qoh = decimal.Parse(Console.ReadLine());

            Console.WriteLine("What is the min of the product?");
            Decimal minn = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new products discount:");
            Decimal discount = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new products code");
            String code = Console.ReadLine();

           int vendorCode;
           VENDOR vendor  = null;

            Console.WriteLine("Enter 1 to choose an exisiting vendor or enter 2 to add a new vendor");
            int choice = int.Parse(Console.ReadLine());


            if(choice == 1)
            {
                Console.WriteLine("What is the vendors code?");
                vendorCode = int.Parse(Console.ReadLine());

                //check if vendor does n fact exist 
               while(!db.VENDORs.Any(v => v.V_CODE.Equals(vendorCode)));
                {
                    Console.WriteLine("Sorry the Vendor code could not befound, please renenter the code");
                    vendorCode = int.Parse(Console.ReadLine());
                }

                //assign the matching vender to variable
               vendor = db.VENDORs.Where(v => v.V_CODE == vendorCode).First();

        

            }
            else
            {

                //create a new vendor
                Console.WriteLine("What is the new vendors code?");
                vendorCode = int.Parse(Console.ReadLine());

                Console.WriteLine("What is the name of the vendor?");
                String vendorName = Console.ReadLine();
                
                Console.WriteLine("Who is the contact at this vendor?");
                String vendorContact = Console.ReadLine();

                Console.WriteLine("What is the area code of the vendor?");
                String vendorArea = Console.ReadLine();

                Console.WriteLine("What is the phone number of the vendor?");
                String vendorPhone = Console.ReadLine();

                Console.WriteLine("What state is the vendor in?");
                String vendorState = Console.ReadLine();

                Console.WriteLine("Does this vendor accpet orders? (Enter Y or N)");
                Char vendorOrders = char.Parse(Console.ReadLine());

                vendor = new VENDOR
                {
                    V_NAME = vendorName,
                    V_CODE = vendorCode,
                    V_AREACODE = vendorArea,
                    V_PHONE = vendorPhone,
                    V_STATE = vendorState,
                    V_CONTACT = vendorContact,
                    V_ORDER = vendorOrders

                };

                //add to database
                db.VENDORs.InsertOnSubmit(vendor);
                db.SubmitChanges();
            }

            //create new product
            PRODUCT pr = new PRODUCT
            {
                P_CODE = code,
                P_DESCRIPT = desc,
                P_PRICE = price,
                P_INDATE = date,
                P_QOH = qoh,
                P_MIN = minn,
                P_DISCOUNT = discount,
            };

            pr.VENDOR = vendor;

            //add new product to database
            db.PRODUCTs.InsertOnSubmit(pr);
            db.SubmitChanges();

            Console.WriteLine("The product has been added to the database");
            Console.ReadKey();
        }
    }
}
