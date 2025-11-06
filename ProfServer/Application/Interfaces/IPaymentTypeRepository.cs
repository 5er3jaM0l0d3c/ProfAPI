using ProfServer.Models;

namespace ProfServer.Application.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentType>> GetPaymentTypes();
        Task<PaymentType?> GetPaymentTypeByIdAsync(int id);
        Task<int> AddPaymentType(PaymentType paymentType);
        Task<bool> UpdatePaymentType(PaymentType paymentType);
        Task<bool> DeletePaymentType(int id);
    }
}
