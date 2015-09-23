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

using Mes.Core;
using Mes.Core.Caching;
using Mes.Core.Data;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Develop;
using Mes.Utility.Extensions;


namespace CpkDemo
{
    public class LogResolver : IDependency
    {
        public IRepository<Cpk, int> CpkRepository { get; set; }
        public IRepository<Tlog, int> TlogRepository { get; set; }
        public IRepository<OperationLog,int> OperationLogRepository { get; set; }
       
        public string CpkLogFromPath = ConfigurationManager.AppSettings["Cpk_LOG_FROM_PATH"];
        public string CpkLogToPath = ConfigurationManager.AppSettings["Cpk_LOG_TO_PATH"];
        public string CpkErrorPath = ConfigurationManager.AppSettings["Cpk_ERROR_PATH"];
        public string TestLogFromPath = ConfigurationManager.AppSettings["Test_LOG_FROM_PATH"];
        public string TestLogToPath = ConfigurationManager.AppSettings["Test_LOG_TO_PATH"];
        public string TestErrorPath = ConfigurationManager.AppSettings["Test_ERROR_PATH"];


        public static string SysLogPath = ConfigurationManager.AppSettings["SYS_LOG_PATH"];
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

      
        public  void CpkExcute(string logFromPath,
           string logToPath,
           Action<FileInfo, List<Cpk>, string> transfaseDataTable)
        {
            double move = 0, read = 0, zip = 0, del = 0;
            var fileCount = 0;
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
                        List<Cpk> cpks=new List<Cpk>(); 
                        read = CodeTimer.TimeLong("解析数据", 1, () =>
                        {
                            TraverseFolder(myFolder, transfaseDataTable, cpks, zipFileName);
                            CpkRepository.BulkInsertAll(cpks);
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
            OperationLogRepository.Insert(new OperationLog()
            {
                Num = fileCount, MoveTimes = move, ReadTimes = read,ZipTimes = zip,
                DeleteTimes = del,TotalTimes = total,OperationType = "Cpks"
            });
        }

        public void TestLogExcute(string logFromPath,
            string logToPath,
            Action<FileInfo, List<Tlog>, string> transfaseDataTable)
        {
            double move = 0, read = 0, zip = 0, del = 0;
            var fileCount = 0;
          
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
                        List<Tlog> tlogs=new List<Tlog>();
                        read = CodeTimer.TimeLong("解析数据", 1, () =>
                        {
                            TraverseFolder2(myFolder, transfaseDataTable, tlogs, zipFileName);
                            TlogRepository.BulkInsertAll(tlogs);
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
            OperationLogRepository.Insert(new OperationLog()
            {
                Num = fileCount,
                MoveTimes = move,
                ReadTimes = read,
                ZipTimes = zip,
                DeleteTimes = del,
                TotalTimes = total,
                OperationType = "TestLog"
            });

        }

        public void TraverseFolder(DirectoryInfo dir, Action<FileInfo, List<Cpk>, string> act, List<Cpk> cpks, string zipName) //搜索文件夹中的文件
        {
            foreach (var file in dir.GetFiles())
            {
                try
                {

                    act(file, cpks, zipName);
                    if (cpks.Count() > 4500)
                    {
                        CpkRepository.BulkInsertAll(cpks);
                        cpks.Clear();
                    }
                }
                catch (Exception e)
                {
                    // 移动文件到Error,记录Log
                    string strToPath = CpkErrorPath + "\\" + zipName;
                    if (!Directory.Exists(strToPath))
                    {
                        Directory.CreateDirectory(strToPath);
                    }
                    File.Move(file.FullName, strToPath + "\\"+file.Name);
                    WriteSysLog(e.Message);
                }
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                TraverseFolder(d, act, cpks, zipName);
            }
        }


        public void TraverseFolder2(DirectoryInfo dir, Action<FileInfo, List<Tlog>, string> act, List<Tlog> tlogs, string zipName) //搜索文件夹中的文件
        {
            foreach (var file in dir.GetFiles())
            {
                try
                {

                    act(file, tlogs, zipName);
                    if (tlogs.Count() > 4500)
                    {
                        TlogRepository.BulkInsertAll(tlogs);
                        tlogs.Clear();
                    }
                }
                catch (Exception e)
                {
                    // 移动文件到Error,记录Log
                    string strToPath = TestErrorPath + "\\" + zipName;
                    if (!Directory.Exists(strToPath))
                    {
                        Directory.CreateDirectory(strToPath);
                    }
                    File.Move(file.FullName, strToPath + "\\" + file.Name);
                    WriteSysLog(e.Message);
                }
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                TraverseFolder2(d, act, tlogs, zipName);
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



        public void CpkTransfaseDataTable(FileInfo chosenFile, List<Cpk> cpks, string zipName)
        {
            List<Cpk> collection=new List<Cpk>();
            string logFile, platForm, projectName, stationName, ip, result,sn,time;
            string[] timesArr, linesArray;
            int testDate;
            int testTime;
            DateTime readLogDate;
            StringCollection linesCollection;
            try
            {
                 logFile = chosenFile.Name;
                 timesArr = chosenFile.Name.Split('_');
                 linesCollection = ReadFileIntoStringCollection(chosenFile.FullName);
                 linesArray = new string[linesCollection.Count];
                 linesCollection.CopyTo(linesArray, 0);
                 sn = timesArr[0];
                 time = timesArr[1];
                 testDate = Convert.ToInt32(time.Substring(0, 8));
                 testTime = Convert.ToInt32(time.Substring(8));
                 platForm = linesArray[0].Split(':', ';')[1]; // 平台
                 projectName = linesArray[1].Split(':', ';')[1]; // 项目名
                 stationName = linesArray[2].Split(':', ';')[1]; // 站点
                 ip = linesArray[3].Split(':', ';')[1]; // IP
                 result = linesArray[7].Split(':', ';')[1]; //
                 readLogDate = DateTime.Now;
            }
            catch (Exception)
            {
                
                throw new Exception("文件名格式不对："+ chosenFile.Name);
            }
            
            

            foreach (var line in linesArray.Skip(11))
            {
                try
                {
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

                    platForm = platForm.ToUpper().Replace(" ", "");
                    projectName = projectName.ToUpper().Replace(" ", "");
                    stationName = stationName.ToUpper().Replace(" ", "");
                    ip = ip.Replace(" ", "");
                    sn = sn.ToUpper().Replace(" ", "");
                    TestReslut testReslut = result.ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                    wgsn = wgsn.ToUpper().Replace(" ", "");
                    tch = tch.ToUpper().Replace(" ", "");
                    pcl = pcl.ToUpper().Replace(" ", "");
                    testItem = testItem.ToUpper().Replace(" ", "");
                    logFile = logFile.Replace(" ", "");
                    zipName = zipName.Replace(" ", "");
                    
                    collection.Add(new Cpk()
                    {
                        PlatForm = platForm,ProjectName = projectName,Opcode = stationName,Ip = ip,
                        Sn = sn,TestDate = testDate,TestTime = testTime.ToString(),Result = testReslut,Wgsn = wgsn,
                        Tch = tch,Pcl = pcl,TestItem = testItem,TestVal = testVal,MinVal = minVal,
                        MaxVal = maxVal,ReadLogDate = readLogDate,LogFileName = logFile,ZipFileName = zipName

                    });
                }
                catch (Exception)
                {
                    var log = "log格式不正确[" + line + "]、log[" + logFile + "]";
                    throw new Exception(log);
                }
                
            }
            cpks.AddRange(collection);

        }

        public void TestLogTransfaseDataTable(FileInfo chosenFile, List<Tlog> tlogs, string zipName)
        {
            string fileNameArr = chosenFile.Name.Split('_').First();
            List<Tlog> tlogsTest=new List<Tlog>();
            LogFileType test;
            try
            {
                if (chosenFile.Name.ToUpper().EndsWith("HTML"))
                test = LogFileType.HTML;
                else
                test = fileNameArr.ToUpper().CastTo<LogFileType>();
            }
            catch (Exception)
            {
                test = LogFileType.OTHER;
            }
            try
            {
                StringCollection linesCollection = ReadFileIntoStringCollection(chosenFile.FullName);
                string[] linesArray = new string[linesCollection.Count];
                linesCollection.CopyTo(linesArray, 0);
                int testDate = DateTime.Now.ToString("yyyyMMdd").CastTo<int>();
                string sn;
                switch (test)
                {
                    case LogFileType.MAIN:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[2];
                            TestReslut reslut= line.Split('\t')[5].ToUpper().Equals("PASS")?TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.MAIN,
                                TestReslut = reslut
                                
                            });
                        }
                        break;

                    case LogFileType.RECEIVERAMPL:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[2];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.RECEIVERAMPL,
                                TestReslut = reslut
                            });
                        }
                        break;

                    case LogFileType.SEC:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[2];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.SEC,
                                TestReslut = reslut
                            });
                        }
                        break;

                    case LogFileType.SPEAKERAMPL:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[2];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.SPEAKERAMPL,
                                TestReslut = reslut
                            });
                        }
                        break;

                    case LogFileType.副麦:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[4];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.副麦,
                                TestReslut = reslut
                            });
                        }
                        break;

                    case LogFileType.听筒:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[4];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.听筒,
                                TestReslut = reslut
                            });
                        }
                        break;

                    case LogFileType.扬声器:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[4];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.扬声器,
                                TestReslut = reslut
                            });
                        }
                        break;

                    case LogFileType.扬声器右:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[4];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.扬声器右,
                                TestReslut = reslut
                            });
                        }
                        break;
                    case LogFileType.扬声器左:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[4];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.扬声器左,
                                TestReslut = reslut
                            });
                        }
                        break;
                    case LogFileType.主麦:
                        foreach (var line in linesArray.Skip(3))
                        {
                            sn = line.Split('\t')[4];
                            TestReslut reslut = line.Split('\t')[5].ToUpper().Equals("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.主麦,
                                TestReslut = reslut
                            });
                        }
                        break;
                    case LogFileType.HTML:
                        sn = fileNameArr;
                        tlogsTest.Add(new Tlog()
                        {
                            Sn = sn,
                            TestDate = testDate,
                            ZipFileName = zipName,
                            LogFileName = chosenFile.Name,
                            Path = chosenFile.FullName,
                            LogFileType = LogFileType.HTML,
                            TestReslut = TestReslut.Pass
                        });
                        break;
                    case LogFileType.OTHER:
                        {
                            sn = fileNameArr;
                            TestReslut reslut = chosenFile.Name.ToUpper().Contains("PASS") ? TestReslut.Pass : TestReslut.Fail;
                            tlogsTest.Add(new Tlog()
                            {
                                Sn = sn,
                                TestDate = testDate,
                                ZipFileName = zipName,
                                LogFileName = chosenFile.Name,
                                Path = chosenFile.FullName,
                                LogFileType = LogFileType.OTHER,
                                TestReslut = reslut
                            });
                        }
                        
                        break;
                }
            }
            catch (Exception)
            {
                throw new Exception("解析文件错误："+chosenFile.Name);
            }
            
            tlogs.AddRange(tlogsTest);
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
