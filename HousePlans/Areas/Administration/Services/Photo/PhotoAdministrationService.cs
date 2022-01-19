namespace HousePlans.Areas.Administration.Services.Photo
{
    using HousePlans.Data;
    using HousePlans.Data.Models;
    public class PhotoAdministrationService : IPhotoAdministrationService
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext dbContext;

        public PhotoAdministrationService(IWebHostEnvironment environment, ApplicationDbContext dbContext)
        {
            this.environment = environment;
            this.dbContext = dbContext;
        }

        public async Task UploadHousePhotos(IEnumerable<IFormFile> formFiles, int houseId)
        {
            string path = Path.Combine(this.environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new();
            foreach (IFormFile postedFile in formFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using FileStream stream = new(Path.Combine(path, fileName), FileMode.Create);
                postedFile.CopyTo(stream);
                uploadedFiles.Add(fileName);

                string url = path + $"\\{fileName}";
                var photo = new Photo
                {
                    Url = url.Substring(57),
                    CreatedOn = DateTime.UtcNow,
                    BuildingId = houseId,
                };

                this.dbContext.Photos.Add(photo);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task UploadPlanPhotos(IEnumerable<IFormFile> formFiles, int planId)
        {
            string path = Path.Combine(this.environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new();
            foreach (IFormFile postedFile in formFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using FileStream stream = new(Path.Combine(path, fileName), FileMode.Create);
                postedFile.CopyTo(stream);
                uploadedFiles.Add(fileName);

                string url = path + $"\\{fileName}";
                var photo = new Photo
                {
                    Url = url.Substring(57),
                    CreatedOn = DateTime.UtcNow,
                    PlanId = planId,
                };

                this.dbContext.Photos.Add(photo);
                await this.dbContext.SaveChangesAsync();
            }
        }

    }
}
