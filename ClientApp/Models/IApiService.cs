using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.Net.Http;

namespace ClientApp.Models
{
    public interface IApiService
    {
        HttpResponseMessage UploadImage(Photo photo);
        Photo GetImage(Photo photo);
    }
}
