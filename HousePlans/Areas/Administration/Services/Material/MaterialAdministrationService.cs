namespace HousePlans.Areas.Administration.Services.Material
{
    using HousePlans.Data.Models;
    using HousePlans.Areas.Administration.Models.Material;
    using HousePlans.Data;

    public class MaterialAdministrationService : IMateialAdministrationService
    {
        private readonly ApplicationDbContext dbContext;

        public MaterialAdministrationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateMaterial(MaterialFormViewModel model)
        {
            var material = new Material
            {
                CreatedOn = DateTime.UtcNow,
                OverlappingTypes = model.OverlappingTypes,
                Technology = model.Technology,
                TypesOfRoof = model.TypesOfRoof,
                TypesOfWalls = model.TypesOfWalls,
            };


            await this.dbContext.AddAsync(material);
            await this.dbContext.SaveChangesAsync();

            return material.Id;
        }

        public async Task<MaterialDetailsViewModel> GetById(int materialId)
        {
            var material = this.dbContext.Materials
                .Where(x => !x.IsDeleted && x.Id == materialId)
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
