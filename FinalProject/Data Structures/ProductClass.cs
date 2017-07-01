﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.Data_Structures
{
    public class ProductClass
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductCapacity { get; set; }
        public Dictionary<string, int> ProductTree { get; set; }
        public List<Tool> ProductionToolList { get; set; }
        public Dictionary<Tool, ProductionStatusEnum> ProductionProgress { get; set; }

        public ProductClass(string productID)
        {
            ProductID = productID;
        }

        public ProductClass()
        { }

        public ProductClass(string productID, string productName, double productCapacity)
        {
            ProductID = productID;
            ProductName = productName;
            ProductCapacity = productCapacity;
            ProductTree = new Dictionary<string, int>();
            ProductionToolList = new List<Tool>();
            setInitProductionProgress();
            //ProductionProgress = new Dictionary<Tool, ProductionStatusEnum>();
        }
        public enum ProductionStatusEnum
        {
            NotStarted,
            InProgress,
            Ready
        }



        private void setInitProductionProgress()
        {
            ProductionProgress = new Dictionary<Tool, ProductionStatusEnum>();
            foreach (Tool tool in ProductionToolList)
                ProductionProgress.Add(tool, ProductionStatusEnum.NotStarted);
        }

        public override bool Equals(object obj)
        {
            ProductClass product = (ProductClass)obj;
            return ProductID.Equals(product.ProductID);
        }


        public override int GetHashCode()
        {
            return ProductID.GetHashCode();
        }

    }//end Product class

    public class ProductClassList
    {
        public List<ProductClass> ProductList { get; set; }
        public ProductClassList()
        {
            ProductList = new List<ProductClass>();
        }


        public void AddProduct(ProductClass product)
        {
            ProductList.Add(product);
        }

        public ProductClass GetProduct(ProductClass product)
        {
            return ProductList[ProductList.IndexOf(product)];
        }

        public ProductClass GetProduct(string productID)
        {
            try
            {
                ProductClass product = new ProductClass(productID);
                return ProductList[ProductList.IndexOf(product)];
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Product ID " + productID + " no Found in Product List");
                return null;
            }
        }

    }//end ProductClassList

}//end namespace
