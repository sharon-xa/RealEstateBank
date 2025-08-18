namespace RealEstateBank.Interface;

public interface IRepositoryWrapper {
    IUserRepository Users { get; }
    IBankRepository Bank { get; }
    IAdvertisementRepository Advertisements { get; }
    IServiceRepository Services { get; }
}
