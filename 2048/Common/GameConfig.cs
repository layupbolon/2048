using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2048
{
    public static class GameConfig
    {
        /// <summary>
        /// 用于随机生成单元
        /// </summary>
        public static int[] RandomArray = { 2, 4 };

        /// <summary>
        /// 用来存储游戏中每个单元的内容
        /// </summary>
        public static List<Cell> cells = new List<Cell>();

        /// <summary>
        /// 游戏界面框的颜色画笔
        /// </summary>
        public static Pen pen = new Pen(Color.Black);

        /// <summary>
        /// 游戏界面边框
        /// </summary>
        public const int GAMEMAP_WIDTH = 240;

        /// <summary>
        /// 游戏界面矩阵的纬度
        /// </summary>
        public const int GAMEMAP_CELLNUMBER = 4;

        /// <summary>
        /// 游戏界面单元格边宽
        /// </summary>
        public const int GAMEMAP_CELLWIDTH = GAMEMAP_WIDTH / GAMEMAP_CELLNUMBER;

        /// <summary>
        /// 单元格实际边宽
        /// </summary>
        public const int CELLMAP_WIDTH = 59;

        /// <summary>
        /// 初始化时创建的单元格数
        /// </summary>
        public const int INITIALIZE_CELLAMOUNT = 2;

        /// <summary>
        /// 游戏得分
        /// </summary>
        public static int SCORE = 0;

    }
}
