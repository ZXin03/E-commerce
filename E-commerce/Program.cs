// See https://aka.ms/new-console-template for more information

using ConsoleTables;
using E_commerce.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Cryptography.X509Certificates;
using System.Text;

AppDBcontext db = new AppDBcontext();

while (true)
{
    Console.WriteLine("1.Admin ");
    Console.WriteLine("2.Seller");
    Console.WriteLine("3.Customer");
    string opt = Console.ReadLine();
    switch (opt)
    {
        case "1":
            while (true)
            {
                Console.WriteLine("Enter [Q] To Quit");
                Console.WriteLine("Entry Admin Name:");
                string adminname = Console.ReadLine();
                Console.WriteLine("Entry Admin Password");
                string password = Console.ReadLine();
                if (adminname.ToUpper() == "Q")
                {
                    break;
                }
                if (adminname == "Admin" && password == "123")
                {
                    Console.WriteLine("Wrong Login");
                    continue;
                }
                Console.Clear();
                Console.WriteLine("welcome Admin");

                bool adminmenu = true;
                while (true)
                {
                    Console.WriteLine("1.create New Customer");
                    Console.WriteLine("2.Manage Customers");
                    Console.WriteLine("3.create New Seller");
                    Console.WriteLine("4.Manage Seller");
                    Console.WriteLine("5.Manage Products");
                    Console.WriteLine("0.Logout");
                    Console.WriteLine("Please Enter Your Option:");
                    string adminopt = Console.ReadLine();
                    Console.Clear();
                    switch (adminopt)
                    {
                        case "1":
                            Address newaddress = new Address();
                            newaddress.Address1 = "Address1";
                            db.addresses.Add(newaddress);
                            db.SaveChanges();

                            Console.WriteLine("Create New User:");
                            user newuser = new user();
                            Console.WriteLine("Create User Name:");
                            newuser.UserName = Console.ReadLine();
                            Console.WriteLine("Enter Password:");
                            newuser.Password = Console.ReadLine();
                            newuser.LoginTry = 3;
                            newuser.LastLoginTime = DateTime.Now;
                            newuser.Status = "Active";
                            Address newadress = new Address();
                            newuser.AddressId = db.addresses.Last().AddressId;
                            db.customers.Add(newuser);
                            db.SaveChanges();
                            break;
                        case "2":
                            var usertable = new ConsoleTable("Id", "Seller User Name", "Last Login Time", "Login try", "Status");
                            foreach (var u in db.sellers)
                            {
                                usertable.AddRow(u.SellerId, u.SellerUserName, u.LastLoginTime, u.loginTry, u.Status);
                            }
                            usertable.Write();
                            Console.ReadKey();
                            break;
                        case "3":
                            Address newselleraddress = new Address();
                            newselleraddress.Address1 = "Address1";
                            db.addresses.Add(newselleraddress);
                            db.SaveChanges();

                            Console.WriteLine("Create seller");
                            Seller newseller = new Seller();
                            Console.WriteLine("Enter Seller Name");
                            newseller.SellerUserName = Console.ReadLine();
                            Console.WriteLine("Enter Seller Password");
                            newseller.SellerPassword = Console.ReadLine();
                            newseller.loginTry = 3;
                            newseller.LastLoginTime = DateTime.Now;
                            newseller.Status = "Active";
                            newseller.AddressId = db.addresses.Last().AddressId;
                            db.sellers.Add(newseller);
                            db.SaveChanges();
                            break;
                        case "4":
                            var sellertable = new ConsoleTable("Id", "Seller User Name", "Last Login Time", "Login try", "Status");
                            foreach (var s in db.sellers)
                            {
                                sellertable.AddRow(s.SellerId, s.SellerUserName, s.LastLoginTime, s.loginTry, s.Status);
                            }
                            sellertable.Write();

                            Console.ReadKey();
                            break;
                        case "5":
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("please Enter Option 0-5");
                            break;
                    }
                }
            }
            break;
        case "2":
            Console.WriteLine("seller login");
            var findseller = findsellers();

            Console.WriteLine($"Welcome Back {findseller.SellerUserName}Last Login Time {findseller.LastLoginTime}");
            findseller.LastLoginTime = DateTime.Now;
            findseller.LoginTry = 3;
            db.SaveChanges();

            bool sellermenu = true;
            while (sellermenu)
            {
                Console.WriteLine("1.Create Category");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Display Product");
                Console.WriteLine("4.Manage product");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Please Enter Your Option:");
                string selleropt = Console.ReadLine();
                switch (selleropt)
                {
                    case "1":
                        Console.WriteLine("Create Catgery");
                        Category newcategory = new Category();
                        Console.WriteLine("Enter Category Name");
                        newcategory.CategoryName = Console.ReadLine();
                        Console.WriteLine("Enter Category Desription");
                        newcategory.CategoryDescription = Console.ReadLine();
                        db.categories.Add(newcategory);
                        db.SaveChanges();
                        break;
                    case "2":
                        if (db.categories.Count() == 0)
                        {
                            Console.WriteLine("Please Add category First");
                        }
                        else
                        {
                            Console.WriteLine("Create Product");
                            Products newproduct = new Products();
                            Console.WriteLine("Enter Product Name");
                            newproduct.ProductName = Console.ReadLine();
                            Console.WriteLine("Enter Description");
                            newproduct.Description = Console.ReadLine();
                            Console.WriteLine("Enter Product Price");
                            newproduct.ProductPrice = Convert.ToDecimal(Console.ReadLine());
                            Console.WriteLine("Enter Discount Price");
                            newproduct.DiscountPrice = Convert.ToDecimal(Console.ReadLine());
                            Console.WriteLine("Enter Stock");
                            newproduct.Stock = Convert.ToInt32(Console.ReadLine());
                            while (true)
                            {
                                var categorytable = new ConsoleTable("CategoryId", "Category Name", "Description");
                                foreach (var c in db.categories)
                                {
                                    categorytable.AddRow(c.CategoryId, c.CategoryName, c.CategoryDescription);
                                }
                                categorytable.Write();
                                Console.WriteLine("Please Select Category");
                                string categoryid = Console.ReadLine();
                                var findcategory = db.categories.FirstOrDefault(c => c.CategoryId.ToString() == categoryid);
                                if (findcategory == null)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Please Enter Valid CategoryId");
                                    continue;
                                }
                                newproduct.CategoryId = findcategory.CategoryId;
                                break;

                            }
                            newproduct.SellerId = findseller.SellerId;
                            db.products.Add(newproduct);
                            db.SaveChanges();
                        }
                        break;
                    case "3":
                        var producttable = new ConsoleTable("Id", "Product Name", "Price", "Discount price");
                        foreach (var c in db.products)
                        {
                            producttable.AddRow(c.CategoryId,c.ProductName,c.ProductPrice,c.DiscountPrice);
                        }
                        producttable.Write();
                        Console.ReadKey();
                        break;
                }
            }
            break;

        case "3":
            var finduser = findcustomer();

            Console.WriteLine($"Welcome Back{finduser.UserName} LAst Login Time {finduser}");
            finduser.LastLoginTime = DateTime.Now;
            finduser.LoginTry = 3;
            db.SaveChanges();
            Console.WriteLine("1.Display All Active products");
            Console.WriteLine("2.Add To Cart ");
            Console.WriteLine("3.Show Cart List and Check Out");
            Console.WriteLine("Please Enter Your Option");
            string customeropt = Console.ReadLine();
            switch (customeropt)
            {
                case "1":
                    var producttable = new ConsoleTable("Id", "Product Name", "Price", "Discount Price", "");
                    foreach(var p in db.products)
                    {
                        producttable.AddRow(p.CategoryId, p.ProductName, p.ProductPrice, p.DiscountPrice,p.);
                    }
                    producttable.Write();
                    Console.ReadKey();
                    break;

            }
            break;
    }

    user findcustomer()
    {
        while (true)
        {
            Console.WriteLine("Login");
            Console.WriteLine("Enter User Name:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();
            var findusername = db.customers
                .FirstOrDefault(x => x.UserName == username);
            if (findusername == null)
            {
                Console.WriteLine("No User Found");
                continue;
            }
            var finduser = db.customers
                .FirstOrDefault(x => x.UserName == username
                && x.Password == password);
            if (finduser == null)
            {
                Console.WriteLine("Wrong password");
                findusername.LoginTry--;
                if (finduser.LoginTry <= 0)
                {
                    finduser.LoginTry = 0;
                    Console.WriteLine("Account Suspend");
                }
                db.SaveChanges();
                continue;
            }
        }

    }
    user findsellers()
    {
        while (true)
        {

            Console.WriteLine("Login");
            Console.WriteLine("Enter User Name:");
            string SellerUserName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string SellerPassword = Console.ReadLine();
            var findsellername = db.sellers
                .FirstOrDefault(x => x.SellerUserName == SellerUserName);
            if (findsellername == null)
            {
                Console.WriteLine("No User Found");
                continue;
            }
            var findseller = db.sellers
                .FirstOrDefault(x => x.SellerUserName == SellerUserName
                && x.SellerPassword == SellerPassword);
            if (findseller == null)
            {
                Console.WriteLine("Wrong password");
                findsellername.loginTry--;
                if (findseller.loginTry <= 0)
                {
                    findseller.loginTry = 0;
                    Console.WriteLine("Account Suspend");
                }
                db.SaveChanges();
                continue;
            }

        }
    }
}




