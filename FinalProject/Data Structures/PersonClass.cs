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

    public class PersonClass
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public PersonTypeEnum PersonType { get; set; }

        public PersonClass() { }

        public PersonClass(string name, string id, PersonTypeEnum personType)
        {
            Name = name;
            ID = id;
            PersonType = personType;
        }

        public PersonClass(string name, string id)
        {
            Name = name;
            ID = id;
        }

        public PersonClass(string id)
        {
            ID = id;
        }

        public override bool Equals(object obj)
        {
            PersonClass person = (PersonClass)obj;
            return ID.Equals(person.ID);
        }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

    }//end class Person

    public class Customer : PersonClass
    {
        public Customer(string name, string id) : base(name, id)
        {
            Name = name;
            ID = id;
            PersonType = PersonTypeEnum.Customer;
        }


    }

    public class Supplier : PersonClass
    {
        public struct PriceMatrixStruct
        {
            public ProductClass product;
            public double UnitPrice;
            public int LeadTime;
        }

        public List<PriceMatrixStruct> PriceMatrix { get; set; }
        public double Reliability { get; set; }


        public Supplier() : base()
        {
            PersonType = PersonTypeEnum.Supplier;
        }

        public Supplier(string id) : base(id)
        {
            ID = id;
            PersonType = PersonTypeEnum.Supplier;
            PriceMatrix = new List<PriceMatrixStruct>();
        }

        public Supplier(string name, string id) : base(name, id)
        {
            Name = name;
            ID = id;
            PersonType = PersonTypeEnum.Supplier;
            PriceMatrix = new List<PriceMatrixStruct>();
        }
        public Supplier(string name, string id, double reliability) : base(name, id)
        {
            Name = name;
            ID = id;
            Reliability = reliability;
            PersonType = PersonTypeEnum.Supplier;
            PriceMatrix = new List<PriceMatrixStruct>();
        }

        public Supplier(string name, string id, double reliability, List<PriceMatrixStruct> priceMatrix) : base(name, id)
        {
            Name = name;
            ID = id;
            Reliability = reliability;
            PersonType = PersonTypeEnum.Supplier;
            PriceMatrix = priceMatrix;
        }

    }//end class Supplier : PersonClass

    public class SuppliersList
    {
        public List<Supplier> SupplierList { get; set; }

        public SuppliersList()
        {
            SupplierList = new List<Supplier>();
        }

        public void AddSupplier(Supplier supplier)
        {
            SupplierList.Add(supplier);
        }

        public Supplier GetSupploer(Supplier supplier)
        {
            return SupplierList[SupplierList.IndexOf(supplier)];
        }

        public Supplier GetSupploer(string supplierID)
        {
            Supplier supplier = new Supplier(supplierID);
            return SupplierList[SupplierList.IndexOf(supplier)];
        }
    }// end supplierList Class

}//end nameSpace
