// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-08-26 10:15</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using DBUtility;

using FileClizz;

using Mes.Utility.Develop;


namespace CpkDemo
{
    internal class Program
    {
        public static string ExcelLogFromPath = ConfigurationManager.AppSettings["Excel_LOG_FROM_PATH"];
        public static string ExcelLogToPath = ConfigurationManager.AppSettings["Excel_LOG_TO_PATH"];
        public static string CsvLogFromPath = ConfigurationManager.AppSettings["CSV_LOG_FROM_PATH"];
        public static string CsvLogToPath = ConfigurationManager.AppSettings["CSV_LOG_TO_PATH"];
        public static string CsvnoLogFromPath = ConfigurationManager.AppSettings["CSVNO_LOG_FROM_PATH"];
        public static string CsvnoLogToPath = ConfigurationManager.AppSettings["CSVNO_LOG_TO_PATH"];
        public static string HtmlLogFromPath = ConfigurationManager.AppSettings["HTML_LOG_FROM_PATH"];
        public static string HtmlLogToPath = ConfigurationManager.AppSettings["HTML_LOG_TO_PATH"];
        public static string CpkLogFromPath = ConfigurationManager.AppSettings["Cpk_LOG_FROM_PATH"];
        public static string CpkLogToPath = ConfigurationManager.AppSettings["Cpk_LOG_TO_PATH"];
        public static string SysLogPath = ConfigurationManager.AppSettings["SYS_LOG_PATH"];
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        #region ExcelHead
       
        #endregion
        private static string ZipFileName { get; set; }
        

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            //var dt3 = CpksFile();
            //InsertDb(dt3);

            ReadAndInsert();
            Console.ReadLine();
        }

        public static void  ReadAndInsert()
        {
            
            DirectoryInfo myFolder = new DirectoryInfo(CpkLogFromPath);

            foreach (var directory in myFolder.GetDirectories())
            {
                CodeTimer.Time(directory.Name,1,
                    () =>
                    {
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
                        GetAll2(directory, CpkTransfaseDataTable2, dataTable, directory.Name);
                        InsertDb(dataTable);
                        dataTable.Dispose();
                    });
                
            }

        }

        private static void Cpk()
        {
            DataTable dtTable = new DataTable();
            {
                dtTable.Columns.Add("PlatForm", typeof(string));
                dtTable.Columns.Add("ProjectName", typeof(string));
                dtTable.Columns.Add("Opcode", typeof(string));
                dtTable.Columns.Add("Ip", typeof(string));
                dtTable.Columns.Add("Sn", typeof(string));
                dtTable.Columns.Add("TestDate", typeof(int));
                dtTable.Columns.Add("TestTime", typeof(string));
                dtTable.Columns.Add("Result", typeof(int));
                dtTable.Columns.Add("Wgsn", typeof(string));
                dtTable.Columns.Add("Tch", typeof(string));
                dtTable.Columns.Add("Pcl", typeof(string));
                dtTable.Columns.Add("TestItem", typeof(string));
                dtTable.Columns.Add("TestVal", typeof(string));
                dtTable.Columns.Add("MinVal", typeof(double));
                dtTable.Columns.Add("MaxVal", typeof(double));
                dtTable.Columns.Add("ReadLogDate", typeof(DateTime));
                dtTable.Columns.Add("LogFileName", typeof(string));
                dtTable.Columns.Add("ZipFileName", typeof(string));
                dtTable.Columns.Add("IsDeleted", typeof(bool));
                dtTable.Columns.Add("CreatedTime", typeof(DateTime));
            }

            dtTable.TableName = "Cpks";
            
        }

        private static void CpkCount()
        {
            List<double> list = new List<double>();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(21.00001);
            }

            #region

            List<double> list2 = new List<double>
            {
                31.90799,
                29.79160,
                29.50238,
                31.92334,
                29.11130,
                29.14536,
                29.87259,
                29.07852,
                29.03458,
                29.29665,
                29.85486,
                29.29509,
                29.60806,
                29.71625,
                29.67749,
                29.51538,
                29.87296,
                29.11569,
                29.46088,
                29.70709,
                29.76892,
                29.60690,
                29.19720,
                29.90610,
                29.00107,
                29.78519,
                29.63794,
                29.43466,
                29.61835,
                29.19067,
                32.42059,
                29.82275,
                30.35547,
                29.87469,
                29.77762,
                29.96130,
                29.64117,
                31.76547,
                29.97348,
                29.23676,
                29.76099,
                29.36694,
                29.25879,
                29.93500,
                29.86017,
                29.83698,
                29.92703,
                29.58566,
                29.70892,
                29.53970,
                29.22122,
                29.57635,
                28.48273,
                29.06714,
                29.57147,
                29.11792,
                29.49380,
                29.02368,
                29.62039,
                29.25122,
                29.97784,
                29.56339,
                29.17917,
                28.71713,
                29.12911,
                29.37393,
                29.01364,
                28.29290,
                29.21085,
                29.08041,
                29.82611,
                29.76154,
                29.54047,
                29.01254,
                29.32906,
                29.93896,
                29.99291,
                29.53745,
                28.42700,
                29.93921,
                29.80240,
                29.13794,
                29.47354,
                29.35989,
                29.62259,
                29.22905,
                29.66107,
                29.75870,
                29.52295,
                29.29819,
                29.01550,
                29.51068,
                29.04459,
                29.80273,
                29.32929,
                29.98413,
                29.29372,
                29.54599,
                29.76578,
                34.89600,
                29.42926,
                29.46680,
                29.88257,
                33.94669,
                29.48645,
                29.61154,
                29.01624,
                29.93839,
                33.64954,
                29.07120,
                32.37460,
                29.62926,
                34.82108,
                29.12027,
                29.89410,
                29.93588,
                29.08691,
                29.85284,
                29.45514,
                29.87183,
                29.22958,
                29.80743,
                29.97229,
                29.71283,
                29.04205,
                29.39407,
                29.07495,
                34.07278,
                29.84467,
                29.12914,
                29.44449,
                29.10437,
                29.01788,
                29.70575,
                29.88977,
                29.38641,
                32.42972,
                34.67401,
                29.08356,
                29.79517,
                29.48511,
                29.49359,
                29.44803,
                34.87811,
                23.66922,
                29.39581,
                29.26065,
                29.19528,
                34.46124,
                29.26578,
                34.48819,
                29.98370,
                33.99762,
                29.68927,
                29.26477,
                30.71078,
                29.70987,
                29.85504,
                31.22095,
                31.77148,
                33.47037,
                29.29059,
                29.45456,
                31.80475,
                29.52518,
                29.72549,
                29.66348,
                31.71445,
                29.09833,
                29.28030,
                30.67929,
                33.63608
            };
            List<double> list3 = new List<double>
            {
                31.90799,
                31.79160,
                31.50238,
                31.92334,
                31.11130,
                31.14536,
                31.87259,
                31.07852,
                31.03458,
                31.31665,
                31.85486,
                31.31509,
                31.60806,
                31.71625,
                31.67749,
                31.51538,
                31.87316,
                31.11569,
                31.46088,
                31.70709,
                31.76892,
                31.60690,
                31.19720,
                31.90610,
                31.00107,
                31.78519,
                31.63794,
                31.43466,
                31.61835,
                31.19067,
                31.42059,
                31.82275,
                31.35547,
                31.87469,
                31.77762,
                31.96130,
                31.64117,
                31.76547,
                31.97348,
                31.23676,
                31.76099,
                31.36694,
                31.25879,
                31.93500,
                31.86017,
                31.83698,
                31.92703,
                31.58566,
                31.70892,
                31.53970,
                31.22122,
                31.57635,
                31.48273,
                31.06714,
                31.57147,
                31.11792,
                31.49380,
                31.02368,
                31.62039,
                31.25122,
                31.97784,
                31.56339,
                31.17917,
                31.71713,
                31.13111,
                31.37393,
                31.01364,
                31.31310,
                31.21085,
                31.08041,
                31.82611,
                31.76154,
                31.54047,
                31.01254,
                31.33106,
                31.93896,
                31.99311,
                31.53745,
                31.42700,
                31.93921,
                31.80240,
                31.13794,
                31.47354,
                31.35989,
                31.62259,
                31.23105,
                31.66107,
                31.75870,
                31.52315,
                31.31819,
                31.01550,
                31.51068,
                31.04459,
                31.80273,
                31.33131,
                31.98413,
                31.31372,
                31.54599,
                31.76578,
                31.89600,
                31.43126,
                31.46680,
                31.88257,
                31.94669,
                31.48645,
                31.61154,
                31.01624,
                31.93839,
                31.64954,
                31.07120,
                31.37460,
                31.63126,
                31.82108,
                31.12027,
                31.89410,
                31.93588,
                31.08691,
                31.85284,
                31.45514,
                31.87183,
                31.23158,
                31.80743,
                31.97231,
                31.71283,
                31.04205,
                31.39407,
                31.07495,
                31.07278,
                31.84467,
                31.13114,
                31.44449,
                31.10437,
                31.01788,
                31.70575,
                31.88977,
                31.38641,
                31.43172,
                31.67401,
                31.08356,
                31.79517,
                31.48511,
                31.49359,
                31.44803,
                31.87811,
                31.66922,
                31.39581,
                31.26065,
                31.19528,
                31.46124,
                31.26578,
                31.48819,
                31.98370,
                31.99762,
                31.68927,
                31.26477,
                31.71078,
                31.70987,
                31.85504,
                31.22095,
                31.77148,
                31.47037,
                31.31059,
                31.45456,
                31.80475,
                31.52518,
                31.72549,
                31.66348,
                31.71445,
                31.09833,
                31.28030,
                31.67931,
                31.63608,
            };

            #endregion

            CodeTimer.TimeLong("平均值",
                1,
                () =>
                {
                    Console.WriteLine(Average(list));
                });
            CodeTimer.TimeLong("标准差",
                1,
                () =>
                {
                    Console.WriteLine(StandardDeviation(list3, 35, 29));
                });
        }

        private static void Excute(string logFromPath, string logToPath, Action<FileInfo, DataTable> transfaseDataTable, string operTable)
        {
            double move = 0, read = 0, zip = 0, del = 0;
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
                    DirectoryInfo myFolder1 = new DirectoryInfo(logFromPath);
                    CountLog(myFolder1);
                    if (_counnLogNum <= 0)
                    {
                        return;
                    }
                    string zipFileName = DateTime.Now.ToString("yyyyMMddHHmm");

                    string logToAbsPath = logToPath + "\\" + zipFileName;

                    try
                    {
                        move = CodeTimer.TimeLong("移动数据", 1, () => FileUtil.MoveFolder(logFromPath, logToAbsPath));

                        DirectoryInfo myFolder = new DirectoryInfo(logToAbsPath);
                        read = CodeTimer.TimeLong("解析数据", 1, () =>
                        {
                            GetAll(myFolder, transfaseDataTable, dataTable);
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
                    }
                    catch (Exception e)
                    {
                        string log = "解析数据出错，原因：[" + e + "、消息：" + e.Message + "]";
                        Console.WriteLine("\tErr:\t" + e.Message);
                        WriteSysLog(log);
                    }
                });
            AddOperLog(_counnLogNum, move, read, zip, del, total, operTable);
            _counnLogNum = 0;
        }

        private static void AddOperLog(int log, double move, double read, double zip, double del, double total, string actionName)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = SqlHelper.ConnectionStringLocalTransaction;
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

        #region 递归

        public static void GetAll(DirectoryInfo dir, Action<FileInfo,DataTable> act,DataTable dt) //搜索文件夹中的文件
        {
            foreach (var file in dir.GetFiles())
            {
                try
                {
                    act(file,dt);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAll(d, act,dt);
            }
        }

        public static void GetAll2(DirectoryInfo dir, Action<FileInfo, DataTable,string> act, DataTable dt,string zipName) //搜索文件夹中的文件
        {
            foreach (var file in dir.GetFiles())
            {
                try
                {
                    act(file, dt, zipName);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAll2(d, act, dt, zipName);
            }
        }

        #endregion

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

        private static void CpkTransfaseDataTable(FileInfo chosenFile, DataTable dt )
        {
            string logFile = chosenFile.Name;
            string[] timesArr = chosenFile.Name.Split('_');
            string sn = timesArr[0];
            string time = timesArr[1];
            int testDate = 0;
            int testTime = 0;
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
                        var testList = tStrings.ToList<string>();
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

                    dr[0] = platForm.ToUpper();
                    dr[1] = projectName.ToUpper();
                    dr[2] = stationName.ToUpper();
                    dr[3] = ip;
                    dr[4] = sn.ToUpper();
                    dr[5] = testDate;
                    dr[6] = testTime;
                    dr[7] = result.ToUpper().Equals("PASS") ? 1 : 0;
                    dr[8] = wgsn.ToUpper();
                    dr[9] = tch.ToUpper();
                    dr[10] = pcl.ToUpper();
                    dr[11] = testItem.ToUpper();
                    dr[12] = testVal;
                    dr[13] = minVal;
                    dr[14] = maxVal;
                    //  dr[15] = System.Guid.NewGuid().ToString().ToUpper();
                    dr[15] = readLogDate;
                    // dr[17] = readLogTime;
                    dr[16] = logFile;
                    dr[17] = 0;
                    dr[18] = DateTime.Now;
                    dr[19] = ZipFileName;
                    dt.Rows.Add(dr);
                }
                catch (Exception)
                {
                    string log = "log格式不正确[" + line + "]、log[" + logFile + "]";
                    WriteSysLog(log);
                }
            }
            
        }

        private static void CpkTransfaseDataTable2(FileInfo chosenFile, DataTable dt,string zipName)
        {
            string logFile = chosenFile.Name;
            string[] timesArr = chosenFile.Name.Split('_');
            string sn = timesArr[0];
            string time = timesArr[1];
            int testDate = 0;
            int testTime = 0;
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
                        var testList = tStrings.ToList<string>();
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

                    dr[0] = platForm.ToUpper();
                    dr[1] = projectName.ToUpper();
                    dr[2] = stationName.ToUpper();
                    dr[3] = ip;
                    dr[4] = sn.ToUpper();
                    dr[5] = testDate;
                    dr[6] = testTime;
                    dr[7] = result.ToUpper().Equals("PASS") ? 1 : 0;
                    dr[8] = wgsn.ToUpper();
                    dr[9] = tch.ToUpper();
                    dr[10] = pcl.ToUpper();
                    dr[11] = testItem.ToUpper();
                    dr[12] = testVal;
                    dr[13] = minVal;
                    dr[14] = maxVal;
                    //  dr[15] = System.Guid.NewGuid().ToString().ToUpper();
                    dr[15] = readLogDate;
                    // dr[17] = readLogTime;
                    dr[16] = logFile;
                    dr[17] = 0;
                    dr[18] = DateTime.Now;
                    dr[19] = zipName;
                    dt.Rows.Add(dr);
                }
                catch (Exception e)
                {
                    string log = "log格式不正确[" + line + "]、log[" + logFile + "]";
                    WriteSysLog(log);
                }
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
                Int32 number = dt.Rows.Count;
                sqlbulkTransaction.Commit();
            }
            catch (Exception ex)
            {
                //string err = "FAIL" + ex.Message;
                sqlbulkTransaction.Rollback();
                string log = "log数据写入DB失败，原因：[" + ex.ToString() + "消息：" + ex.Message + "]";
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
            string err = "";
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

        public static double Average(IEnumerable<double> list)
        {
            return list.Average();
        }

        public static double StandardDeviation(IEnumerable<double> list, double up, double low)
        {
            var average = list.Average();
            var count = list.Count();
            double t = Math.Abs(up - low);
            double u = (up + low) / 2;
            double sum = list.Sum(a => Math.Pow(a - average, 2));
            double standardDeviation = Math.Sqrt(sum / count);
            double cpk = 0;
            cpk = t / (6 * standardDeviation) * (1 - Math.Abs((average - u) * 2 / t));
            return cpk;
        }

        #region 递归

        private static int _counnLogNum = 0;

        public static void CountLog(DirectoryInfo dir) //搜索文件夹中的文件
        {
            _counnLogNum = _counnLogNum + dir.GetFiles().Count();
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                CountLog(d);
            }
        }

        #endregion


    }
}