namespace HousePlans.Services.House
{
    using HousePlans.Models.House;

    public interface IHouseService
    {
        Task<HouseDetailsViewModel> GetById(int houseId);
    }
}
