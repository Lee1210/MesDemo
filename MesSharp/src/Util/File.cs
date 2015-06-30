using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Util {
    /// <summary>
    /// 文件及流操作
    /// </summary>
    public partial class File {
        
        #region Read(读取文件到字符串)

        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string Read(string filePath) {
            return Read(filePath, Config.DefaultEncoding);
        }

        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="encoding">字符编码</param>
        public static string Read(string filePath, Encoding encoding) {
            if (!System.IO.File.Exists(filePath))
                return string.Empty;
            using (var reader = new StreamReader(filePath, encoding)) {
                return reader.ReadToEnd();
            }
        }

        #endregion

        #region ReadToBytes(将文件读取到字节流中)

        /// <summary>
        /// 将文件读取到字节流中
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static byte[] ReadToBytes( string filePath ) {
            if ( !System.IO.File.Exists( filePath ) )
                return null;
            FileInfo fileInfo = new FileInfo( filePath );
            int fileSize = (int)fileInfo.Length;
            using ( BinaryReader reader = new BinaryReader( fileInfo.Open( FileMode.Open ) ) ) {
                return reader.ReadBytes( fileSize );
            }
        }

        #endregion

        #region Write(将字节流写入文件)

        /// <summary>
        /// 将字节流写入文件,文件不存在则创建
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="bytes">数据</param>
        public static void Write( string filePath, byte[] bytes ) {
            if ( string.IsNullOrWhiteSpace( filePath ) )
                return;
            if ( bytes == null )
                return;
            FileInfo file = new FileInfo( filePath );
            using ( FileStream fileStream = file.Open( FileMode.OpenOrCreate ) ) {
                fileStream.Write( bytes, 0, bytes.Length );
            }
        }

        #endregion

        #region Delete(删除文件)

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void Delete( string filePath ) {
            if ( string.IsNullOrWhiteSpace( filePath ) )
                return;
            System.IO.File.Delete( filePath );
        }

        #endregion

        #region GetAllFiles(获取目录中全部文件列表)

        /// <summary>
        /// 获取目录中全部文件列表，包括子目录
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        public static List<string> GetAllFiles( string directoryPath ) {
            return Directory.GetFiles( directoryPath,"*.*",SearchOption.AllDirectories ).ToList();
        }

        #endregion
    }
}
