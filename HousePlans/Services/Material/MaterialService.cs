namespace HousePlans.Services.Material
{
    using HousePlans.Data;
    using HousePlans.Models.Material;

    public class MaterialService : IMaterialService
    {
        private readonly ApplicationDbContext dbContext;

        public MaterialService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MaterialDetailsViewModel> GetByHouseId(int houseId)
        {
            var material = this.dbContext.Materials
                 .Where(x => !x.IsDeleted && x.Id == houseId)
                 .Select(x => new MaterialDetailsViewModel
                 {
                     OverlappingTypes = x.OverlappingTypes,
                     Technology = x.Technology,
                     TypesOfRoof = x.TypesOfRoof,
                     TypesOfWalls = x.TypesOfWalls,
                 })
                 .FirstOrDefault();

            return material;
        }
    }
}
