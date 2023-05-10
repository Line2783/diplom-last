using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class FileUploadsController : Controller
    {
        // /// <summary>
        // /// Добавление изображения для продукта. Role - Manager
        // /// </summary>
        // /// <param name="idProduct">id продукта</param>
        // /// <param name="uploadedFile">фото продукта</param>
        // /// <returns></returns>
        // [Authorize(Roles = "Customer")]
        // [HttpPost("{idProduct}")]
        // public async Task<IActionResult> AddPhoto(Guid idProduct, IFormFile uploadedFile)
        // {
        //     var productPhoto = new ProductPhoto
        //     {
        //         Id = Guid.NewGuid(),
        //         ProductId = idProduct,
        //         Name = uploadedFile.FileName
        //     };
        //     using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
        //     {
        //         productPhoto.Photo = binaryReader.ReadBytes((int)uploadedFile.Length);
        //     }
        //
        //     await _productPhotoService.AddPhoto(productPhoto);
        //     return await GetPhoto(productPhoto.Id);
        // }
        //
        // /// <summary>
        // /// Получение изображения продукта
        // /// </summary>
        // /// <param name="imageId">id фото</param>
        // /// <returns></returns>
        // [HttpGet("/photo/{imageId}", Name = "GetPhoto")]
        // public async Task<IActionResult> GetPhoto(Guid imageId)
        // {
        //     var file = await _productPhotoService.GetFileAsync(imageId);
        //     var stream = new MemoryStream(file.Photo);
        //     return File(stream, "application/octet-stream", $"{file.Name}");
        // }
        //
    }
}