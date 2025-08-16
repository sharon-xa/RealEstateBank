
using AutoMapper;

using Microsoft.EntityFrameworkCore;

using RealEstateBank.Data;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils.Exceptions;

namespace RealEstateBank.Repository;

public class BankRepository : IBankRepository {
    protected readonly DataContext _ctx;
    protected readonly IMapper _mapper;

    public BankRepository(DataContext context, IMapper mapper) {
        _ctx = context;
        _mapper = mapper;
    }

    public async Task<Bank> GetBank() {
        Bank? bank = await _ctx.Bank.FirstOrDefaultAsync();
        // The bank should exists, we populated the table with migrations
        if (bank == null)
            throw new DatabaseException("no bank Found", nameof(BankRepository), nameof(GetBank));

        return bank;
    }

    public async Task<Bank> UpdateBank(Bank bank) {
        _ctx.Bank.Update(bank);
        await _ctx.SaveChangesAsync();
        return bank;
    }
}
