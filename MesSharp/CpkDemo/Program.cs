using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBUtility;

using Mes.Utility.Develop;


namespace CpkDemo
{
    class Program
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

        static string ZipFileName { get; set; }
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            Excute(CpkLogFromPath, CpkLogToPath, CpkTransfaseDataTable, "CPK");
            Console.ReadLine();
        }

        static void Excute(string logFromPath, string logToPath, Action<FileInfo> transfaseDataTable, string operTable)
        {
            double move = 0, read = 0, zip = 0, del = 0;
            var total = CodeTimer.TimeLong("总共时间", 1, () =>
            {
                DirectoryInfo myFolder1 = new DirectoryInfo(logFromPath);
                CountLog(myFolder1);
                if (_counnLogNum <= 0)
                {
                    return;
                }
                ZipFileName = DateTime.Now.ToString("yyyyMMddHHmm");

                string logToAbsPath = logToPath + "\\" + ZipFileName;

                try
                {
                    move=CodeTimer.TimeLong("移动数据", 1, () => FileClizz.FileUtil.MoveFolder(logFromPath, logToAbsPath));

                    DirectoryInfo myFolder = new DirectoryInfo(logToAbsPath);
                    read=CodeTimer.TimeLong("解析数据", 1, () => GetAll(myFolder, transfaseDataTable));

                    zip=CodeTimer.TimeLong("压缩数据", 1, () =>
                    {
                        if (FileClizz.FileUtil.Zip(logToAbsPath, logToAbsPath + ".zip", ""))
                            del=CodeTimer.TimeLong("删除数据", 1, () => FileClizz.FileUtil.DeleteDir(logToAbsPath));
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
        static void AddOperLog(int log, double move, double read, double zip, double del, double total, string ActionName)
        {

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = SqlHelper.ConnectionStringLocalTransaction;
            conn.Open();
            //SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
            string sql = string.Format("insert into OperationLogs(Num,MoveTimes,ReadTimes,ZipTimes,DeleteTimes,TotalTimes,IsDeleted,CreatedTime,OperationType)values({0},{1},{2},{3},{4},{5},0,'{6}','{7}')", log, move, read, zip, del, total,DateTime.Now,ActionName);
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
        public static void GetAll(DirectoryInfo dir, Action<FileInfo> act)//搜索文件夹中的文件
        {
            foreach (var file in dir.GetFiles())
            {
                //readLogNum++;
                try
                {
                    act(file);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAll(d, act);
            }
        }

        #endregion


        #region 递归
        static int _counnLogNum = 0;
        public static void CountLog(DirectoryInfo dir)//搜索文件夹中的文件
        {
            _counnLogNum = _counnLogNum + dir.GetFiles().Count();
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                CountLog(d);
            }
        }
        #endregion


        #region  递归
        public ArrayList Al = new ArrayList();
        //我把ArrayList当成动态数组用，非常好用
        public void GetAllDirList(string strBaseDir)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(strBaseDir);
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            foreach (DirectoryInfo info in directoryInfos)
            {
                Al.Add(info.FullName);
                //diA[i].FullName是某个子目录的绝对地址，把它记录在ArrayList中
                GetAllDirList(info.FullName);
                //注意：递归了。逻辑思维正常的人应该能反应过来
            }
        }
        #endregion


        static StringCollection ReadFileIntoStringCollection(string chosenFile)
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

        static readonly ArrayList Excelhzs = new ArrayList() {300,315,335,355,375,400,425,450,475,500,530,560,600,630,
            670,710,750,800,900,1000,1060,1120,1180,1250,1320,1400,1500,1600,1700,1800,1900,2000,2120,
            2240,2360,2500,2650,2800,3000,3150,3550,3750,4000,4250,4500,4750,5000,5300,5600,6000,6300,
            6700,7100,7500,8000};//
        static void CpkTransfaseDataTable(FileInfo chosenFile)
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
                DateTime currentTime = System.DateTime.Now;
                string dateStr = currentTime.ToString("yyyyMMdd");
                string timeStr = currentTime.ToString("HHmmss");
                testDate = Convert.ToInt32(dateStr);
                testTime = Convert.ToInt32(timeStr);
            }

            StringCollection linesCollection = ReadFileIntoStringCollection(chosenFile.FullName);
            string[] linesArray = new string[linesCollection.Count];
            linesCollection.CopyTo(linesArray, 0);

            string platForm = linesArray[0].Split(':', ';')[1];// 平台
            string projectName = linesArray[1].Split(':', ';')[1]; // 项目名
            string stationName = linesArray[2].Split(':', ';')[1]; // 站点
            string ip = linesArray[3].Split(':', ';')[1]; // IP
            //string sn = linesArray[4].Split(':', ';')[1];//sn
            //string Start_time = linesArray[5].Substring(11).TrimEnd(';');//测试时间 
            //string TestTime = linesArray[6].Split(':', ';')[1];//

            string result = linesArray[7].Split(':', ';')[1];//
            DateTime readLogDate = DateTime.Now;
            //ErrItem NULL;
            //ErrMsg NULL;
            //Test_item,measure_data,Lower,Upper;
            DataTable dt = new DataTable();
            {
                dt.Columns.Add("PlatForm", typeof(string));
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("Opcode", typeof(string));
                dt.Columns.Add("Ip", typeof(string));
                dt.Columns.Add("Sn", typeof(string));
                dt.Columns.Add("TestDate", typeof(int));
                dt.Columns.Add("TestTime", typeof(string));
                dt.Columns.Add("Result", typeof(int));
                dt.Columns.Add("Wgsn", typeof(string));
                dt.Columns.Add("Tch", typeof(string));
                dt.Columns.Add("Pcl", typeof(string));
                dt.Columns.Add("TestItem", typeof(string));
                dt.Columns.Add("TestVal", typeof(string));
                dt.Columns.Add("MinVal", typeof(double));
                dt.Columns.Add("MaxVal", typeof(double));
                //     dt.Columns.Add("id", typeof(string));
                dt.Columns.Add("ReadLogDate", typeof(DateTime));
              //  dt.Columns.Add("readLogTime", typeof(int));
                dt.Columns.Add("LogFileName", typeof(string));
                dt.Columns.Add("IsDeleted", typeof(bool));
                dt.Columns.Add("CreatedTime", typeof(DateTime));

            }

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
                    dr[7] = result.ToUpper().Equals("PASS")?1:0;
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
                    dt.Rows.Add(dr);
                }
                catch (Exception)
                {
                    string log = "log格式不正确[" + line + "]、log[" + logFile + "]";
                    WriteSysLog(log);
                }

            }


            SqlConnection conn = new SqlConnection();

            string tt = SqlHelper.ConnectionStringLocalTransaction;

            conn.ConnectionString = SqlHelper.ConnectionStringLocalTransaction;
            conn.Open();

            SqlTransaction sqlbulkTransaction = conn.BeginTransaction();

            //请在插入数据的同时检查约束，如果发生错误调用sqlbulkTransaction事务
            SqlBulkCopy copy = new SqlBulkCopy(conn, SqlBulkCopyOptions.CheckConstraints, sqlbulkTransaction);
            copy.BatchSize = 5000;//每次传输的行数
            copy.DestinationTableName = "Cpks";  //定义目标表    
            foreach (DataColumn dc in dt.Columns)
            {
                copy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
            }

            try
            {
                copy.WriteToServer(dt);//将数据写入Sql Server数据表   
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
            DateTime logTime = System.DateTime.Now;
            string logName = logTime.ToString("yyyy-MM-dd-HH");
            string writeTime = logTime.ToString("yyyy-MM-dd HH:mm:ss");
            string path = SysLogPath + "\\" + logName;
            log = writeTime + "\t" + log;
            if (!FileClizz.FileUtil.writeLog(path, log, out err))
            {
                Console.WriteLine(err);
            }

        }

    }
}
