using System;

namespace Util {
    /// <summary>
    /// 文件及流操作 - 文件路径操作
    /// </summary>
    public partial class File {
        /// <summary>
        /// 从文件路径中获取目录路径
        /// </summary>
        /// <param name="filePath">文件路径,可以是绝对路径，也可以是相对路径</param>
        public static string GetDirectoryFromPath( string filePath ) {
            if ( string.IsNullOrWhiteSpace( filePath ) )
                return string.Empty;
            filePath = filePath.Replace( "/", "\\" );
            int lastIndex = filePath.LastIndexOf("\\", StringComparison.Ordinal);
            return filePath.Substring( 0, lastIndex + 1 );
        }

        /// <summary>
        /// 连接基路径和子路径,比如把 c: 与 test.doc 连接成 c:\test.doc
        /// </summary>
        /// <param name="basePath">基路径,范例：c:</param>
        /// <param name="subPath">子路径,可以是文件名, 范例：test.doc</param>
        public static string JoinPath( string basePath, string subPath ) {
            basePath = basePath.TrimEnd( '/' ).TrimEnd( '\\' );
            subPath = subPath.TrimStart( '/' ).TrimStart( '\\' );
            string path = basePath + "\\" + subPath;
            return path.Replace( "/", "\\" ).ToLower();
        }
    }
}
