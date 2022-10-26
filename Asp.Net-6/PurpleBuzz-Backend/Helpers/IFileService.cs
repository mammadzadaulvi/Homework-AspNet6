﻿namespace PurpleBuzz_Backend.Helpers
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string webRootPath);
        void Delete(string fileName, string webRootPath);
        bool IsImage(IFormFile formFile);
        bool checkSize(IFormFile formFile, int maxSize);
    }
}
