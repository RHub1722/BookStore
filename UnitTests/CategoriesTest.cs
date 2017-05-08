using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Shared.DTOS;

namespace UnitTests
{
    [TestClass]
    public class CategoriesTest : PrepairTests
    {
        
        [TestMethod]
        public void CreateCategory()
        {
            _categoriesService.CreateCategory(new CategoryDto(){Name = "TestCateg"});
            Assert.IsTrue(_categoriesService.GetAllCategories().Any(x =>x.Name == "TestCateg"));
        }

        [TestMethod]
        public void DeleteCategory()
        {
            _categoriesService.DeleteCategory(1);
            Assert.IsFalse(_categoriesService.GetAllCategories().Any(x =>x.Id == 1));
        }

        [TestMethod]
        public void GetCategory()
        {
           Assert.IsNotNull(_categoriesService.GetCategory(2));
        }

        [TestMethod]
        public void GetAllCategories()
        {
            Assert.IsTrue(_categoriesService.GetAllCategories().Any());
        }

        [TestMethod]
        public void UpdateCategory()
        {
            _categoriesService.UpdateCategory(2, new CategoryDto(){Name = "TestName"});
            Assert.IsTrue(_categoriesService.GetCategory(2).Name == "TestName");
        }
    }
}
