using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public enum PersonTypeEnum
    {
        Customer,
        Supplier,
    }

    public class Person
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public PersonTypeEnum PersonType { get; set; }

        public Person(string name, string id, PersonTypeEnum personType)
        {
            Name = name;
            ID = id;
            PersonType = personType;
        }

        public Person(string name, string id)
        {
            Name = name;
            ID = id;
        }

    }//end class Person

    public class Customer : Person
    {
        public Customer(string name, string id) : base(name, id)
        {
            Name = name;
            ID = id;
            PersonType = PersonTypeEnum.Customer ;
        }

    }

    public class Supplier : Person
    {
        public struct PriceMatrixStruct
        {
            PriceTable priceTable;
            int DeliveryTime;
        }

        public Supplier(string name, string id) : base(name, id)
        {
            Name = name;
            ID = id;
            PersonType = PersonTypeEnum.Supplier;
        }

        public List<PriceMatrixStruct> PriceMatrix { get; set; }
        public double Reliability { get; set; }
    }

}//end nameSpace
