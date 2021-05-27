using System;
using System.Collections.Generic;

namespace TestUserInterface
{
    public class Customer
    {
        protected string username;
        protected string contact_details;
        protected string password;
        public List<Properties> properties = new List<Properties>();
        // it's public so we don't need a find function 
        //everytime we tryna get user's listed properties, would not do this if we have an sql

        // register account


        //Constructor
        public Customer(string username, string contact_details, string password) 
        {
            this.username = username;
            this.contact_details = contact_details;
            this.password = password;
        }
        



// add prop
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

        public string getContactDetails(){
                return this.contact_details;
         
        }

        public string name(){
                return this.username;
           
        }


      

    }
}