using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class Machine_ProductService : IMachine_ProductService
    {
        private readonly IMachine_ProductRepository _machine_ProductRepository;
        private readonly IMachineService _machineService;
        private readonly IProductService _productService;
        private readonly ILogger<Machine_ProductService> _logger;
        private readonly IMapper _mapper;

        public Machine_ProductService(IMachine_ProductRepository machine_ProductRepository, IMachineService machineService, IProductService productService, IMapper mapper, ILogger<Machine_ProductService> logger)
        {
            _machine_ProductRepository = machine_ProductRepository;
            _machineService = machineService;
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Machine_ProductDTO> CreateMachine_ProductAsync(CreateMachine_ProductRequest request)
        {
            try
            {
                var machine_product = _mapper.Map<Machine_Product>(request);

                var machine_ProductId = await _machine_ProductRepository.CreateMachine_ProductAsync(machine_product);

                return await GetMachine_ProductByIdAsync(machine_ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Machine_Product for product {ProductId} in machine {MachineId}", request.ProductId, request.MachineId);
                throw;
            }
        }

        public async Task<bool> DeleteMachine_ProductAsync(int id)
        {
            try
            {
                var mach_prod = await GetMachine_ProductByIdAsync(id);
                if(mach_prod.Quantity > 0)
                    throw new ConflictException("Cannot delete Machine_Product with non-zero product quantity.");
                return await _machine_ProductRepository.DeleteMachine_ProductAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Machine_Product {Machine_ProductId}", id);
                throw;
            }
        }
        public async Task<Machine_ProductDTO> GetMachine_ProductByIdAsync(int id)
        {
            try
            {
                var machine_product = await _machine_ProductRepository.GetMachine_ProductByIdAsync(id);
                if (machine_product == null)
                    throw new NotFoundException(nameof(Machine_Product), id);

                return _mapper.Map<Machine_ProductDTO>(machine_product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Machine_Product {Machine_ProductId}", id);
                throw;
            }
        }

        public async Task<Machine_ProductDTO> GetMachine_ProductByMachineAndProductAsync(int machineId, int productId)
        {
            try
            {
                var machine_product = await _machine_ProductRepository.GetMachine_ProductByMachineAndProductId(machineId, productId);
                if (machine_product == null) throw new NotFoundException(nameof(Machine_Product), new {machineId, productId});

                return _mapper.Map<Machine_ProductDTO>(machine_product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Machine_Product for machine {MachineId} and product {ProductId}", machineId, productId);
                throw;
            }
        }

        public async Task<Machine_ProductDTO> UpdateMachine_ProductAsync(UpdateMachine_ProductRequest request)
        {
            try
            {
                await _machine_ProductRepository.GetMachine_ProductByIdAsync(request.Id);

                var machine_product = _mapper.Map<Machine_Product>(request);

                if(!await _machine_ProductRepository.UpdateMachine_ProductAsync(machine_product))
                    throw new ConflictException("Failed to update Machine_Product.");

                return await GetMachine_ProductByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Machine_Product {Machine_ProductId}", request.Id);
                throw;
            }
        }
    }
}
