using System;
namespace PropertyMarketPlace
{
    public abstract class Properties
    {
        protected int id;
        protected string address;
        protected int postcode;
        public Customer username_FK{ get; set; }

        public virtual string listAll(Customer username)
    {
        if(username_FK == username){
            return $"{id},{address},{postcode},{username_FK}";
        }else{
            return "false";
        }
    }

    }


    public class Land : Properties
    {
        protected int size;

    public override string listAll(Customer username)
    {
        if(username.isAuth){
            return $"{id},{address},{postcode},{username_FK},{size}";
        }else{
            return "false";
        }
    }
    public Land(Customer username_FK, string address, int postcode, int size)
    {
        Console.WriteLine( "Created");
    }
    }
    public class House : Properties
    {
        protected string desc;
    public House(Customer username_FK, string address, int postcode, string desc )
    {
        Console.WriteLine("Created");
    }
    public override string listAll(Customer username)
        {
            if(username.isAuth){
                return $"{id},{address},{postcode},{username_FK},{desc}";
            }else{
                return "false";
            }
        }
    }
    }
