using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
//using System.Web.Mvc;

namespace TestApiWithImages.Controllers
{
    public class UploadCustomerImageModel
    {
        public string Description { get; set; }
        public string ImageData { get; set; }
    }
    public class FileUploadingController : ApiController
    {
        [HttpPost]
        [Route("api/FileUploading/UploadFile")]
        public async Task<string> UploadFile()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data");
            var provider =
                new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content
                    .ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers
                        .ContentDisposition
                        .FileName;

                    // remove double quotes from string.
                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    File.Move(localFileName, filePath);
                }
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }

            return "File uploaded!";
        }
        [HttpPost]
        [Route("api/FileUploading/UploadFileNew")]
        public async Task<JObject> ExecutePostAsync(Stream myStreem, string url, string token, string parameter1, string parameter2, string parameter3)
        {
            try
            {
                using (var content = new MultipartFormDataContent("----MyBoundary"))
                {
                    using (var memoryStream = myStreem)
                    {
                        using (var stream = new StreamContent(memoryStream))
                        {
                            content.Add(stream, "file", Guid.NewGuid().ToString() + ".jpg");
                            content.Add(new StringContent(parameter1), "parameter1");
                            content.Add(new StringContent(parameter3), "parameter2");
                            content.Add(new StringContent(parameter3), "parameter3");

                            using (HttpClient client = new HttpClient())
                            {
                                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                                var responce = await client.PostAsync(url, content);
                                string contents = await responce.Content.ReadAsStringAsync();
                                return (JObject.Parse(contents));
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<IHttpActionResult> UploadFileNew()
        //{

        //    string parameter1 = HttpContext.Current.Request.Form["parameter1"];
        //    string parameter2 = HttpContext.Current.Request.Form["parameter2"];
        //    string parameter3 = HttpContext.Current.Request.Form["parameter3"];

        //}
        [System.Web.Http.HttpPost]
        [Route("{customerId}/images")]
        public System.Web.Mvc.FileContentResult UploadCustomerImage(int customerId, [FromBody] UploadCustomerImageModel model)
        {
            //Depending on if you want the byte array or a memory stream, you can use the below. 
            var imageDataByteArray = Convert.FromBase64String(model.ImageData);

            //When creating a stream, you need to reset the position, without it you will see that you always write files with a 0 byte length. 
            var imageDataStream = new MemoryStream(imageDataByteArray);
            imageDataStream.Position = 0;

            //Go and do something with the actual data.
            //_customerImageService.Upload([...])

            //For the purpose of the demo, we return a file so we can ensure it was uploaded correctly. 
            //But otherwise you can just return a 204 etc. 
            return null;// File(imageDataByteArray, "image/png");
        }
        [HttpGet]
        public HttpResponseMessage Test()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/brande"); ;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentLength = stream.Length;
            return result;
        }
    }
}
