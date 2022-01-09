namespace HousePlans.Areas.Administration.Services.Floor
{
    using System.Collections.Generic;
    using HousePlans.Areas.Administration.Models.Floor;

    public interface IFloorService
    {
        Task CreateFloor(IEnumerable<FloorFormViewModel> model, int houseId);
    }
}
