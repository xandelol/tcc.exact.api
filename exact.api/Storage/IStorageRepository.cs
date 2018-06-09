using System.Threading.Tasks;

namespace exact.api.Storage
{
    public interface IStorageRepository
    {
        Task<string> UploadImage(string image);

        Task<string> UploadPdf(string pdf);

        Task<string> UploadPdfByBytes(byte[] pdfBytes);
    }
}