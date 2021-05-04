using System;
namespace PropertyMarketPlace
{
    public class Customer
    {
        protected string username;
        protected string contact_details;
        protected string password;

        protected bool isAuth;

        // register account
        public Customer(string username, string contact_details, string password) 
        {
            this.username = username;
            this.contact_details = contact_details;
            this.password = password;
            this.isAuth = false;
        }

        // login
        public string Auth(string username, string password){
            if (this.username == username && this.password == password){
                 this.isAuth = true;
                 return $"Welcome, {this.username}";
            }else{
                return $"username or password error";
            }
        }

        public string[] getContactDetails(string username){
            if (this.isAuth == true){
                string[] info = {username,contact_details};
                return info;
            }else{
                string[] err = {username,"not authed"};
                return err;
            }
        }

        public string Logout (string username){
            if (this.isAuth == true){
                this.isAuth = false;
                return $"{username} had logged out";
            }else{
                return $"user not logged in";
            }
        }



        public virtual void Display()
        {
            Console.WriteLine(this.id + " " + this.name + " " + this.salary);
        }

        public override string ToString()
        {
            return this.id + " " + this.name + " " + this.salary;
        }

    }
}
