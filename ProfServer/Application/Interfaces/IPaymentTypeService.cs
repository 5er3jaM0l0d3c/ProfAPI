using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<IEnumerable<PaymentType>> GetPaymentTypesAsync();
        Task<PaymentType> GetPaymentTypeByIdAsync(int id);
        Task<PaymentType> CreatePaymentTypeAsync(CreatePaymentTypeRequest request);
        Task<PaymentType> UpdatePaymentTypeAsync(UpdatePaymentTypeRequest request);
        Task<bool> DeletePaymentTypeAsync(int id);
    }
}
