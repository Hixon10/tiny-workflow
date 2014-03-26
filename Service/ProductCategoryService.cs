using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Infrastructure.Contracts;

namespace Service
{
    public class ProductCategoryService : BaseService, IProductCategoryService
    {
        public ProductCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<ProductCategory> GetAllCategories()
        {
            return unitOfWork.ProductCategoryRepository.GetAll().ToList();
        }

        public ProductCategory GetCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new NullReferenceException("name");
            }

            return unitOfWork.ProductCategoryRepository.Get(p => p.Name == name).FirstOrDefault();
        }
    }
}
