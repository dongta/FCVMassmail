using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var request = HttpContext.Current.Request.Url.Authority;
           HttpPostedFile uploads = context.Request.Files["upload"];
           string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
           string file = System.IO.Path.GetFileName(uploads.FileName);
           uploads.SaveAs(context.Server.MapPath(".") + "\\Files\\UploadImage\\" + file);
           //provide direct URL here
            string path= context.Server.MapPath(".") + "\\Files\\UploadImage\\" + file;
           string url1 ="http:\\\\" +request + "\\Files\\UploadImage\\" + file;
           string url = url1.Replace(@"\", @"\\");



           //string path = url1;
           byte[] imageByteData = System.IO.File.ReadAllBytes(path);
           string imageBase64Data = Convert.ToBase64String(imageByteData);
           string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);


           context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + 1 +
             ", \"" + url + "\");</script>");
           context.Response.End();   
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


         private static string GetFileContentType(string path)
    {
        if (path.EndsWith(".JPG", StringComparison.OrdinalIgnoreCase) == true)
        {
            return "image/jpeg";
        }
        else if (path.EndsWith(".GIF", StringComparison.OrdinalIgnoreCase) == true)
        {
            return "image/gif";
        }
        else if (path.EndsWith(".PNG", StringComparison.OrdinalIgnoreCase) == true)
        {
            return "image/png";
        }
 
        throw new ArgumentException("Unknown file type");
    }
 
    //public static HtmlString InlineImage(this IHtmlHelper html, string path, object attributes = null)
    //{
    //    var contentType = GetFileContentType(path);
    //    var env = html.ViewContext.HttpContext.ApplicationServices.GetService(typeof (IHostingEnvironment)) as IHostingEnvironment;
 
    //    using (var stream = env.WebRootFileProvider.GetFileInfo(path).CreateReadStream())
    //    {
    //        var array = new byte[stream.Length];
    //        stream.Read(array, 0, array.Length);
 
    //        var base64 = Convert.ToBase64String(array);
 
    //        var props = (attributes != null) ? attributes.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(attributes)) : null;
 
    //        var attrs = (props == null)
    //            ? string.Empty
    //            : string.Join(" ", props.Select(x => string.Format("{0}=\"{1}\"", x.Key, x.Value)));
 
    //        var img = $"<img src=\"data:{contentType};base64,{base64}\" {attrs}/>";
 
    //        return new HtmlString(img);
    //    }
    //}
    }
}