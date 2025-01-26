
using Microsoft.EntityFrameworkCore;
using NetCore_OnionArchitecture.Application.DTOs.Product;
using NetCore_OnionArchitecture.Application.Exceptions;
using NetCore_OnionArchitecture.Domain.Common.DI;
using NetCore_OnionArchitecture.Domain.Common.Repositories;
using NetCore_OnionArchitecture.Domain.Common.UnitOfWork;
using NetCore_OnionArchitecture.Domain.Entities;

namespace NetCore_OnionArchitecture.Application.Features.Products
{
    public class ProductManager : ITransientDependency
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Product, int> _productRepository;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.GetRepository<Product, int>();
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            return await _productRepository.GetAll()

                .Select(x => new ProductDto
                {
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price,
                    Stock = x.Stock,
                }).ToListAsync();
        }

        //Ekleme-güncelleme ve silme.
        public async Task<int> AddOrEditProduct(AddOrEditProductDto input)
        {
            if (input.ProductId == null)
                return await createProduct(input);
            else
                return await editProduct(input);

        }
        private async Task<int> createProduct(AddOrEditProductDto input)
        {
            var data = new Product
            {
                Name = input.Name,
                CategoryId = input.CategoryId,
                Description = input.Description,
                Price = input.Price,
                Stock = input.Stock
            };

            await _productRepository.InsertAsync(data);
            await _unitOfWork.CompleteAsync();

            return data.Id;
        }
        private async Task<int> editProduct(AddOrEditProductDto input)
        {
            var product = await _productRepository.GetAll()
               .FirstOrDefaultAsync(x => x.Id == input.ProductId);
            if (product == null)
                throw new ApiException("Müşteri bulunamadı");

            product.Name = input.Name;
            product.CategoryId = input.CategoryId;
            product.Description = input.Description;
            product.Price = input.Price;
            product.Stock = input.Stock;


            await _unitOfWork.CompleteAsync();
            return product.Id;
        }

        public async Task DeleteProduct(int customerId)
        {
            var product = await _productRepository.GetAll() 
                .FirstOrDefaultAsync(x => x.Id == customerId);
            if (product == null)
                throw new ApiException("Ürün bulunamadı");

            await _productRepository.DeleteAsync(product); 
            await _unitOfWork.CompleteAsync();
        }
    }
}





