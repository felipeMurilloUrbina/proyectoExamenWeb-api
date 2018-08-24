using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Providers
{
    public class ImageProvider: IDisposable
    {
        string path = "~/App_Data/avatars";
        public ImageProvider(HttpContext context)
        {
            this.path = context.Server.MapPath(this.path);
            if (Directory.Exists(this.path))
                Directory.CreateDirectory(this.path);
        }

        public void Dispose()
        {
        }

        public void Save(string image64, string userId)
        {
            var name = Path.Combine(this.path, userId, ".png");
            if (File.Exists(name))
                File.Delete(name);
            var bytes = Convert.FromBase64String(image64);
            using (var imageFile = new FileStream(name, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
        }
    }
}