using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

using menjin;

using Mes.Utility.DbHelper;
using Mes.Utility.Extensions;


namespace CpkDemo.Menjin
{
    public class HrUserRespority
    {
        private const string Sql = "select distinct PID,EmpNo,EmpName  from Employee where Incumbency=1 and pid is not null";
        private const string Sql2 = "INSERT INTO Lg_swipe (Card,SwipeTime,SwipeTime,InsertTime)  VALUES(@Card,@SwipeTime,@SwipeTime,@InsertTime)";
        private const string Sql3 = "select a.Card,a.SwipeDate,a.SwipeTime,b.empno,b.empname from Lg_swipe a left join   Employee b on a.card=b.pid";
        private static readonly string Connctionstring = ConfigurationManager.ConnectionStrings["HrConnString"].ConnectionString;
        public static IList<RecordModel> Cards()
        {
            List<RecordModel> cardList = new List<RecordModel>();
            using (var sqlreader = SqlHelper.ExecuteReader(Connctionstring, System.Data.CommandType.Text, Sql))
            {
                while (sqlreader.Read())
                {
                    cardList.Add(new RecordModel
                    {
                        Card = sqlreader.GetString(0),
                        EmpNo = sqlreader.GetString(1),
                        EmpName = sqlreader.GetString(2)
                    });
                       
                }
                sqlreader.Close();
            }
            return cardList;
        }


        public static List<RecordModel> Cards2()
        {
            List<RecordModel> cardList = new List<RecordModel>();
            using (var sqlreader = SqlHelper.ExecuteReader(Connctionstring, System.Data.CommandType.Text, Sql3))
            {
                while (sqlreader.Read())
                {
                    cardList.Add(new RecordModel
                    {
                        Card = sqlreader.GetString(0),
                        SwipeDate = sqlreader.GetValue(1).CastTo<int>(),
                        SwipeTime = sqlreader.GetDateTime(2),
                        EmpNo = sqlreader.GetValue(3).CastTo<string>(),
                        EmpName = sqlreader.GetValue(4).CastTo<string>()
                    });

                }
                sqlreader.Close();
            }
            return cardList;
        }
        public static void SaveRecords(List<RecordModel> models)
        {
            foreach (var model in models)
            {
                SqlParameter card = new SqlParameter("@Card", model.Card);
                SqlParameter swipeTime = new SqlParameter("@SwipeTime", model.SwipeTime);
                SqlParameter swipeDate = new SqlParameter("@SwipeTime", Convert.ToInt64(model.SwipeTime.ToString("yyyyMMdd")));
                SqlParameter insertTime = new SqlParameter("@InsertTime", DateTime.Now);
                
                SqlHelper.ExecuteNonQuery(Connctionstring, System.Data.CommandType.Text, Sql2, card, swipeTime, swipeDate, insertTime);
            }

        }
    }
}
