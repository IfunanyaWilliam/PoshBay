﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PoshBay.Services
{
    public class ImageService : IImageService
    {
        private readonly CloudinaryConfig _config;

        //The IOptions is injected via the constructor to map the configuration to the ImageService
        public ImageService(IOptions<CloudinaryConfig> config)
        {
            _config = config.Value;
        }
        
        public async Task<string> UploadImage(string path)
        {

            var myAccount = new Account { ApiKey = _config.ApiKey, ApiSecret = _config.ApiSecret, Cloud = _config.Name };
            Cloudinary _cloudinary = new(myAccount);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.AbsoluteUri;

        }
    }
}
