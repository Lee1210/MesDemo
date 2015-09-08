// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-08-26 10:15</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

using Autofac;

using CpkDemo.Menjin;

using FileClizz;

using menjin;

using Mes.Core;
using Mes.Core.Caching;
using Mes.Core.Data;
using Mes.Demo.Models.Hr;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Develop;
using Mes.Utility.Extensions;




namespace CpkDemo
{

    internal class Program : IDependency
    {
        private static Program _program;
        // ReSharper disable once NotAccessedField.Local
        private static ICache _cache;
        public LogResolver LogResolver { get; set; }
        public IRepository<SwipeCard, int> SwipeCardRepository { get; set; }
        public IRepository<TemporaryCard, int> TemporaryCardRepository { get; set; }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            Startup.Start();
            _program = Startup.Container.Resolve<Program>();
            _cache = CacheManager.GetCacher(typeof(Program));
          
            //EntranceTest();
             EntranceExcute();
            //Console.ReadLine();

        }

        private static void LogExcute()
        {
            _program.LogResolver.CpkExcute(_program.LogResolver.CpkLogFromPath,
                _program.LogResolver.CpkLogToPath,
                _program.LogResolver.CpkTransfaseDataTable);
            _program.LogResolver.TestLogExcute(_program.LogResolver.TestLogFromPath,
                _program.LogResolver.TestLogToPath,
                _program.LogResolver.TestLogTransfaseDataTable);
        }

        public static void EntranceTest()
        {
            Entrance entrance = new Entrance("172.16.202.202", 37526);
            //19565344
            // var cards = new long[] { 12844864, 16317849, 16257097, 15150896 };

            List<RecordModel> temporaryCardModel = _program.TemporaryCardRepository.Entities.ToList()
                .Select(a=>new RecordModel
            {
                Card = a.Card.PadLeft(10,'0'),
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
            _program.SwipeCardRepository.BulkInsertAll(swipeCards);

            //long[] cards = recordModels.Select(r => r.Card.CastTo<long>()).ToArray();
            //var doors = new Int64[] { 1, 2 };
            //entrance.InsertPrivaleges(cards, doors);
            //entrance.ClearRecords();
        }

        public static void EntranceExcute()
        {
            Entrance entrance37477 = new Entrance("172.16.192.241", 37477);
            Entrance entrance37647 = new Entrance("172.16.192.242", 37647);

            List<RecordModel> temporaryCardModel = _program.TemporaryCardRepository.Entities.ToList()
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
            _program.SwipeCardRepository.BulkInsertAll(swipeCards);

            long[] cards = recordModels.Select(r => r.Card.CastTo<long>()).ToArray();
            var doors = new Int64[] { 1, 2 };
            entrance37477.InsertPrivaleges(cards, doors);
            entrance37647.InsertPrivaleges(cards, doors);
            entrance37477.ClearRecords();
            entrance37647.ClearRecords();
        }

        public static string DoorInResoler(string test)
        {
            if (test.Equals("0F932") || test.Equals("65920"))
                return "出";
            else
                return "进";
        }

        public static void TemporaryCardInsert()
        {
            List<long> cards = new List<long>
            {
                6982224,
5295584,
4644896,
5657309,
5623536,
14915629,
3207881,
8174674,
4513344,
13722747,
10048269,
8748179,
5403635,
4426947,
14632675,
2810451,
13545696,
13564640,
12645184,
14310080,
12380579,
3215504,
3712048,
3440976,
2526672,
8657443,
3722144,
5986083,
2842445,
6140634,
7624206,
13417872,
13157307,
5859923,
5599760,
8133680,
12584688,
4765187,
8004528,
4778512,
6473392,
166291,
5369219,
2374464,
4724928,
7672653,
8410851,
4685203,
2089517,
4976016
            };
            List<TemporaryCard> temporaryCards=new List<TemporaryCard>();
            foreach (var card in cards)
            {
                temporaryCards.Add(new TemporaryCard
                {
                    Card = card.ToString(),EmpName = "临时卡",EmpNo = "临时卡"
                });
            }
            _program.TemporaryCardRepository.BulkInsertAll(temporaryCards);
        }
    }
}