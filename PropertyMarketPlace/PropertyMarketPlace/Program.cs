using System;

namespace PropertyMarketPlace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Customer alan = new Customer("Alan Babe", "0481126808", "2002");
            string[]a = alan.getContactDetails();
            foreach(string i in a){
                  Console.WriteLine(i);
            }
            alan.Auth("2002");
            string[]b = alan.getContactDetails();
            foreach(string i in b){
                  Console.WriteLine(i);
            }
            Land bl = new Land(alan, "256 main street", 4000, 300);
            House ba = new House(alan, "659 gerorge st.", 4000, "great amazing house in general, all bills included");
            Customer phone = new Customer("Phone What", "0481126808", "2002");
            phone.Auth("2002");
            Bid baPhone = new Bid(0,phone,2000,ba);
            Bid blPhone = new Bid(1,phone,2000,bl);
            alan.Logout();
        }
    }
}
