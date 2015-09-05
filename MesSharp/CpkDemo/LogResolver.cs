using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FileClizz;

using Mes.Utility.Develop;
using Mes.Utility.Extensions;


namespace CpkDemo
{
    public enum LogFileType
    {
        // ReSharper disable once InconsistentNaming
        MAIN = 1,
        // ReSharper disable once InconsistentNaming
        RECEIVERAMPL = 2,
        // ReSharper disable once InconsistentNaming
        SEC = 3,
        // ReSharper disable once InconsistentNaming
        SPEAKERAMPL = 4,
        副麦 = 5,
        听筒 = 6,
        扬声器 = 7,
        扬声器右 = 8,
        扬声器左 = 9,
        主麦 = 10,
        // ReSharper disable once InconsistentNaming
        HTML = 11,
        // ReSharper disable once InconsistentNaming
        OTHER = 20
    }
    public class LogResolver
    {
        public static string ExcelLogFromPath = ConfigurationManager.AppSettings["Excel_LOG_FROM_PATH"];
        public static string ExcelLogToPath = ConfigurationManager.AppSettings["Excel_LOG_TO_PATH"];
        public static string CsvLogFromPath = ConfigurationManager.AppSettings["CSV_LOG_FROM_PATH"];
        public static string CsvLogToPath = ConfigurationManager.AppSettings["CSV_LOG_TO_PATH"];
        public static string CsvnoLogFromPath = ConfigurationManager.AppSettings["CSVNO_LOG_FROM_PATH"];
        public static string CsvnoLogToPath = ConfigurationManager.AppSettings["CSVNO_LOG_TO_PATH"];
        public static string HtmlLogFromPath = ConfigurationManager.AppSettings["HTML_LOG_FROM_PATH"];
        public static string HtmlLogToPath = ConfigurationManager.AppSettings["HTML_LOG_TO_PATH"];
        public string CpkLogFromPath = ConfigurationManager.AppSettings["Cpk_LOG_FROM_PATH"];
        public string CpkLogToPath = ConfigurationManager.AppSettings["Cpk_LOG_TO_PATH"];
        public static string SysLogPath = ConfigurationManager.AppSettings["SYS_LOG_PATH"];
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        public  void CpkExcute(string logFromPath,
           string logToPath,
           Action<FileInfo, DataTable, string> transfaseDataTable,
           string operTable)
        {
            double move = 0, read = 0, zip = 0, del = 0;
            var fileCount = 0;
            DataTable dataTable = new DataTable();
            {
                dataTable.Columns.Add("PlatForm", typeof(string));
                dataTable.Columns.Add("ProjectName", typeof(string));
                dataTable.Columns.Add("Opcode", typeof(string));
                dataTable.Columns.Add("Ip", typeof(string));
                dataTable.Columns.Add("Sn", typeof(string));
                dataTable.Columns.Add("TestDate", typeof(int));
                dataTable.Columns.Add("TestTime", typeof(string));
                dataTable.Columns.Add("Result", typeof(int));
                dataTable.Columns.Add("Wgsn", typeof(string));
                dataTable.Columns.Add("Tch", typeof(string));
                dataTable.Columns.Add("Pcl", typeof(string));
                dataTable.Columns.Add("TestItem", typeof(string));
                dataTable.Columns.Add("TestVal", typeof(string));
                dataTable.Columns.Add("MinVal", typeof(double));
                dataTable.Columns.Add("MaxVal", typeof(double));
                dataTable.Columns.Add("ReadLogDate", typeof(DateTime));
                dataTable.Columns.Add("LogFileName", typeof(string));
                dataTable.Columns.Add("IsDeleted", typeof(bool));
                dataTable.Columns.Add("CreatedTime", typeof(DateTime));
                dataTable.Columns.Add("ZipFileName", typeof(string));
            }
            dataTable.TableName = "Cpks";
            var total = CodeTimer.TimeLong("总共时间",
                1,
                () =>
                {
                    string zipFileName = DateTime.Now.ToString("yyyyMMddHHmm");
                    string logToAbsPath = logToPath + "\\" + zipFileName;

                    try
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        move = CodeTimer.TimeLong("移动数据", 1, () => FileUtil.MoveFolder(logFromPath, logToAbsPath, ref fileCount));

                        DirectoryInfo myFolder = new DirectoryInfo(logToAbsPath);
                        read = CodeTimer.TimeLong("解析数据", 1, () =>
                        {
                            TraverseFolder(myFolder, transfaseDataTable, dataTable, zipFileName);
                            InsertDb(dataTable);
                        }
                        );

                        zip = CodeTimer.TimeLong("压缩数据",
                            1,
                            () =>
                            {
                                if (FileUtil.Zip(logToAbsPath, logToAbsPath + ".zip", ""))
                                {
                                    del = CodeTimer.TimeLong("删除数据", 1, () => FileUtil.DeleteDir(logToAbsPath));
                                }
                            });
                        zip = zip - del;
                    }
                    catch (Exception e)
                    {
                        string log = "解析数据出错，原因：[" + e + "、消息：" + e.Message + "]";
                        Console.WriteLine(@"	Err:	" + e.Message);
                        WriteSysLog(log);
                    }
                });
            AddOperLog(fileCount, move, read, zip, del, total, operTable);
        }

        public void TestLogExcute(string logFromPath,
            string logToPath,
            Action<FileInfo, DataTable, string> transfaseDataTable,
            string operTable)
        {
            double move = 0, read = 0, zip = 0, del = 0;
            var fileCount = 0;
            DataTable dataTable = new DataTable();
            {
                dataTable.Columns.Add("PlatForm", typeof(string));
                dataTable.Columns.Add("ProjectName", typeof(string));
                dataTable.Columns.Add("Opcode", typeof(string));
                dataTable.Columns.Add("Ip", typeof(string));
                dataTable.Columns.Add("Sn", typeof(string));
                dataTable.Columns.Add("TestDate", typeof(int));
                dataTable.Columns.Add("TestTime", typeof(string));
                dataTable.Columns.Add("Result", typeof(int));
                dataTable.Columns.Add("Wgsn", typeof(string));
                dataTable.Columns.Add("Tch", typeof(string));
                dataTable.Columns.Add("Pcl", typeof(string));
                dataTable.Columns.Add("TestItem", typeof(string));
                dataTable.Columns.Add("TestVal", typeof(string));
                dataTable.Columns.Add("MinVal", typeof(double));
                dataTable.Columns.Add("MaxVal", typeof(double));
                dataTable.Columns.Add("ReadLogDate", typeof(DateTime));
                dataTable.Columns.Add("LogFileName", typeof(string));
                dataTable.Columns.Add("IsDeleted", typeof(bool));
                dataTable.Columns.Add("CreatedTime", typeof(DateTime));
                dataTable.Columns.Add("ZipFileName", typeof(string));
            }
            dataTable.TableName = "Cpks";
            var total = CodeTimer.TimeLong("总共时间",
                1,
                () =>
                {
                    string zipFileName = DateTime.Now.ToString("yyyyMMddHHmm");
                    string logToAbsPath = logToPath + "\\" + zipFileName;

                    try
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        move = CodeTimer.TimeLong("移动数据", 1, () => FileUtil.MoveFolder(logFromPath, logToAbsPath, ref fileCount));

                        DirectoryInfo myFolder = new DirectoryInfo(logToAbsPath);
                        read = CodeTimer.TimeLong("解析数据", 1, () =>
                        {
                            TraverseFolder(myFolder, transfaseDataTable, dataTable, zipFileName);
                            InsertDb(dataTable);
                        }
                        );

                        zip = CodeTimer.TimeLong("压缩数据",
                            1,
                            () =>
                            {
                                if (FileUtil.Zip(logToAbsPath, logToAbsPath + ".zip", ""))
                                {
                                    del = CodeTimer.TimeLong("删除数据", 1, () => FileUtil.DeleteDir(logToAbsPath));
                                }
                            });
                        zip = zip - del;
                    }
                    catch (Exception e)
                    {
                        string log = "解析数据出错，原因：[" + e + "、消息：" + e.Message + "]";
                        Console.WriteLine(@"	Err:	" + e.Message);
                        WriteSysLog(log);
                    }
                });
            AddOperLog(fileCount, move, read, zip, del, total, operTable);

        }
        public void AddOperLog(int log, double move, double read, double zip, double del, double total, string actionName)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = ConnectionString;
            conn.Open();
            //SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
            string sql =
                string.Format(
                    "insert into OperationLogs(Num,MoveTimes,ReadTimes,ZipTimes,DeleteTimes,TotalTimes,IsDeleted,CreatedTime,OperationType)values({0},{1},{2},{3},{4},{5},0,'{6}','{7}')",
                    log,
                    move,
                    read,
                    zip,
                    del,
                    total,
                    DateTime.Now,
                    actionName);
            //请在插入数据的同时检查约束，如果发生错误调用sqlbulkTransaction事务
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }


        public void TraverseFolder(DirectoryInfo dir, Action<FileInfo, DataTable, string> act, DataTable dt, string zipName) //搜索文件夹中的文件
        {
            foreach (var file in dir.GetFiles())
            {
                try
                {

                    act(file, dt, zipName);
                    if (dt.Rows.Count > 4500)
                    {
                        InsertDb(dt);
                        dt.Rows.Clear();
                    }
                }
                catch (Exception e)
                {
                    // 移动文件到Error,记录Log
                }
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                TraverseFolder(d, act, dt, zipName);
            }
        }



        private static StringCollection ReadFileIntoStringCollection(string chosenFile)
        {
            //const int MaxBytes = 65536;
            StreamReader sr = new StreamReader(chosenFile, Encoding.Default);
            StringCollection result = new StringCollection();
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                result.Add(nextLine);
            }
            sr.Close();
            return result;
        }



        public void CpkTransfaseDataTable(FileInfo chosenFile, DataTable dt, string zipName)
        {
            string logFile = chosenFile.Name;
            string[] timesArr = chosenFile.Name.Split('_');
            string sn = timesArr[0];
            string time = timesArr[1];
            int testDate;
            int testTime;
            try
            {
                testDate = Convert.ToInt32(time.Substring(0, 8));
                testTime = Convert.ToInt32(time.Substring(8));
            }
            catch (Exception)
            {
                DateTime currentTime = DateTime.Now;
                string dateStr = currentTime.ToString("yyyyMMdd");
                string timeStr = currentTime.ToString("HHmmss");
                testDate = Convert.ToInt32(dateStr);
                testTime = Convert.ToInt32(timeStr);
            }

            StringCollection linesCollection = ReadFileIntoStringCollection(chosenFile.FullName);
            string[] linesArray = new string[linesCollection.Count];
            linesCollection.CopyTo(linesArray, 0);

            string platForm = linesArray[0].Split(':', ';')[1]; // 平台
            string projectName = linesArray[1].Split(':', ';')[1]; // 项目名
            string stationName = linesArray[2].Split(':', ';')[1]; // 站点
            string ip = linesArray[3].Split(':', ';')[1]; // IP
            string result = linesArray[7].Split(':', ';')[1]; //
            DateTime readLogDate = DateTime.Now;

            foreach (var line in linesArray.Skip(11))
            {
                try
                {
                    DataRow dr = dt.NewRow();
                    string[] tStrings = line.Split('_', ':', ',', ';');

                    if (tStrings.Count() == 9)
                    {
                        var testList = tStrings.ToList();
                        testList.RemoveAt(1);
                        tStrings = testList.ToArray<string>();
                    }
                    string wgsn = tStrings[0]; // 测试频段
                    string tch = tStrings[1]; // 信道
                    string pcl = tStrings[2]; // 功率等级
                    string testItem = tStrings[3]; // 测试类型(项)
                    double testVal = double.Parse(tStrings[4]); // 测试值
                    double minVal = double.Parse(tStrings[5]); // 最小值
                    double maxVal = double.Parse(tStrings[6]); // 最大值

                    dr[0] = platForm.ToUpper().Replace(" ", "");
                    dr[1] = projectName.ToUpper().Replace(" ", "");
                    dr[2] = stationName.ToUpper().Replace(" ", "");
                    dr[3] = ip.Replace(" ", "");
                    dr[4] = sn.ToUpper().Replace(" ", "");
                    dr[5] = testDate;
                    dr[6] = testTime;
                    dr[7] = result.ToUpper().Equals("PASS") ? 1 : 0;
                    dr[8] = wgsn.ToUpper().Replace(" ", "");
                    dr[9] = tch.ToUpper().Replace(" ", "");
                    dr[10] = pcl.ToUpper().Replace(" ", "");
                    dr[11] = testItem.ToUpper().Replace(" ", "");
                    dr[12] = testVal;
                    dr[13] = minVal;
                    dr[14] = maxVal;
                    //  dr[15] = System.Guid.NewGuid().ToString().ToUpper();
                    dr[15] = readLogDate;
                    // dr[17] = readLogTime;
                    dr[16] = logFile.Replace(" ", "");
                    dr[17] = 0;
                    dr[18] = DateTime.Now;
                    dr[19] = zipName.Replace(" ", "");
                    dt.Rows.Add(dr);


                }
                catch (Exception)
                {
                    var log = "log格式不正确[" + line + "]、log[" + logFile + "]";
                    WriteSysLog(log);

                }
            }

        }

        public void TestLogTransfaseDataTable(FileInfo chosenFile, DataTable dt, string zipName)
        {
            string fileNameArr = chosenFile.Name.Split('_').First();
            LogFileType test;
            try
            {
                test = fileNameArr.ToUpper().CastTo<LogFileType>();
            }
            catch (Exception)
            {
                test = LogFileType.OTHER;
            }

            switch (test)
            {
                case LogFileType.MAIN:
                    StringCollection linesCollection = ReadFileIntoStringCollection(chosenFile.FullName);
                    string[] linesArray = new string[linesCollection.Count];
                    linesCollection.CopyTo(linesArray, 0);
                    foreach (var line in linesArray.Skip(3))
                    {
                        string sn = line.Split('\t')[2];

                    }
                    break;
                case LogFileType.RECEIVERAMPL:
                    break;
                case LogFileType.SEC:
                    break;
                case LogFileType.SPEAKERAMPL:
                    break;
                case LogFileType.副麦:
                    break;
                case LogFileType.听筒:
                    break;
                case LogFileType.扬声器:
                    break;
                case LogFileType.扬声器右:
                    break;
                case LogFileType.扬声器左:
                    break;
                case LogFileType.主麦:
                    break;
                case LogFileType.OTHER:
                    break;
            }

        }

        private static void InsertDb(DataTable dt)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = ConnectionString;
            conn.Open();

            SqlTransaction sqlbulkTransaction = conn.BeginTransaction();

            //请在插入数据的同时检查约束，如果发生错误调用sqlbulkTransaction事务
            SqlBulkCopy copy = new SqlBulkCopy(conn, SqlBulkCopyOptions.CheckConstraints, sqlbulkTransaction);
            copy.BatchSize = 5000; //每次传输的行数
            copy.DestinationTableName = dt.TableName; //定义目标表    
            foreach (DataColumn dc in dt.Columns)
            {
                copy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
            }

            try
            {
                copy.WriteToServer(dt); //将数据写入Sql Server数据表   
                sqlbulkTransaction.Commit();
            }
            catch (Exception ex)
            {
                //string err = "FAIL" + ex.Message;
                sqlbulkTransaction.Rollback();
                string log = "log数据写入DB失败，原因：[" + ex + "消息：" + ex.Message + "]";
                Console.WriteLine(log);
                WriteSysLog(log);
            }
            finally
            {
                copy.Close();
                conn.Close();
            }
        }

        private static void WriteSysLog(string log)
        {
            string err;
            DateTime logTime = DateTime.Now;
            string logName = logTime.ToString("yyyy-MM-dd-HH");
            string writeTime = logTime.ToString("yyyy-MM-dd HH:mm:ss");
            string path = SysLogPath + "\\" + logName;
            log = writeTime + "\t" + log;
            if (!FileUtil.writeLog(path, log, out err))
            {
                Console.WriteLine(err);
            }
        }
    }
}
