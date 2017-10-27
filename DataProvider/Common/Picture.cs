using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataProvider.Data
{
    public class Picture
    {

         

        public string FolderBase = "\\Images\\pic\\"; //照片基路径
        public string Category = "Student";//照片分类
        public string ThumbnailImagePath = "\\Thumbnail_Image\\";//照片分类

        public Dictionary<string, string> DPUpLoadPhoto(JObject json)
        {
            var fileTemp = json["fileTemp"].ToString();
            var fileExt = json["fileExt"].ToString();
            //var offset = filetemp.IndexOf(";base64,") + 8;
            var offset = 0;
            var result = this.SaveImgFile(null, fileExt, Category, fileTemp.Substring(offset));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(result))
            {
                var filename = this.FolderBase + Category + "\\" + Path.GetFileName(result);
                var thumbnailImage = this.FolderBase + Category + ThumbnailImagePath + Path.GetFileName(result);//缩略图
                d.Add("result", "上传成功");
                d.Add("filename", filename);
                d.Add("thumbnailImage","../" + thumbnailImage);//目录跳转到上一级

            }
            else
            {
                d.Add("result", "上传失败");
                d.Add("filename", "");
            }
            return d;
        }






        public string FolderBase_Attendanc = "\\Upload\\"; //照片基路径
        public string Category_Attendance = "AttendanceList";//照片分类 

        public Dictionary<string, string> DPUpLoadFile(JObject json)
        {
            var fileTemp = json["fileTemp"].ToString();
            var fileExt = json["fileExt"].ToString(); 
            //var offset = filetemp.IndexOf(";base64,") + 8;
            var offset = 0;
            var result = this.SaveImgFile_Attendanc(null, fileExt, Category_Attendance, fileTemp.Substring(offset));

            Dictionary<string, string> d = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(result))
            {
               // var filename = this.FolderBase_Attendanc + Category_Attendance + "\\" + Path.GetFileName(result);
              //  var thumbnailImage = this.FolderBase_Attendanc + Category_Attendance + ThumbnailImagePath + Path.GetFileName(result);//缩略图
                var filename =Path.GetFileName(result);
                var ContentType = Path.GetExtension(result);
                
                d.Add("result", "上传成功");
                d.Add("filename", filename);
                d.Add("ContentType", ContentType);
                //d.Add("thumbnailImage", thumbnailImage);

            }
            else
            {
                d.Add("result", "上传失败");
                d.Add("filename", "");
            }
            return d;
        }

        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string SaveImgFile_Attendanc(string fileName, string fileExt, string category, string fileContent)
        {
            string path = string.Empty;
            try
            {
                string strDomainAppPath = HttpRuntime.AppDomainAppPath;
                var subpath = strDomainAppPath.Substring(0, strDomainAppPath.LastIndexOf("\\")) + FolderBase_Attendanc + category + "\\";
                if (!Directory.Exists(subpath))//如果子目录不存在，则创建。
                {
                    Directory.CreateDirectory(subpath);
                }
                //path = strDomainAppPath.Substring(0, strDomainAppPath.LastIndexOf("\\")) + "\\Images\\pic\\" + category + "\\" + fileName;
                path = strDomainAppPath.Substring(0, strDomainAppPath.LastIndexOf("\\")) + FolderBase_Attendanc + category + "\\";
                path += String.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + "." + fileExt;

                if (File.Exists(path))//文件重名的处理
                {
                    string dictName = Path.GetDirectoryName(path);
                    string pureName = Path.GetFileNameWithoutExtension(path);
                    string extName = Path.GetExtension(path);
                    File.Move(path, dictName + "\\" + String.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + "0" + extName);
                }

                using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                {
                    Byte[] bData = Convert.FromBase64String(fileContent);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(bData);
                    bw.Flush();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(path));
                //ErrorMessage.ErrorMess(ex.Message);
            }
          
            return path;
        }





        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string SaveImgFile(string fileName, string fileExt, string category, string fileContent)
        {
            string path = string.Empty;
            try
            {
                string strDomainAppPath = HttpRuntime.AppDomainAppPath;
                var subpath = strDomainAppPath.Substring(0, strDomainAppPath.LastIndexOf("\\")) + FolderBase + category + "\\";
                if (!Directory.Exists(subpath))//如果子目录不存在，则创建。
                {
                    Directory.CreateDirectory(subpath);
                }
                //path = strDomainAppPath.Substring(0, strDomainAppPath.LastIndexOf("\\")) + "\\Images\\pic\\" + category + "\\" + fileName;
                path = strDomainAppPath.Substring(0, strDomainAppPath.LastIndexOf("\\")) + FolderBase + category + "\\";
                path += String.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + "." + fileExt;

                if (File.Exists(path))//文件重名的处理
                {
                    string dictName = Path.GetDirectoryName(path);
                    string pureName = Path.GetFileNameWithoutExtension(path);
                    string extName = Path.GetExtension(path);
                    File.Move(path, dictName + "\\" + String.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + "0" + extName);
                }

                using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                {
                    Byte[] bData = Convert.FromBase64String(fileContent);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(bData);
                    bw.Flush();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(path));
                //ErrorMessage.ErrorMess(ex.Message);
            }
            GetThumbnailImage(path);
            return path;
        }

        /// <summary>
        /// 按比例生成缩略图
        /// </summary>
        /// <param name="fileName"></param>
        private void GetThumbnailImage(string fileName)
        {
            try
            {
                Image img = Image.FromFile(fileName);
                string basePath = HttpRuntime.AppDomainAppPath + FolderBase + Category + ThumbnailImagePath;
                CreateThumbDirectory(basePath);
                string ThumbnailImageName = basePath + System.IO.Path.GetFileName(fileName);
                var gene = (float)img.Width / (float)img.Height; //宽高比
                var fh = 180; //缩略图高
                var fw = Convert.ToInt32(gene * fh); //缩略图宽
                Image bb = CreateThumbnailImage(img, fw, fh);
                bb.Save(ThumbnailImageName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //创建缩略图
        private Image CreateThumbnailImage(Image img, int imgWidth, int imgHeight)
        {
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            Image myThumbnail = img.GetThumbnailImage(imgWidth, imgHeight, myCallback, IntPtr.Zero);

            return myThumbnail;
        }
        //回调方法
        private bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// 创建缩略图目录
        /// </summary>
        /// <param name="path"></param>
        private static void CreateThumbDirectory(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }







        
        /// <summary>
        /// 保存文件，获取路径
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string DPSaveOrderFile(string fileName, string category, string fileContent)
        {
            string path = string.Empty;
            try
            {
                string strDomainAppPath = HttpRuntime.AppDomainAppPath;
                path = strDomainAppPath.Substring(0, strDomainAppPath.LastIndexOf("\\")) + "\\Upload\\" + category + "\\" + fileName;

                if (File.Exists(path))
                {
                    string dictName = Path.GetDirectoryName(path);
                    string pureName = Path.GetFileNameWithoutExtension(path);
                    string extName = Path.GetExtension(path);
                    File.Move(path, dictName + "\\" + pureName + "_" + String.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + extName);
                }

                using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                {
                    Byte[] bData = Convert.FromBase64String(fileContent);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(bData);
                    bw.Flush();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(path));
                //ErrorMessage.ErrorMess(ex.Message);
            }

            return path;
        }


        public static DataSet GetData(string path, string fileSuffix)
        {


            if (string.IsNullOrEmpty(fileSuffix))

                return null;


            using (DataSet ds = new DataSet())
            {

                //判断Excel文件是2003版本还是2007版本

                string connString = "";

                if (fileSuffix == ".xls")

                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";

                else

                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

                //读取文件

                string sql_select = " SELECT * FROM [Sheet1$]";

                using (OleDbConnection conn = new OleDbConnection(connString))

                using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                {

                    conn.Open();

                    cmd.Fill(ds);

                }

                if (ds == null || ds.Tables.Count <= 0) return null;

                return ds;

            }

        }



    }
}
