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
        PersonTypeEnum PersonType { get; set; }

        public Person(string name, string id, PersonTypeEnum personType)
        {
            Name = name;
            ID = id;
            PersonType = personType;
        }

    }//end class Person

    public class Customer : Person
    {
        public Customer(string name, string id, PersonTypeEnum personType) : base(name, id, personType)
        {
        }

    }

    public class Supplier : Person
    {
        public struct PriceMatrixStruct
        {
            PriceTable priceTable;
            int DeliveryTime;
        }

        public Supplier(string name, string id, PersonTypeEnum personType) : base(name, id, personType)
        {

        }
        public List<PriceMatrixStruct> PriceMatrix { get; set; }
        public double Reliability { get; set; }
    }

}//end nameSpace
