namespace HousePlans.Areas.Administration.Services.Photo
{
    public interface IPhotoAdministrationService
    {
        Task Upload(IEnumerable<IFormFile> formFiles, int houseId);
    }
}
