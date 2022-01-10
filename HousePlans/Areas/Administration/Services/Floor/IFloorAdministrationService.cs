namespace HousePlans.Areas.Administration.Services.Floor
{
    using System.Collections.Generic;
    using HousePlans.Areas.Administration.Models.Floor;

    public interface IFloorAdministrationService
    {
        Task CreateFloor(IEnumerable<FloorFormViewModel> model, int houseId);

        Task<IEnumerable<FloorFormViewModel>> GetByPlanId(int planId);
    }
}
