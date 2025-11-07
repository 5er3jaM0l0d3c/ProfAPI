using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

namespace ProfServer.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMachine_ProductService _machine_productService;
        private readonly IProductService _productService;
        private readonly IMachineService _machineService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly ILogger<SaleService> _logger;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository,
            IMachine_ProductService machine_productService,
            IProductService productService, 
            IMachineService machineService, 
            IPaymentTypeService paymentTypeService, 
            IMapper mapper, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _machine_productService = machine_productService;
            _productService = productService;
            _machineService = machineService;
            _paymentTypeService = paymentTypeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SaleDTO> CreateSaleAsync(CreateSaleRequest request)
        {
            try
            {
                Sale sale = _mapper.Map<Sale>(request);

                var saleId = await _saleRepository.CreateSaleAsync(sale);

                return await GetSaleByIdAsync(saleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale for product {ProductId} in machine {MachineId}", request.ProductId, request.MachineId);
                throw;
            }
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            try
            {
                await GetSaleByIdAsync(id); //проверка на существование

                return await _saleRepository.DeleteSaleAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sale {SaleId}", id);
                throw;
            }
        }

        public async Task<SaleDTO> GetSaleByIdAsync(int id)
        {
            try
            {
                var sale = await _saleRepository.GetSaleByIdAsync(id);
                if(sale == null)
                    throw new NotFoundException(nameof(Sale), id);


                sale.Product = await _productService.GetProductByIdAsync(sale.ProductId);
                sale.Machine = await _machineService.GetMachineByIdAsync(sale.MachineId);
                sale.PaymentType = await _paymentTypeService.GetPaymentTypeByIdAsync(sale.PaymentTypeId);

                return _mapper.Map<SaleDTO>(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sale {SaleId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<SaleDTO>> GetSalesFromMachineAsync(int machineId)
        {
            try
            {
                var sales = await _saleRepository.GetSalesFromMachineAsync(machineId);

                foreach (var sale in sales)
                {
                    sale.Product = await _productService.GetProductByIdAsync(sale.ProductId);
                    sale.Machine = await _machineService.GetMachineByIdAsync(sale.MachineId);
                    sale.PaymentType = await _paymentTypeService.GetPaymentTypeByIdAsync(sale.PaymentTypeId);
                }

                return _mapper.Map<IEnumerable<SaleDTO>>(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales from machine {MachineId}", machineId);
                throw;
            }
        }

        public async Task<IEnumerable<SaleDTO>> GetSalesWithProductAsync(int productId)
        {
            try
            {
                var sales = await _saleRepository.GetSalesFromProductAsync(productId);

                foreach(var sale in sales)
                {
                    sale.Product = await _productService.GetProductByIdAsync(sale.ProductId);
                    sale.Machine = await _machineService.GetMachineByIdAsync(sale.MachineId);
                    sale.PaymentType = await _paymentTypeService.GetPaymentTypeByIdAsync(sale.PaymentTypeId);
                }

                return _mapper.Map<IEnumerable<SaleDTO>>(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales from product {ProductId}", productId);
                throw;
            }
        }

        public async Task<SaleDTO> UpdateSaleAsync(UpdateSaleRequest request)
        {
            try
            {
                await GetSaleByIdAsync(request.Id);

                Sale sale = _mapper.Map<Sale>(request);

                if(!await _saleRepository.UpdateSaleAsync(sale))
                    throw new ConflictException("Failed to update sale.");

                return await GetSaleByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sale {SaleId}", request.Id);
                throw;
            }
        }
    }
}
