using System;
namespace TestUserInterface
{
    public abstract class Properties
    {
        protected string address;
        protected int postcode;
        protected string username_FK;
        protected string sold_to;

        public virtual int post(){
            return this.postcode;
        }
        public virtual string addi(){
            return this.address;
        }
        public virtual string sold(string sold_user){
            sold_to = sold_user;
            return "sold to {sold_user}";
        }

        public virtual double tax(int money){
            return money;
        }

        public virtual string listAll()
    {
            return $"{this.address},{this.postcode}, belongs to {this.username_FK}";
    }




    }




    public class Land : Properties
    {
        protected int size;

    public override string listAll()
    {
        
            return $" Land {this.address},{this.postcode},belongs to {this.username_FK}, is {this.size} m^2";
      
    }
    public override double tax(int money){
            return Math.Round(this.size * 5.50);
        }

     
    public Land(string username_FK, string address, int postcode, int size)
    {
        this.username_FK = username_FK;
        this.address = address;
        this.postcode = postcode;
        this.size = size;
    }
    }
    public class House : Properties
    {
        protected string desc;
    public House(string username_FK, string address, int postcode, string desc )
    {
        this.username_FK = username_FK;
        this.address = address;
        this.postcode = postcode;
        this.desc = desc;
    }
    public override double tax(int money){
            return Math.Round(money * 0.1);
            //get tax for bid
        }
    public override string listAll()
        {
           
            return $"The House {this.address},{this.postcode}, belongs to {this.username_FK}: \n {this.desc}";
           
        }
    }
    }