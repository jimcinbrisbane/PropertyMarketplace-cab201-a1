using System;
using System.Collections.Generic;

namespace TestUserInterface
{
    class TestUserInterface
    {
        Menu menu;
        List<Properties> prop;
        List<Land> land;
        List<House> house;

        List<Customer> customer;
        List<Customer> current;

        const string PROPS = "properties";
        const string LANDS = "lands";
        const string HOUSE = "houses";
        const string CUSTOMER = "customers";

        public TestUserInterface()
        {
            menu = new Menu();
            prop = new List<Properties>();
            land = new List<Land>();
            house = new List<House>();
            customer = new List<Customer>();
            current = new List<Customer>();
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
            current.Clear();
            UserInterface.Message($"user logged out");


        }

        public void PropMenu()
        {
            Menu submenu = new Menu();
            submenu.Add("Register a new land for sale", AddLand);
            submenu.Add("Register a new house for sale", AddLand);
            submenu.Add("List my properties", AddLand);
            submenu.Add("List bids received for a property", AddLand);
            submenu.Add("Sell one of my properties to the highest bidder", AddLand);
            submenu.Add("Search for a property for sale", AddLand);
            submenu.Add("Place a bid on a property", AddLand);
            submenu.Add("Logout", Logout);
            submenu.Display();
            
        }

        public void AddLand()
        {
            string username  = UserInterface.GetInput("Enter your name");
            string contact_details  = UserInterface.GetInput("Enter your contact details");
            string password = UserInterface.GetPassword("Enter your Password");
            land.Add(new Land(username, "256 main street", 4000, 300));
            UserInterface.Message($"Adding an land");
        }
        
        // public void RemoveApple()
        // {
        //     UserInterface.Message($"Remove apple");
        //     string apple = UserInterface.ChooseFromList(apples);
        //     apples.Remove(apple);
        // }

        // public void DisplayApples()
        // {
        //     UserInterface.DisplayList("This is your apples", apples);
        // }



        public void Run()
        {
            menu.Add("Enter name/password", AddUser);
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
