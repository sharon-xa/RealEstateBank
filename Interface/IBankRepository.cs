using RealEstateBank.Entities;

namespace RealEstateBank.Interface;

public interface IBankRepository {
    Task<Bank> UpdateBank(Bank bank);
    Task<Bank> GetBank();
}
