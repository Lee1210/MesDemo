using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using menjin;

using Mes.Core;
using Mes.Core.Data;
using Mes.Demo.Models.Hr;
using Mes.Utility.Extensions;


namespace CpkDemo.Menjin
{
    public class EntranceSolution : IDependency
    {
        public IRepository<SwipeCard, int> SwipeCardRepository { get; set; }

        public IRepository<TemporaryCard, int> TemporaryCardRepository { get; set; }

        public IRepository<IgnoreCard, int> IgnoreCardRepository { get; set; }
        public void EntranceTest()
        {
            Entrance entrance = new Entrance("172.16.202.202", 37526);
            //19565344
            // var cards = new long[] { 12844864, 16317849, 16257097, 15150896 };
            DateTime dt = DateTime.Now;
            entrance.SetTime(dt);
            List<RecordModel> temporaryCardModel = TemporaryCardRepository.Entities.ToList()
                .Select(a => new RecordModel
                {
                    Card = a.Card.PadLeft(10, '0'),
                    EmpNo = a.EmpNo,
                    EmpName = a.EmpName
                }).ToList();
            List<RecordModel> recordModels = HrUserRespority.Cards().ToList();
            recordModels = recordModels.Concat(temporaryCardModel).ToList();

            var models = entrance.Records().ToList();
            foreach (var model in models)
            {
                model.DoorIo = DoorInResoler(model.BarCode.Substring(2, 4) + model.BarCode.Substring(17, 1));
            }
            var result = from m in models
                         join r in recordModels
                         on m.Card equals r.Card into os
                         from r2 in os.DefaultIfEmpty(
                             new RecordModel { Card = null, EmpName = null, EmpNo = null }
                             )
                         select new SwipeCard
                         {
                             Card = m.Card,
                             EmpNo = r2.EmpNo,
                             EmpName = r2.EmpName,
                             BarCode = m.BarCode,
                             DoorIo = m.DoorIo,
                             SwipdeDate = m.SwipdeDate,
                             SwipeTime = m.SwipeTime
                         };
            var swipeCards = result.DistinctBy(b => new { b.Card, b.EmpNo, b.EmpName, b.BarCode, b.DoorIo, b.SwipdeDate, b.SwipeTime })
                .OrderBy(m => m.SwipdeDate).ToList();

            List<string> ignoreEmpNos = IgnoreCardRepository.Entities.Select(m => m.EmpNo).ToList();
            List<SwipeCard> ignoreCards = new List<SwipeCard>();
            foreach (var swipeCard in swipeCards)
            {
                if (!ignoreEmpNos.Contains(swipeCard.EmpNo))
                    ignoreCards.Add(swipeCard);
            }
            // _program.SwipeCardRepository.BulkInsertAll(ignoreCards);

            long[] cards = recordModels.Select(r => r.Card.CastTo<long>()).ToArray();
            var doors = new Int64[] { 1, 2 };
            entrance.PrivalegeAddRange(cards, doors);
            entrance.RecordClear();
        }

        public void EntranceExcute()
        {
            Entrance entrance37477 = new Entrance("172.16.192.241", 37477);
            Entrance entrance37647 = new Entrance("172.16.192.242", 37647);


            List<RecordModel> temporaryCardModel = TemporaryCardRepository.Entities.ToList()
                .Select(a => new RecordModel
                {
                    Card = a.Card.PadLeft(10, '0'),
                    EmpNo = a.EmpNo,
                    EmpName = a.EmpName
                }).ToList();
            List<RecordModel> recordModels = HrUserRespority.Cards().ToList();
            recordModels = recordModels.Concat(temporaryCardModel).ToList();

            var models = entrance37477.Records().Concat(entrance37647.Records()).ToList();
            foreach (var model in models)
            {
                model.DoorIo = DoorInResoler(model.BarCode.Substring(2, 4) + model.BarCode.Substring(17, 1));
            }
            var result = from m in models
                         join r in recordModels
                         on m.Card equals r.Card into os
                         from r2 in os.DefaultIfEmpty(
                             new RecordModel { Card = null, EmpName = null, EmpNo = null }
                             )
                         select new SwipeCard
                         {
                             Card = m.Card,
                             EmpNo = r2.EmpNo,
                             EmpName = r2.EmpName,
                             BarCode = m.BarCode,
                             DoorIo = m.DoorIo,
                             SwipdeDate = m.SwipdeDate,
                             SwipeTime = m.SwipeTime
                         };
            var swipeCards = result.DistinctBy(b => new { b.Card, b.EmpNo, b.EmpName, b.BarCode, b.DoorIo, b.SwipdeDate, b.SwipeTime })
                .OrderBy(m => m.SwipdeDate).ToList();

            List<string> ignoreEmpNos = IgnoreCardRepository.Entities.Select(m => m.EmpNo).ToList();
            List<SwipeCard> ignoreCards = new List<SwipeCard>();
            foreach (var swipeCard in swipeCards)
            {
                if (!ignoreEmpNos.Contains(swipeCard.EmpNo))
                    ignoreCards.Add(swipeCard);
            }

            //保存刷卡数据到数据库
            SwipeCardRepository.BulkInsertAll(ignoreCards);
            //重置时间
            DateTime dt = DateTime.Now;
            entrance37477.SetTime(dt);
            entrance37647.SetTime(dt);
            //插入权限
            long[] cards = recordModels.Select(r => r.Card.CastTo<long>()).ToArray();
            var doors = new Int64[] { 1, 2 };
            entrance37477.PrivalegeAddRange(cards, doors);
            entrance37647.PrivalegeAddRange(cards, doors);
            //清空记录
            entrance37477.RecordClear();
            entrance37647.RecordClear();
        }

        public static void Test()
        {
            var ping = new Ping();
            var pingReply = ping.Send("172.16.202.202");
            // if (pingReply.Status == IPStatus.Success)
            Entrance entrance = new Entrance("172.16.202.202", 37526);
            //var cards = new long[] { 12844864, 16317849, 16257097, 15150896 };
            var cards = new long[] { 16257097, 15150896 };

            var doors = new Int64[] { 1, 2 };
            // entrance.PrivalegesClear();
            var a = entrance.PrivilegeCount();
            entrance.PrivalegeEndRemoveRange(cards, doors);
            // entrance.PrivalegeEndAddRange(cards, doors);
            var b = entrance.PrivilegeCount();
            entrance.RecordClear();
        }

        private static string DoorInResoler(string test)
        {
            if (test.Equals("0F932") || test.Equals("65920"))
                return "出";
            else
                return "进";
        }

        public static void Wrtest()
        {
            using (var stream = new System.IO.StreamWriter("E:\\log.txt", true))
            {
                stream.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            }
        }
    }
}
