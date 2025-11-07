using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.Repositories;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMachine_ProductRepository _machine_ProductRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IMachine_ProductRepository machine_ProductRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _machine_ProductRepository = machine_ProductRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Product> CreateProductAsync(CreateProductRequest request)
        {
            try
            {
                Product product = _mapper.Map<Product>(request);

                var productId = await _productRepository.CreateProductAsync(product);

                return await GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product with name {name}", request.Name);
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                await GetProductByIdAsync(id);

                return await _productRepository.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with id {id}", id);
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if(product == null)
                    throw new NotFoundException(nameof(Product), id);

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product with id {id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                return await _productRepository.GetProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsInMachineAsync(int machineId)
        {
            try
            {
                var productsIds = await _machine_ProductRepository.GetProductsInMachineAsync(machineId);

                var products = await _productRepository.GetProductsByIdsAsync(productsIds);

                return _mapper.Map<IEnumerable<Product>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products in machine {MachineId}", machineId);
                throw;
            }
        }

        public async Task<Product> UpdateProductAsync(UpdateProductRequest request)
        {
            try
            {
                await GetProductByIdAsync(request.Id);

                if(!await _productRepository.UpdateProductAsync(_mapper.Map<Product>(request)))
                    throw new ConflictException("Failed to update product");

                return await GetProductByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with id {id}", request.Id);
                throw;
            }
        }
    }
}
