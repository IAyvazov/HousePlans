namespace HousePlans.Areas.Administration.Services.Photo
{
    public interface IPhotoAdministrationService
    {
        Task UploadHousePhotos(IEnumerable<IFormFile> formFiles, int houseId);

        Task UploadPlanPhotos(IEnumerable<IFormFile> formFiles, int planId);
    }
}
