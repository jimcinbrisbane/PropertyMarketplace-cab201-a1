using System;
using System.Collections.Generic;

namespace TestUserInterface
{
    public class Customer
    {
        public string username;
        protected string contact_details;
        protected string password;

        public bool isAuth;

        protected List<Properties> properties;

        // register account
        public Customer(string username, string contact_details, string password) 
        {
            this.username = username;
            this.contact_details = contact_details;
            this.password = password;
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
            if (this.isAuth == true){
                string[] info = {username,contact_details};
                return info;
            }else{
                string[] err = {username,"not authed"};
                return err;
            }
        }

        public string name(){
            if (this.isAuth == true){
                return username;
            }else{
                string err = "err";
                return err;
            }
        }

      

    }
}