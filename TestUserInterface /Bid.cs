using System;
namespace TestUserInterface
{
    public class Bid
    { 

        protected Properties propID;
        protected string username;
        protected int amount;

        protected string contact_details;
        public Bid(string username, int amount, Properties propID, string contact_details) 
        {
            this.username = username;
            this.amount = amount > 0 ? amount : 0;
            this.propID = propID;
            this.contact_details = contact_details;
        }
        public string listAll(){
            return this.propID.listAll();

        }

        public int bid_amount(){
            return this.amount;
        }

    private double showTaxPayable() 
        {
            return propID.tax(amount);
    }

    public string view_bid(){
        return $"user {this.username} had bid for ${this.amount}, which means they will pay ${showTaxPayable()} in tax, please contact them at {this.contact_details}";
    }

    public Properties showProp(){
        return this.propID;
    }
}
}