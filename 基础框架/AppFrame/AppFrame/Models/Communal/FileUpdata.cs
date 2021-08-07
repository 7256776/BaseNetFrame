using Abp.Web.Models;
using AppFrame.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppFrame.Models.Communal
{
    public class FileUpdata 
    {
        
        public static AjaxResponse<AppFileInput> AppUpload(IFormFile formFile)
        {
            AppFileInput model = new AppFileInput();
            var ajaxResponse = new AjaxResponse<AppFileInput>(model)
            {
                Success = false
            };

            if (formFile == null)
            {
                ajaxResponse.Error = new ErrorInfo("上传失败", "未获取到上传文件");
                return ajaxResponse;
            }
            var now = DateTime.Now;
            //文件存储路径
            var filePath = string.Format(@"\Uploads\{0}\{1}\{2}\", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            //获取当前web目录 
            var webRootPath = Environment.CurrentDirectory + filePath;
            //创建目录
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);
            }
            try
            {
                #region  图片文件的条件判断
                //文件后缀
                var fileExtension = Path.GetExtension(formFile.FileName);
                //判断后缀是否是图片
                const string fileFilt = ".gif|.jpg|.jpeg|.png";
                if (fileExtension == null)
                {
                    ajaxResponse.Error = new ErrorInfo("上传失败", "上传文件仅限" + fileFilt);
                    return ajaxResponse;
                }
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                {
                    ajaxResponse.Error = new ErrorInfo("上传失败", "上传文件仅限" + fileFilt);
                    return ajaxResponse;
                }
                //获取配置信息中文件上传大小
                IConfigurationRoot _appConfiguration = AppConfigurations.GetConfigurationCache();
                var multipartBodyLengthLimit = _appConfiguration["File:MultipartBodyLengthLimit"];
                long limit = 0;
                long.TryParse(multipartBodyLengthLimit, out limit);
                limit = limit * 1024 * 1024;
                //判断文件大小    
                long length = formFile.Length;
                if (length > limit) 
                {
                    ajaxResponse.Error = new ErrorInfo("上传失败", "上传的文件大小超出限制");
                    return ajaxResponse;
                }
                #endregion
                var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                var saveName = strDateTime + strRan + fileExtension;

                webRootPath = Path.Combine(webRootPath, saveName);
                //插入图片数据                 
                using (FileStream fs = System.IO.File.Create(webRootPath))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }

                string filePathPreview = "", filePathThumbnail = "";
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) >= 0)
                {
                    //生成预览图
                    filePathPreview = FileUpdata.ThumbImg(filePath + saveName, "_PreviewImg", 1080, 720);
                    //生成缩放图
                    filePathThumbnail = FileUpdata.ThumbImg(filePath + saveName, "_ThumbImg");
                }
                //
                model.FileAlias = saveName;
                model.FileName = formFile.FileName;
                model.FilePathOriginal = filePath + saveName;
                model.FilePathPreview= filePathPreview;
                model.FilePathThumbnail = filePathThumbnail;
                model.FileSuffix = fileExtension;
                model.FileSize = formFile.Length;
                model.IsActive = true;
                ajaxResponse.Success = true;
               
                return ajaxResponse;
            }
            catch (Exception ex)
            {
                //这边增加日志，记录错误的原因
                ajaxResponse.Error = new ErrorInfo("上传失败", ex.Message);
                return ajaxResponse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件绝对路径地址</param>
        /// <param name="maxWidth">宽度</param>
        /// <param name="maxHeight">高度</param>
        public static string ThumbImg(string fileName, string fileNameExtension, int maxWidth = 150, int maxHeight = 150)
        {
            //获取当前web目录 
            string relativeFileName =
                Path.GetDirectoryName(fileName) + "\\" +
                Path.GetFileNameWithoutExtension(fileName)  +
                fileNameExtension +
                Path.GetExtension(fileName);

            string newFileName = Environment.CurrentDirectory + relativeFileName;
            //
            fileName = Environment.CurrentDirectory + fileName;

            //获取图片
            byte[] imageBytes = File.ReadAllBytes(fileName);
            Image original = Image.FromStream(new MemoryStream(imageBytes));
            //获取缩放后尺寸
            Size newSize = ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
            using (Image displayImage = new Bitmap(original, newSize))
            {
                try
                {
                    displayImage.Save(newFileName, original.RawFormat);
                }
                finally
                {
                    original.Dispose();
                }
            }
            return relativeFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private static Size ResizeImage(int w, int h, int maxW, int maxH)
        {
            decimal width = Convert.ToDecimal(w);
            decimal height = Convert.ToDecimal(h);
            decimal maxWidth = Convert.ToDecimal(maxW);
            decimal maxHeight = Convert.ToDecimal(maxH);

            //排除0的长度单位
            maxWidth = maxWidth <= 0 ? width : maxWidth;
            maxHeight = maxHeight <= 0 ? height : maxHeight;
            //横向缩放比例
            decimal aspectRatio = maxWidth / maxHeight;
            //
            int newWidth, newHeight;
            //
            if (width > maxWidth || height > maxHeight)
            {
                decimal factor;
                //现有长宽比例如果与缩放长宽比不一致采用宽度比例进行缩放
                if (width / height > aspectRatio)
                {
                    factor = width / maxWidth;
                    newWidth = Convert.ToInt32(width / factor);
                    newHeight = Convert.ToInt32(height / factor);
                }
                else
                {
                    factor = height / maxHeight;
                    newWidth = Convert.ToInt32(width / factor);
                    newHeight = Convert.ToInt32(height / factor);
                }
            }
            else
            {
                newWidth = Convert.ToInt32(width);
                newHeight = Convert.ToInt32(height);
            }
            return new Size(newWidth, newHeight);
        }



    }
}
