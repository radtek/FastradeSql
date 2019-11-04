 using System.Collections.Generic;
 using System.IO;
 using System.Linq;
 using System.Net.Http.Headers;
 using System.Threading.Tasks;
 using System;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc;

 namespace UploadControllers {
     public class UploadController : ControllerBase {
         public string Upload (IFormFile arquivo, string savingFolder ) {

             var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), savingFolder);

             if (arquivo.Length > 0) {

                 var folderName = Path.Combine ("Resources", "Images");
                 var fileName = ContentDispositionHeaderValue.Parse (arquivo.ContentDisposition).FileName.Trim ('"');
                 var fullPath = Path.Combine (pathToSave, fileName);
                 var Dbpath = Path.Combine (folderName, fileName);


                 using (var stream = new FileStream (fullPath, FileMode.Create)) {
                     arquivo.CopyTo (stream);
                 }

                 return fullPath;
             } else {
                 return null;
             }
         }
     }
 }