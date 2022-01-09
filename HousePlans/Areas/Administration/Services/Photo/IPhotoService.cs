namespace HousePlans.Areas.Administration.Services.Photo
{
    public interface IPhotoService
    {
        Task Upload(IEnumerable<IFormFile> formFiles, int houseId);
    }
}
