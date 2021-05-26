using System;
using System.Collections.Generic;

namespace TestUserInterface
{
    public class Customer
    {
        public string username;
        protected string contact_details;
        protected string password;


        protected List<Properties> properties = new List<Properties>();
        // register account
        public Customer(string username, string contact_details, string password) 
        {
            this.username = username;
            this.contact_details = contact_details;
            this.password = password;
        }

        public void AddProp(Properties property){
            this.properties.Add(property);
        }

        // login
        public bool Auth(string password){
            if (this.password == password){
                 return true;
            }else{
                return false;
            }
        }

        public string[] getContactDetails(){
                string[] info = {username,contact_details};
                return info;
         
        }

        public string name(){
                return username;
           
        }

        public void ListAllMyProperty(){
            foreach (Properties prop in properties)
            {
                Console.WriteLine($"{prop.listAll()}");
            }
        }

      

    }
}