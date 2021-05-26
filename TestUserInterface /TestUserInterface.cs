using System;
using System.Collections.Generic;
using System.Linq;
namespace TestUserInterface
{
    class TestUserInterface
    {
        Menu menu;
        List<Properties> prop;

        List<Customer> customer;
        List<Customer> current;
        List<Bid> bids;

        const string PROPS = "properties";
        const string CUSTOMER = "customers";

        public TestUserInterface()
        {
            menu = new Menu();
            prop = new List<Properties>();
            customer = new List<Customer>();
            current = new List<Customer>();
            bids = new List<Bid>();
        }

        public void AddUser()
        {

            string username  = UserInterface.GetInput("Enter your name");
            string contact_details  = UserInterface.GetInput("Enter your contact details");
            string password = UserInterface.GetPassword("Enter your Password");
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(contact_details)){
            customer.Add(new Customer(username,contact_details,password));
            UserInterface.Message($"User {customer[0].name()} registered");
            }else{
                UserInterface.Message($"username, contacts or password cannot be empty");
            }

        }

        public void LoginUser()
        {
            string username  = UserInterface.GetInput("Enter your name");
            string password = UserInterface.GetPassword("Enter your Password");
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)){
            try {
                current.Add(customer.Find(x => x.username == username && x.Auth(password)));
                UserInterface.Message($"Welcome {current[0].name()}");
                }
            catch{
                current.Clear();
                UserInterface.Message($"username or password error, please try again or create a new account");
            }
            }else{
                UserInterface.Message($"username, password cannot be empty");
            }

        }

        public void Logout(){
            customer[customer.FindIndex(ind=>ind.name() == current[0].name())] = current[0];
            current.Clear();
            UserInterface.Message($"user logged out");
        }

        public void PropMenu()
        {
            Menu submenu = new Menu();
            submenu.Add("Register a new land for sale", AddLand);
            submenu.Add("Register a new house for sale", AddHouse);
            submenu.Add("List my properties",  ListAllMyProperty);
            submenu.Add("!List bids received for a property", ListBidBuyer);
            submenu.Add("!Sell one of my properties to the highest bidder", SellProp);
            submenu.Add("Search for a property for sale", SearchPostCode);
            submenu.Add("!Place a bid on a property", SearchBidtoPlace);
            submenu.Add("Logout", Logout);
            submenu.Display();
        }

        public void AddLand()
        {
            string address  = UserInterface.GetInput("Enter your address");
            if (!string.IsNullOrEmpty(address) && Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int postcode) && Int32.TryParse(UserInterface.GetInput("Enter the size"), out int size) ){
                Land place = new Land(current[0].name(), address, postcode, size);
                prop.Add(place);
                current[0].AddProp(place);
                UserInterface.Message($"{place.listAll()}");
            }else{
                UserInterface.Message($"Please enter correct information.");
            }
        }
        public void AddHouse()
        {
            string address  = UserInterface.GetInput("Enter your address");
            string desc  = UserInterface.GetInput("Enter a description");
            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(desc) && Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int postcode)){
                House place = new House(current[0].name(), address, postcode, desc);
                prop.Add(place);
                current[0].AddProp(place);
                UserInterface.Message($"{place.listAll()}");
            }else{
                UserInterface.Message($"Please enter correct information.");
            }
        }

        public void SearchPostCode(){
            if (Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int pCode)){
                    List<Properties> result = new List<Properties>(prop.FindAll( x => x.postcode == pCode ));
                    Menu search = new Menu();
                    foreach(Properties p in result)
                        {
                            search.Add($" {p.listAll()}", PropMenu);

                        }
                    search.Display();

            }else{
                UserInterface.Message($"Please enter correct postcode.");
            }
        }
        public void SellProp(){
             Menu search = new Menu();
                    foreach (Properties prop in current[0].properties)
                    {
                            search.Add($" {prop.listAll()}", new Action(() => Sell(prop)));
                    }

                    search.Display();
        }

        public void Sell(Properties prop){
            List<Bid> result = new List<Bid>(bids.FindAll(x => x.listAll() == prop.listAll()) );
            // List<int> values = new List<int>();
            // result.ForEach(i => values.Add(i.bid_amount()));
            // int max = values.Max(t => t.Age);
            result.Max(t => t.bid_amount());

        }

        public void SearchBidtoPlace(){
            if (Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int pCode)){
                    List<Properties> result = new List<Properties>(prop.FindAll( x => x.postcode == pCode ));
                    Menu search = new Menu();
                    foreach(Properties p in result)
                        {
                            search.Add($" {p.listAll()}", new Action(() => PlaceBid(p)));

                        }
                    search.Add($"Go back", PropMenu);
                    search.Display();

            }else{
                UserInterface.Message($"Please enter correct postcode.");
            }
        }
        public void PlaceBid(Properties bidprop)
        {   if (Int32.TryParse(UserInterface.GetInput($"How much can you bid {bidprop.address} ?"), out int amount)){
                Bid make = new Bid(current[0].name(), amount, bidprop);
                bids.Add(make);
                UserInterface.Message($"{current[0].name()}, you had bid {bidprop.address} for ${amount}");
            }else{
                UserInterface.Message($"Please enter interger value of money.");
            }
        }
        public void ListBidBuyer(){
             Menu search = new Menu();
                    foreach (Properties prop in current[0].properties)
                    {
                            search.Add($" {prop.listAll()}", new Action(() => ListBid(prop)));
                    }

                    search.Display();
                    search.Add($"Go back", PropMenu);
        }
        public void ListBid(Properties bidprop)
        {
            List<Bid> result = new List<Bid>(bids.FindAll( x => x.listAll() == bidprop.listAll() ));
             Menu bidz = new Menu();
             foreach (Bid prop in result)
                    {
                            bidz.Add($"{prop.listAll()}", PropMenu);
                    }
                    bidz.Add($"Go back", PropMenu);
                    bidz.Display();
        }
        public void ListAllMyProperty(){
                    Menu search = new Menu();
                    foreach (Properties prop in current[0].properties)
                    {
                            search.Add($"{prop.listAll()}", PropMenu);
                    }
                    search.Add($"Go back", PropMenu);
                    search.Display();

        }

        public void Run()
        {
            menu.Add("Register", AddUser);
            menu.Add("Login", LoginUser);
            DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            while (true){
                if (current.Count == 0){
                menu.Display();
                }else{
                    PropMenu();
                }
                }
        }
        static void Main(string[] args)
        {
            TestUserInterface test = new TestUserInterface();
            test.Run();
        }
    }
}
