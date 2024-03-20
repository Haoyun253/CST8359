using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab5.Data;
using Lab5.Models;
using Azure.Storage.Blobs;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Pages.Predictions
{
    public class CreateModel : PageModel
    {

        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";

        private readonly Lab5.Data.PredictionDataContext _context;

        public CreateModel(Lab5.Data.PredictionDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Prediction Prediction { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {


            BlobContainerClient containerClient;
            string containerName;

            if (Prediction.Question == Question.Earth)
            {
                containerName = earthContainerName;
            }
            else
            {
                containerName = computerContainerName;
            }

            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }


            if (ImageFile != null) { 
                var blobClient = containerClient.GetBlobClient(ImageFile.FileName);

                if (await blobClient.ExistsAsync())
                {
                    await blobClient.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);

                    memoryStream.Position = 0;

                    await blobClient.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                Prediction.Url = blobClient.Uri.AbsoluteUri;
                Prediction.FileName = ImageFile.FileName;

            }

            _context.Predictions.Add(Prediction);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}