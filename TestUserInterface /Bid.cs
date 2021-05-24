using System;
namespace TestUserInterface
{
    public class Bid
    { 
        protected int id;

        protected Properties propID;
        protected Customer username;
        protected int amount;
        public Bid(int id, Customer username, int amount, Properties propID) 
        {
            this.id = id;
            this.username = username;
            this.amount = amount > 0 ? amount : 0;
            this.propID = propID;
        }

        public int viewAllBids(Properties propID) 
        {
            if(propID == this.propID){
                return this.amount;
            }else{
                return -1;
            }
            

    }
    public double showTaxPayable(int amount) 
        {
            return amount * 0.05;
            

    }
}
}