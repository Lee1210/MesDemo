using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mes.Demo.Models.Hr;
using Mes.Utility.Extensions;

using WComm_UDP;

namespace menjin
{
    public class Entrance
    {
        public readonly string IpAddr;
        public readonly long ControllerSn;
        /// <summary>
        /// 命令字符串
        /// </summary>
        public string StrCmd { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public string InitializestrFrame { get; set; }
        public WComm_Operate Wudp { get; set; }
        public Entrance(string ipAddr, long controllerSn)
        {
            string strCmd;
            Wudp = new WComm_Operate();
            IpAddr = ipAddr;
            ControllerSn = controllerSn;
            //初始化
            strCmd = InitialComm();
        }

        /// <summary>
        /// 对控制器信息做更改时要重新初始化
        /// </summary>
        /// <returns>发送的指令</returns>
        public string InitialComm()
        {
            string strCmd;
            strCmd = Wudp.CreateBstrCommand(ControllerSn, "8110" + Wudp.NumToStrHex(0, 3)); //生成指令帧 wudp.NumToStrHex(0,3) 表示第0个记录, 也就最新记录
            InitializestrFrame = Wudp.udp_comm(strCmd, IpAddr, 60000);                             // '发送指令, 并获取返回信息
            return strCmd;
        }

        public long RecordCount()
        {
            return Wudp.GetCardRecordCountFromRunInfo(InitializestrFrame);
        }

        /// <summary>
        /// 刷卡记录
        /// </summary>
        public List<SwipeCard> Records()
        {
            List<SwipeCard> modes = new List<SwipeCard>();
            string strCmd, strFrame;
            long count = RecordCount();
            long cardId = 0;
            long status = 0;
            for (long a = 1; a < count + 1; a++)
            {
                strCmd = Wudp.CreateBstrCommand(ControllerSn, "8D10" + Wudp.NumToStrHex(a, 4));//      '生成指令帧
                strFrame = Wudp.udp_comm(strCmd, IpAddr, 60000);
                var swipeDate = Wudp.GetSwipeDateFromRunInfo(strFrame, ref cardId, ref status);
                var sn = Wudp.GetSNFromRunInfo(strFrame);
                modes.Add(new SwipeCard()
                {
                    Card = Card16ToCard(cardId),
                    SwipeTime = Convert.ToDateTime(swipeDate),
                    SwipdeDate = Convert.ToDateTime(swipeDate).ToString("yyyyMMdd").CastTo<int>(),
                    BarCode = strFrame
                });

            }
            return modes;
        }
        /// <summary>
        /// 删除单条刷卡记录
        /// </summary>
        /// <param name="index">索引</param>
        public void DeleteRecord(long index)
        {
            var strCmd = Wudp.CreateBstrCommand(ControllerSn, "8E10" + Wudp.NumToStrHex(index, 4));
            var strFrame = Wudp.udp_comm(strCmd, IpAddr, 60000);
        }

        /// <summary>
        /// 清空刷卡记录
        /// </summary>
        public void ClearRecords()
        {
            long count = RecordCount();
            for (int i = 1; i < count + 1; i++)
            {
                DeleteRecord(i);
            }
            var strCmd = Wudp.CreateBstrCommand(ControllerSn, "8110" + Wudp.NumToStrHex(0, 3));
            var strFrame = Wudp.udp_comm(strCmd, IpAddr, 60000);
        }
        /// <summary>
        /// 权限总数
        /// </summary>
        /// <returns></returns>

        public long PrivilegeCount()
        {
            var strCmd = Wudp.CreateBstrCommand(ControllerSn, "8110" + Wudp.NumToStrHex(0, 3));
            var strFrame = Wudp.udp_comm(strCmd, IpAddr, 60000);
            return Wudp.GetPrivilegeNumFromRunInfo(strFrame);
        }
        /// <summary>
        /// 查询单条权限
        /// </summary>
        /// <param name="index"></param>

        public void Privalege(long index)
        {
            var strCmd = Wudp.CreateBstrCommand(ControllerSn, "9510" + Wudp.NumToStrHex(index, 3));
            var strFrame = Wudp.udp_comm(strCmd, IpAddr, 60000);
            Console.WriteLine("query privage:" + strFrame);
        }
        /// <summary>
        /// 清空权限
        /// </summary>

        public void DeletePrivaleges()
        {
            var strCmd = Wudp.CreateBstrCommand(ControllerSn, "9310");
            var strFrame = Wudp.udp_comm(strCmd, IpAddr, 60000);
        }

        /// <summary>
        /// 插入单条权限
        /// </summary>
        /// <param name="card"></param>
        /// <param name="door"></param>
        /// <param name="index"></param>

        public void InsertPrivalege(long card, long door, long index)
        {
            var privilege = CardtoHey(card);
            privilege = privilege + Wudp.NumToStrHex(door, 1);     //  '门号
            privilege = privilege + Wudp.MSDateYmdToWCDateYmd("2007-8-14"); //    '有效起始日期
            privilege = privilege + Wudp.MSDateYmdToWCDateYmd("2020-12-31");//    '有效截止日期
            privilege = privilege + Wudp.NumToStrHex(1, 1);                 //    '时段索引号
            privilege = privilege + Wudp.NumToStrHex(123456, 3);             //   '用户密码
            privilege = privilege + Wudp.NumToStrHex(0, 4);                 //    '备用4字节(用0填充)
            if (privilege.Length != 32)
                Console.WriteLine(@"privilege leng not equal 32");
            else
            {
                var strCmd = Wudp.CreateBstrCommand(ControllerSn, "9B10" + Wudp.NumToStrHex(index, 2) + privilege);

                Wudp.udp_comm(strCmd, IpAddr, 60000);
            }
        }
        /// <summary>
        /// 插入权限
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="doors"></param>

        public void InsertPrivaleges(long[] cards, long[] doors)
        {
            //先清空权限
            DeletePrivaleges();
            //权限索引从1开始插入  card door排序
            int index = 1;
            cards = cards.OrderBy(c => c).ToArray();
            doors = doors.OrderBy(d => d).ToArray();
            foreach (var door in doors)
            {
                foreach (var card in cards)
                {
                    InsertPrivalege(card, door, index);
                    index = index + 1;
                }
            }
            InitialComm();
        }

        /// <summary>
        /// 设置延迟开门时间
        /// </summary>
        /// <param name="millisecond">单位为0.1秒</param>
        /// <param name="door"></param>
        /// <returns></returns>
        public string SetDelayTime(int millisecond, int door)
        {
            string privilege = "";
            privilege = privilege + Wudp.NumToStrHex(door, 1);     //  '门号
            privilege = privilege + Wudp.NumToStrHex(3, 1);     //  '控制方式
            privilege = privilege + Wudp.NumToStrHex(millisecond, 2);     //  'yanchi
            var strCmd = Wudp.CreateBstrCommand(ControllerSn, "8F10" + privilege);
            return Wudp.udp_comm(strCmd, IpAddr, 60000);

        }

        public string Card16ToCard(long cardId)
        {
            string test = cardId.ToString().PadLeft(8, '0');
            test = Convert.ToString(Convert.ToInt64(test.Substring(0, 3)), 16) + Convert.ToString(Convert.ToInt64(test.Substring(3)), 16);
            return Convert.ToInt64(test, 16).ToString().PadLeft(10, '0');
        }

        static string CardtoHey(long cardId)
        {
            string test = Convert.ToString(cardId, 16).PadLeft(6, '0');
            return test.Substring(4, 2) + test.Substring(2, 2) + test.Substring(0, 2);
        }


    }
}
