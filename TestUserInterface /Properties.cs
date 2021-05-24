using System;
namespace TestUserInterface
{
    public abstract class Properties
    {
        protected int id;
        protected string address;
        protected int postcode;
        public string username_FK{ get; set; }

        public virtual string listAll(string username)
    {
        
            return $"{id},{address},{postcode},{username_FK}";
       
    }

    }


    public class Land : Properties
    {
        protected int size;

    public override string listAll(string username)
    {
        
            return $"{id},{address},{postcode},{username_FK},{size}";
      
    }
    public Land(string username_FK, string address, int postcode, int size)
    {
        Console.WriteLine( "Created");
    }
    }
    public class House : Properties
    {
        protected string desc;
    public House(string username_FK, string address, int postcode, string desc )
    {
        Console.WriteLine("Created");
    }
    public override string listAll(string username)
        {
           
                return $"{id},{address},{postcode},{username_FK},{desc}";
           
        }
    }
    }