using DataAccessLayer;
using ProductManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ProductService

    {
        public List<ProductModel> GetAllData()
        {
            return CRUD.GetAllData();
        }
    }
}
