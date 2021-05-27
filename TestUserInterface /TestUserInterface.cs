
// init namespace
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestUserInterface
{
    class TestUserInterface
    {
        //init menu
        Menu menu;

        //import lists of classes for in-memory storage
        List<Properties> prop;

        List<Customer> customer;
        List<Customer> current;
        List<Bid> bids;

        public TestUserInterface()
        {
            //import lists of classes for in-memory storage
            menu = new Menu();
            prop = new List<Properties>();
            customer = new List<Customer>();
            current = new List<Customer>();
            bids = new List<Bid>();
        }

// register user to customer list
        public void AddUser()
        {

            string username  = UserInterface.GetInput("Enter your name");
            string contact_details  = UserInterface.GetInput("Enter your contact details");
            string password = UserInterface.GetPassword("Enter your Password");

            //check if all filed are valid
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(contact_details)){
            customer.Add(new Customer(username,contact_details,password));
            UserInterface.Message($"User {username} registered");
            }else{
                UserInterface.Message($"username, contacts or password cannot be empty");
            }

        }
// login user
        public void LoginUser()
        {
            string username  = UserInterface.GetInput("Enter your name");
            string password = UserInterface.GetPassword("Enter your Password");
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)){
                // if all filed are valid, try to find the user and match password
            try {
                current.Add(customer.Find(x => x.name() == username && x.Auth(password)));
                UserInterface.Message($"Welcome {current[0].name()}");
                }
            catch{
                // if username not found or password is incorrect, clean current user list
                current.Clear();
                UserInterface.Message($"username or password error, please try again or create a new account");
            }
            }else{
                UserInterface.Message($"username, password cannot be empty");
            }

        }
        public void Logout(){
            //  update all changes from current to customer list
            customer[customer.FindIndex(ind=>ind.name() == current[0].name())] = current[0];
            UserInterface.Message($"{current[0].name()} logged out");
            // clean filed
            current.Clear();
        }

        //submenu
        public void PropMenu()
        {
            Menu submenu = new Menu();
            submenu.Add("Register a new land for sale", AddLand);
            submenu.Add("Register a new house for sale", AddHouse);
            submenu.Add("List my properties",  ListAllMyProperty);
            submenu.Add("List bids received for a property", ListBidBuyer);
            submenu.Add("Sell one of my properties to the highest bidder", SellProp);
            submenu.Add("Search for a property for sale", SearchPostCode);
            submenu.Add("Place a bid on a property", SearchBidtoPlace);
            submenu.Add("Logout", Logout);
            submenu.Display();
        }

// Register a new land for sale
        public void AddLand()
        {
            string address  = UserInterface.GetInput("Enter your address");
            if (!string.IsNullOrEmpty(address) && Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int postcode) && Int32.TryParse(UserInterface.GetInput("Enter the size"), out int size) ){
                Land place = new Land(current[0].name(), address, postcode, size);
                // if all filed valid, add to properties list in the main memory 
                prop.Add(place);
                // save it to current user as well, for faster display
                current[0].AddProp(place);
                UserInterface.Message($"Add new land {place.listAll()}");
            }else{
                UserInterface.Message($"Please enter correct information.");
            }
        }

        //add new house
        public void AddHouse()
        {
            string address  = UserInterface.GetInput("Enter your address");
            string desc  = UserInterface.GetInput("Enter a description");
            // if all filed valid, add to properties list in the main memory 
            // save it to current user as well, for faster display in ListAllMyProperty and ListBidBuyer
            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(desc) && Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int postcode)){

                House place = new House(current[0].name(), address, postcode, desc);
                prop.Add(place);
                current[0].AddProp(place);
                UserInterface.Message($"Add new house, {place.listAll()}");
            }else{
                UserInterface.Message($"Please enter correct information.");
            }
        }


// search by postcode, list all properties that matched the postcode
        public void SearchPostCode(){
            if (Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int pCode)){
                    List<Properties> result = new List<Properties>(prop.FindAll( x => x.post() == pCode ));
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
                    //list all the properties user had and if user made a selection it will diect it to the lambda function sell() with a param of the selected property
                    {
                            search.Add($" {prop.listAll()}", new Action(() => Sell(prop)));
                    }

                    search.Display();
        }

        private void Sell(Properties property){
            // this function search for all bids for the properties selected from SellProp()
            try{
                // find all bid, get the highest, save the property object, 
                // mark it as sold, update it in property memory and current customer memory
                List<Bid> result = new List<Bid>(bids.FindAll(x => x.listAll() == property.listAll()) );
                result.Max(t => t.bid_amount());
                Properties sold = result[0].showProp();
                sold.sold(result[0].view_bid());
                prop[prop.FindIndex(ind=>ind.listAll() == result[0].listAll())] = sold;
                current[0].properties[current[0].properties.FindIndex(ind=>ind.listAll() == result[0].listAll())] = sold;
                UserInterface.Message($"Sold to {result[0].view_bid()}");
                }catch{
                        UserInterface.Message($"Currently there are no bids");
                }

        }

        public void SearchBidtoPlace(){
            if (Int32.TryParse(UserInterface.GetInput("Enter your postcode"), out int pCode)){
                //search property by postcode and then let the user place a bid using lambda action PlaceBid
                    List<Properties> result = new List<Properties>(prop.FindAll( x => x.post() == pCode ));
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
        private void PlaceBid(Properties bidprop)
        {   if (Int32.TryParse(UserInterface.GetInput($"How much can you bid {bidprop.addi()} ?"), out int amount)){
            //make a bid for the property selected in SearchBidtoPlace
                Bid make = new Bid(current[0].name(), amount, bidprop, current[0].getContactDetails());
                bids.Add(make);
                UserInterface.Message($"{current[0].name()}, you had bid {bidprop.addi()} for ${amount}");
            }else{
                UserInterface.Message($"Please enter interger value of money.");
            }
        }
        public void ListBidBuyer(){
             Menu search = new Menu();
             // create menu to  list all current user's property
                    foreach (Properties prop in current[0].properties)
                    {
                            search.Add($" {prop.listAll()}", new Action(() => ListBid(prop))); // link to list bid
                    }

                    search.Display();
                    search.Add($"Go back", PropMenu);
        }
        public void ListBid(Properties bidprop)
        {
            //list all bid for the property selected in ListBidBuyer
            List<Bid> result = new List<Bid>(bids.FindAll( x => x.listAll() == bidprop.listAll() ));
             Menu bidz = new Menu();
             foreach (Bid prop in result)
             // see the user, amount and taxpayable
                    {
                            bidz.Add($"{prop.view_bid()}", PropMenu);
                    }
                    bidz.Add($"Go back", PropMenu);
                    bidz.Display();
        }
        public void ListAllMyProperty(){
                // create menu to  list all current user's property
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
