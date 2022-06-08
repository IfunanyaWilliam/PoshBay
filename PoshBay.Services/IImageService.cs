using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Services
{
    public interface IImageService
    {
        Task<string> UploadImage(string imagePath);
    }
}
