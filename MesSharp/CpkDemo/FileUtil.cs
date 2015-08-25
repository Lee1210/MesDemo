using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;

namespace FileClizz
{
    public class FileUtil
    {
        #region delete
        //========================================  
        //实现一个静态方法将指定文件夹下面的所有内容Detele
        //测试的时候要小心操作，删除之后无法恢复。
        //========================================
        public static void DeleteDir(string aimPath)
        {
            try
            {
                //检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] !=
                    Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                //得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
                //string[]fileList=  Directory.GetFiles(aimPath);
                string[] fileList = Directory.GetFileSystemEntries(aimPath);
                //遍历所有的文件和目录 
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个
                    //目录就递归Delete该目录下面的文件 
                    if (Directory.Exists(file))
                    {
                        DeleteDir(aimPath + Path.GetFileName(file));
                    }
                    //否则直接Delete文件 
                    else
                    {
                        File.Delete(aimPath + Path.GetFileName(file));
                    }
                }
                //删除文件夹 
                Directory.Delete(aimPath, true);
            }
            catch (Exception)
            {
                // MessageBox.Show(e.ToString());
            }
        }


        //========================================  
        //实现一个静态方法将指定文件夹下面的所有内容Detele
        //测试的时候要小心操作，删除之后无法恢复。
        //========================================
        public static void DeleteDir(string aimPath, int n)
        {
            try
            {
                //检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] !=
                    Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                //得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
                string[] fileList = Directory.GetFiles(aimPath);
                //string[] fileList = Directory.GetFileSystemEntries(aimPath);
                //遍历所有的文件和目录 
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个
                    //目录就递归Delete该目录下面的文件 
                    if (Directory.Exists(file))
                    {
                        DeleteDir(aimPath + Path.GetFileName(file), n);
                    }
                    //否则直接Delete文件 
                    else
                    {
                        File.Delete(aimPath + Path.GetFileName(file));
                    }
                }
                //删除文件夹 
                if (n == 1)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(aimPath);
                    //取得源文件夹下的所有子文件夹名称 
                    DirectoryInfo[] ziPath = dirInfo.GetDirectories();
                    for (int j = 0; j < ziPath.Length; j++)
                    {
                        //获取子文件夹名
                        string strZiPath = aimPath + "\\" + ziPath[j];
                        //删除当前文件夹
                        Directory.Delete(strZiPath, true);
                    }
                }
                else
                {
                    Directory.Delete(aimPath, true);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(e.ToString());
            }
        }
        #endregion


        #region  copy
        ///拷贝文件夹(包括子文件夹)到指定文件夹下
        ///源文件夹和目标文件夹均需绝对路径
        ///格式:CopyFolder(源文件夹,目标文件夹)
        //----------------------------------------
        //作者 14
        //----------------------------------------
        /// <summary>
        /// 拷贝文件夹(包括子文件夹)到指定文件夹下
        /// 文件和文件夹分开复制，当是文件夹则递归复制
        /// </summary>
        /// <param name="strFromPath">待复制地址</param>
        /// <param name="strToPath">目标地址</param>
        public static void CopyFolder(string strFromPath, string strToPath)
        {
            //如果源文件夹不存在，则创建  
            if (!Directory.Exists(strFromPath))
            {
                Directory.CreateDirectory(strFromPath);
            }
            //取得要拷贝的文件夹名  
            string strFolderName = strFromPath.Substring(
                strFromPath.LastIndexOf("\\") + 1,
                strFromPath.Length -
                strFromPath.LastIndexOf("\\") - 1);
            //如果目标文件夹中没有源文件夹
            //则在目标文件夹中创建源文件夹 
            if (!Directory.Exists(
                strToPath + "\\" + strFolderName))
            {
                Directory.CreateDirectory(
                    strToPath + "\\" + strFolderName);
            }
            //创建数组保存源文件夹下的文件名  
            string[] strFiles =
                Directory.GetFiles(strFromPath);
            //循环拷贝文件 
            for (int i = 0; i < strFiles.Length; i++)
            {
                //取得拷贝的文件名，只取文件名，地址截掉。
                string strFileName = strFiles[i].Substring(
                    strFiles[i].LastIndexOf("\\") + 1,
                    strFiles[i].Length -
                    strFiles[i].LastIndexOf("\\") - 1);
                //开始拷贝文件,true表示覆盖同名文件  
                File.Copy(
                    strFiles[i],
                    strToPath + "\\" + strFolderName +
                    "\\" + strFileName,
                    true);
            }
            //创建DirectoryInfo实例  
            DirectoryInfo dirInfo =
                new DirectoryInfo(strFromPath);
            //取得源文件夹下的所有子文件夹名称 
            DirectoryInfo[] ziPath =
                dirInfo.GetDirectories();
            for (int j = 0; j < ziPath.Length; j++)
            {
                //获取所有子文件夹名  
                string strZiPath = strFromPath + "\\" +
                    ziPath[j];
                //把得到的子文件夹当成新的
                //源文件夹，从头开始新一轮的拷贝
                CopyFolder(
                    strZiPath,
                    strToPath + "\\" + strFolderName);
            }
        }
        #endregion


     

        #region  move
        ///拷贝文件夹(包括子文件夹)到指定文件夹下
        ///源文件夹和目标文件夹均需绝对路径
        ///格式:CopyFolder(源文件夹,目标文件夹)
        //----------------------------------------
        //作者 14
        //----------------------------------------
        /// <summary>
        /// 拷贝文件夹(包括子文件夹)到指定文件夹下
        /// 文件和文件夹分开复制，当是文件夹则递归复制
        /// </summary>
        /// <param name="strFromPath">待复制地址</param>
        /// <param name="strToPath">目标地址</param>
        public static void MoveFolder(string strFromPath, string strToPath)
        {
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(strFromPath);
            //如果目标文件夹中没有源文件夹
            //则在目标文件夹中创建源文件夹 
            if (!Directory.Exists(
                strToPath))
            {
                Directory.CreateDirectory(strToPath);
            }
            //创建数组保存源文件夹下的文件名  
            FileInfo[] fileInfos = sourceDirectoryInfo.GetFiles();
            //循环拷贝文件 
            foreach (var fileInfo in fileInfos)
            {
                try
                {
                    File.Move(fileInfo.FullName, strToPath + "\\" + fileInfo.Name);
                }
                catch (Exception)
                {
                    // ignored
                }
            }


            //取得源文件夹下的所有子文件夹名称 
            DirectoryInfo[] subDirectoryInfos =
                sourceDirectoryInfo.GetDirectories();
            foreach (var subDirectoryInfo in subDirectoryInfos)
            {
                MoveFolder(
                    subDirectoryInfo.FullName,
                    strToPath + "\\" + subDirectoryInfo.Name);
            }
        }
        #endregion

        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="fileToUpZip">待解压的文件</param>
        /// <param name="zipedFolder">指定解压目标目录</param>
        /// <param name="password"></param>
        public static void UnZip(string fileToUpZip, string zipedFolder, string password)
        {
            ZipDll.ZipClass.UnZip(fileToUpZip, zipedFolder, password);
        }

        /// <summary>
        /// 压缩文件 和 文件夹
        /// </summary>
        /// <param name="fileToZip">待压缩的文件或文件夹，全路径格式</param>
        /// <param name="zipedFile">压缩后生成的压缩文件名，全路径格式</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Zip(string fileToZip, string zipedFile, string password)
        {
            return ZipDll.ZipClass.Zip(fileToZip, zipedFile, password);
        }

        public static bool WriteLog(string path, string log, out string err)
        {
            //try
            //{
            //    FileStream fs = new FileStream(string.Format("d:\\CPK_TEST_{0}.log", DateTime.Now.ToShortDateString()), FileMode.Create);
            //    StreamWriter sw = new StreamWriter(fs);
            //    DirectoryInfo thisOne = new DirectoryInfo(Path);
            //    str = ListTreeShow(thisOne, 0, str);
            //    sw.WriteLine(str);
            //    if (sw != null) sw.Close();
            //    if (fs != null) fs.Close();
            //}
            //catch (Exception)
            //{

            //}
            bool flag = true;
            err = string.Empty;
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (log != null && log.Trim() != "")
                {
                    //fs = new FileStream(string.Format(path + "_{0}.log", DateTime.Now.ToShortDateString()), FileMode.Append);
                    fs = new FileStream(string.Format(path + ".log"), FileMode.Append);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(log);
                }

            }
            catch (Exception ex)
            {
                flag = false;
                Console.WriteLine("FAIL:" + ex + "[" + ex.Message + "]");
                err = "FAIL:" + ex + "[" + ex.Message + "]";
            }
            finally
            {
                sw?.Close();
                fs?.Close();
            }
            return flag;
        }

        /// <summary>
        /// 比较时间是否相等，精度minute
        /// </summary>
        /// <param name="arrTime"></param>
        /// <returns></returns>
        public static bool BoolTimeEquals(string[] arrTime)
        {
            bool flag = false;
            DateTime systemTime = DateTime.Now;  //获得系统时间
            foreach (var t in arrTime)
            {
                if (t != "")
                {
                    DateTime setTime = DateTime.Parse(t);
                    TimeSpan ts = systemTime.Subtract(setTime);  //系统时间减去数据库时间
                    //int i = ts.Days;  //间隔的天数
                    int min = ts.Minutes;//间隔分钟
                    if (min == 0)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }

        public static bool writeLog(string path, string log, out string err)
        {
            //try
            //{
            //    FileStream fs = new FileStream(string.Format("d:\\CPK_TEST_{0}.log", DateTime.Now.ToShortDateString()), FileMode.Create);
            //    StreamWriter sw = new StreamWriter(fs);
            //    DirectoryInfo thisOne = new DirectoryInfo(Path);
            //    str = ListTreeShow(thisOne, 0, str);
            //    sw.WriteLine(str);
            //    if (sw != null) sw.Close();
            //    if (fs != null) fs.Close();
            //}
            //catch (Exception)
            //{

            //}
            bool flag = true;
            err = string.Empty;
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (log != null && log.Trim() != "")
                {
                    //fs = new FileStream(string.Format(path + "_{0}.log", DateTime.Now.ToShortDateString()), FileMode.Append);
                    fs = new FileStream(string.Format(path + ".log"), FileMode.Append);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(log);
                }

            }
            catch (Exception ex)
            {
                flag = false;
                Console.WriteLine("FAIL:" + ex.ToString() + "[" + ex.Message + "]");
                err = "FAIL:" + ex.ToString() + "[" + ex.Message + "]";
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
            return flag;
        }
    }
}
