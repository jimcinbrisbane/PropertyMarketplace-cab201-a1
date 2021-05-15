using System;
using System.Collections.Generic;

namespace PropertyMarketPlace
{
    public class Customer
    {
        protected string username;
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
            this.isAuth = false;
        }

        // login
        public string Auth(string password){
            if (this.password == password){
                 this.isAuth = true;
                 return $"Welcome, {this.username}";
            }else{
                return $"username or password error";
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

        public string Logout (){
            if (this.isAuth == true){
                this.isAuth = false;
                return $"{this.username} had logged out";
            }else{
                return $"user not logged in";
            }
        }

    }
}
