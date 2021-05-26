using System;
namespace TestUserInterface
{
    public class Bid
    { 

        protected Properties propID;
        protected string username;
        protected int amount;
        public Bid(string username, int amount, Properties propID) 
        {
            this.username = username;
            this.amount = amount > 0 ? amount : 0;
            this.propID = propID;
        }
        public string listAll(){
            return this.propID.listAll();

        }

        public int bid_amount(){
            return this.amount;
        }

    public double showTaxPayable() 
        {
            return this.amount * 0.05;
            

    }
}
}