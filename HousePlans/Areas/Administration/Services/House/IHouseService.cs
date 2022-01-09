﻿namespace HousePlans.Areas.Administration.Services.House
{
    using HousePlans.Areas.Administration.Models.House;

    public interface IHouseService
    {
        Task<int> CreateHouse(HouseFormVIewModel model);

        Task<HouseDetailsViewModel> Details(int houseId);
    }
}
