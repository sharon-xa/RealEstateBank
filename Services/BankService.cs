using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.Bank;
using RealEstateBank.Interface;

namespace RealEstateBank.Services;

public interface IBankService {
    Task<BankDto> UpdateBankAboutUs(string aboutUs);
    Task<BankDto> UpdateAboutRealEstateBank(RealEstateBankForm form);
    Task<BankDto> UpdateElectronicPaymentDep(ElectronicPaymentDepForm form);
    Task<BankDto> GetBank();
}

public class BankService : IBankService {

    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public BankService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        DataContext context
    ) {
        _context = context;
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task<BankDto> GetBank() {
        var bank = await _repositoryWrapper.Bank.GetBank();
        return _mapper.Map<BankDto>(bank);
    }

    public async Task<BankDto> UpdateAboutRealEstateBank(RealEstateBankForm form) {
        var bank = await _repositoryWrapper.Bank.GetBank();
        bank.BankEstablishment = form.BankEstablishment;
        bank.BankBusiness = form.BankBusiness;
        bank = await _repositoryWrapper.Bank.UpdateBank(bank);
        return _mapper.Map<BankDto>(bank);
    }

    public async Task<BankDto> UpdateBankAboutUs(string aboutUs) {
        var bank = await _repositoryWrapper.Bank.GetBank();
        bank.AboutUs = aboutUs;
        bank = await _repositoryWrapper.Bank.UpdateBank(bank);
        return _mapper.Map<BankDto>(bank);
    }

    public async Task<BankDto> UpdateElectronicPaymentDep(ElectronicPaymentDepForm form) {
        var bank = await _repositoryWrapper.Bank.GetBank();
        bank.BankServices = form.BankServices;
        bank.VideoTitle = form.VideoTitle;
        bank.VideoDescription = form.VideoDescription;
        bank = await _repositoryWrapper.Bank.UpdateBank(bank);
        return _mapper.Map<BankDto>(bank);
    }
}
