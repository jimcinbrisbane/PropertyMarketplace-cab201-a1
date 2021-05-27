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

    private double showTaxPayable() 
        {
            return this.amount * 0.05;
            

    }

    public string view_bid(){
        return $"user {this.username} had bid for {this.amount}, which means it will pay ${showTaxPayable()} tax";
    }

    public Properties showProp(){
        return this.propID;
    }
}
}