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
       
        public string Upload (IFormFile arquivo, string savingFolder) {
               
            if(savingFolder == null) {
                savingFolder = Path.Combine ("imgUpdated");                
            }

            var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), savingFolder);

            if (arquivo.Length > 0) {
                var fileName = ContentDispositionHeaderValue.Parse (arquivo.ContentDisposition).FileName.Trim ('"');
                var fullPath = Path.Combine (pathToSave, fileName);

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