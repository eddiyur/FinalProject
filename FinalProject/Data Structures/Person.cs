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
        supplier
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





}//end nameSpace
