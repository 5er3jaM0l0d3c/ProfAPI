using AutoMapper;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly ILogger<PaymentTypeService> _logger;

        public PaymentTypeService(
            IPaymentTypeRepository paymentTypeRepository,
            ILogger<PaymentTypeService> logger)
        {
            _paymentTypeRepository = paymentTypeRepository;
            _logger = logger;
        }

        public async Task<PaymentType> CreatePaymentTypeAsync(CreatePaymentTypeRequest request)
        {
            try
            {
                PaymentType paymentType = new PaymentType
                {
                    Name = request.Name
                };

                var typeId = await _paymentTypeRepository.AddPaymentType(paymentType);

                return await GetPaymentTypeByIdAsync(typeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment type");
                throw;
            }
        }

        public async Task<bool> DeletePaymentTypeAsync(int id)
        {
            try
            {
                await GetPaymentTypeByIdAsync(id);

                return await _paymentTypeRepository.DeletePaymentType(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting payment type");
                throw;
            }
        }

        public async Task<PaymentType> GetPaymentTypeByIdAsync(int id)
        {
            try
            {
                var paymentType = await _paymentTypeRepository.GetPaymentTypeByIdAsync(id);
                if (paymentType == null)
                {
                    throw new NotFoundException(nameof(PaymentType), id);
                }

                return paymentType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment type");
                throw;
            }
        }

        public async Task<IEnumerable<PaymentType>> GetPaymentTypesAsync()
        {
            try
            {
                return await _paymentTypeRepository.GetPaymentTypes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment types");
                throw;
            }
        }

        public async Task<PaymentType> UpdatePaymentTypeAsync(UpdatePaymentTypeRequest request)
        {
            try
            {
                await GetPaymentTypeByIdAsync(request.Id);

                PaymentType paymentType = new PaymentType
                {
                    Id = request.Id,
                    Name = request.Name
                };

                if(!await _paymentTypeRepository.UpdatePaymentType(paymentType))
                    throw new ConflictException("Failed to update payment type");

                return await GetPaymentTypeByIdAsync(request.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment type");
                throw;
            }
        }
    }
}
