using System;
namespace TestUserInterface
{
    public abstract class Properties
    {
        protected string address;
        protected int postcode;
        public string username_FK{ get; set; }

        public virtual string listAll()
    {
        
            return $"{this.address},{this.postcode},{this.username_FK}";
       
    }

    }


    public class Land : Properties
    {
        protected int size;

    public override string listAll()
    {
        
            return $"{this.address},{this.postcode},{this.username_FK},{this.size}";
      
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
    public override string listAll()
        {
           
            return $"{this.address},{this.postcode},{this.username_FK},{this.desc}";
           
        }
    }
    }