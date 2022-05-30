using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly LocalDbContext db;

        public ImageService(IWebHostEnvironment hostEnvironment, 
            LocalDbContext db)
        {
            this.hostEnvironment = hostEnvironment;
            this.db = db;
        }
        public async Task addImages(List<IFormFile> Images, int itemId)
        {
            foreach (var im in Images)
            {

                string dirPath = hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(im.FileName);
                string extension = Path.GetExtension(im.FileName);
                string ImageName = Path.Combine(fileName + DateTime.Now.ToString("yyyyymmssfff") + extension);
                string pathdb = Path.Combine("/ItemImage/", ImageName);
                string path = Path.Combine(dirPath+"/ItemImage/", ImageName);
                Image img = new Image()
                {
                    Path = pathdb,
                    ItemId = itemId
                };


                await db.Images.AddAsync(img);
                await db.SaveChangesAsync();
                
                using (var sr = new FileStream(path, FileMode.Create))
                {
                    await im.CopyToAsync(sr);
                }
            }
        }
    }
}
