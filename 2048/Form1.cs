using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
            byte bVk,    //虚拟键值
            byte bScan,// 一般为0
            int dwFlags,  //这里是整数类型  0 为按下，2为释放
            int dwExtraInfo  //这里是整数类型 一般情况下设成为 0
        );       

        public Form1()
        {
            InitializeComponent();
            this.Text = "2048      【author : Eric Yin】";
            label3.Text = "V1.11 Beta版";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCells();//初始化单元列表
            CreateRandomCell(true);//随机生成两个图片
            //keybd_event(65, 0, 0, 0);
            //keybd_event(65, 0, 2, 0);
            //PaintGameMap();
        }

        //private void Form1_Paint(object sender, PaintEventArgs e)
        //{
            //Graphics g = e.Graphics;

            //g.DrawRectangle(GameConfig.pen, 0, 0, GameConfig.GAMEMAP_WIDTH, GameConfig.GAMEMAP_WIDTH);
            //for (int i = 1; i < GameConfig.GAMEMAP_CELLNUMBER; i++)
            //{
            //    g.DrawLine(GameConfig.pen, new Point(0, GameConfig.GAMEMAP_CELLWIDTH * i), new Point(GameConfig.GAMEMAP_WIDTH, GameConfig.GAMEMAP_CELLWIDTH * i));
            //    g.DrawLine(GameConfig.pen, new Point(GameConfig.GAMEMAP_CELLWIDTH * i, 0), new Point(GameConfig.GAMEMAP_CELLWIDTH * i, GameConfig.GAMEMAP_WIDTH));
            //}
            
        //}

        //上下左右 WSAD
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Thread.Sleep(10);
            bool IsMove = false;
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    IsMove = MoveAction(Global.Direction.Up);
                    break;
                case Keys.Down:
                case Keys.S:
                    IsMove = MoveAction(Global.Direction.Down);
                    break;
                case Keys.Left:
                case Keys.A:
                    IsMove = MoveAction(Global.Direction.Left);
                    break;
                case Keys.Right:
                case Keys.D:
                    IsMove = MoveAction(Global.Direction.Right);
                    break;
            }
            if (IsMove)
                CreateRandomCell(false);

            if (!IsMove && JudgeListIsFull())
                ShowMessageAndLoadNewGame();
        }

        #region 方法

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="eDirection">方向</param>
        private bool MoveAction(Global.Direction eDirection)
        {
            int IsMove = 0;
            List<int> values;

            switch (eDirection)
            {
                case Global.Direction.Up:
                    for (int m = 0; m < GameConfig.GAMEMAP_CELLNUMBER; m++)
                    {
                        values = (from c in GameConfig.cells where c.Column == m + 1 orderby c.ID select c.Value).ToList();
                        string str = ConvertListToString(values);
                        List<int> result = ArrayHandle(values);
                        string str2 = ConvertListToString(result);

                        int index = 0;
                        foreach (Cell cell in GameConfig.cells.Where(a => a.Column == m + 1).OrderBy(a => a.ID))
                        {
                            cell.Value = result[index];
                            index++;
                        }

                        if (!str.Equals(str2))
                            IsMove++;
                    }
                    break;
                case Global.Direction.Down:
                    for (int m = 0; m < GameConfig.GAMEMAP_CELLNUMBER; m++)
                    {
                        values = (from c in GameConfig.cells where c.Column == m + 1 orderby c.ID descending select c.Value).ToList();
                        string str = ConvertListToString(values);
                        List<int> result = ArrayHandle(values);
                        string str2 = ConvertListToString(result);

                        int index = 0;
                        foreach (Cell cell in GameConfig.cells.Where(a => a.Column == m + 1).OrderByDescending(a => a.ID))
                        {
                            cell.Value = result[index];
                            index++;
                        }

                        if (!str.Equals(str2))
                            IsMove++;
                    }
                    break;
                case Global.Direction.Left:
                    for (int m = 0; m < GameConfig.GAMEMAP_CELLNUMBER; m++)
                    {
                        values = (from c in GameConfig.cells where c.Row == m + 1 orderby c.ID select c.Value).ToList();
                        string str = ConvertListToString(values);
                        List<int> result = ArrayHandle(values);
                        string str2 = ConvertListToString(result);

                        int index = 0;
                        foreach (Cell cell in GameConfig.cells.Where(a => a.Row == m + 1).OrderBy(a => a.ID))
                        {
                            cell.Value = result[index];
                            index++;
                        }

                        if (!str.Equals(str2))
                            IsMove++;
                    }
                    break;
                case Global.Direction.Right:
                    for (int m = 0; m < GameConfig.GAMEMAP_CELLNUMBER; m++)
                    {
                        values = (from c in GameConfig.cells where c.Row == m + 1 orderby c.ID descending select c.Value).ToList();
                        string str = ConvertListToString(values);
                        List<int> result = ArrayHandle(values);
                        string str2 = ConvertListToString(result);

                        int index = 0;
                        foreach (Cell cell in GameConfig.cells.Where(a => a.Row == m + 1).OrderByDescending(a => a.ID))
                        {
                            cell.Value = result[index];
                            index++;
                        }

                        if (!str.Equals(str2))
                            IsMove++;
                    }
                    break;
                default:
                    break;
            }
            PaintGameMap();

            return IsMove > 0;
        }

        /// <summary>
        /// 初始化数组
        /// </summary>
        private void InitializeCells()
        {
            for (int i = 1; i < 17; i++)
            {
                Cell cell = new Cell();
                cell.ID = i;
                if (i > 4 && i <= 8)
                {
                    cell.rectangle = new Rectangle(0 + GameConfig.GAMEMAP_CELLWIDTH * (i - 5), GameConfig.GAMEMAP_CELLWIDTH, GameConfig.CELLMAP_WIDTH, GameConfig.CELLMAP_WIDTH);
                    cell.Row = 2;
                    cell.Column = i - 4;
                }
                else if (i > 8 && i <= 12)
                {
                    cell.rectangle = new Rectangle(0 + GameConfig.GAMEMAP_CELLWIDTH * (i - 9), GameConfig.GAMEMAP_CELLWIDTH * 2, GameConfig.CELLMAP_WIDTH, GameConfig.CELLMAP_WIDTH);
                    cell.Row = 3;
                    cell.Column = i - 8;
                }
                else if (i > 12 && i <= 16)
                {
                    cell.rectangle = new Rectangle(0 + GameConfig.GAMEMAP_CELLWIDTH * (i - 13), GameConfig.GAMEMAP_CELLWIDTH * 3, GameConfig.CELLMAP_WIDTH, GameConfig.CELLMAP_WIDTH);
                    cell.Row = 4;
                    cell.Column = i - 12;
                }
                else
                {
                    cell.rectangle = new Rectangle(0 + GameConfig.GAMEMAP_CELLWIDTH * (i - 1), GameConfig.GAMEMAP_CELLWIDTH * 0, GameConfig.CELLMAP_WIDTH, GameConfig.CELLMAP_WIDTH);
                    cell.Row = 1;
                    cell.Column = i;
                }
                GameConfig.cells.Add(cell);
            }
        }

        private void CreateRandomCell(Graphics g)
        {
            try
            {
                Random r = new Random();
                int[] EmptyCells = (from c in GameConfig.cells where c.Value <= 0 select c.ID).ToArray();
                int cellvalue = GameConfig.RandomArray[r.Next(0, GameConfig.RandomArray.Length)];
                int cellid = EmptyCells[r.Next(0, EmptyCells.Length)];
                foreach (Cell cell in GameConfig.cells.Where(a => a.Value <= 0))
                {
                    if (cell.ID == cellid)
                    {
                        cell.Value = cellvalue;

                        Bitmap bitmap = new Bitmap(Image.FromFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Image\" + cell.Value.ToString() + ".png"));
                        g.DrawImage(bitmap, cell.rectangle);
                        break;
                    }
                }
            }
            catch 
            {
                ShowMessageAndLoadNewGame();
            }
        }

        /// <summary>
        /// 随机创建图片
        /// </summary>
        /// <param name="IsFirstLoad">是否为初次加载。True表示初次加载，随机生成两个图片；Flase表示在每次动作后随机生成一个图片</param>
        private void CreateRandomCell(bool IsFirstLoad)
        {
            Graphics g = this.CreateGraphics();
            try
            {
                if (IsFirstLoad)
                {
                    for (int i = 0; i < GameConfig.INITIALIZE_CELLAMOUNT; i++)
                    {
                        CreateRandomCell(g);
                    }
                }
                else
                {
                    CreateRandomCell(g);
                }
            }
            finally
            {
                g.Dispose();
            }
        }

        /// <summary>
        /// 绘制游戏画面
        /// </summary>
        private void PaintGameMap()
        {
            Graphics g = this.CreateGraphics();

            try
            {
                int result = 0;
                foreach (Cell cell in GameConfig.cells)
                {
                    if (cell.Value > 0) result++;
                    Bitmap bitmap = new Bitmap(Image.FromFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Image\" + cell.Value.ToString() + ".png"));
                    g.DrawImage(bitmap, cell.rectangle);
                }
                if (result == 0)
                {
                    ShowMessageAndLoadNewGame();
                }
            }
            finally
            {
                g.Dispose();
            }
        }

        /// <summary>
        /// 数组处理算法
        /// </summary>
        /// <param name="values">数组</param>
        private List<int> ArrayHandle(List<int> values)
        {
            bool IsAdd = false;

            for (int i = 0; i < GameConfig.GAMEMAP_CELLNUMBER; i++)
            {
                for (int j = i + 1; j < GameConfig.GAMEMAP_CELLNUMBER; j++)
                {
                    if (values[i] == values[i + 1] && values[i] != 0 && !IsAdd)
                    {
                        //显示得分
                        GameConfig.SCORE += values[i] + values[i + 1];
                        this.label2.Text = GameConfig.SCORE.ToString();

                        values[i] = values[i] + values[i + 1];
                        values.RemoveAt(i + 1);
                        values.Add(0);
                        IsAdd = true;
                    }
                    else if (values[i + 1] == 0)
                    {
                        values.RemoveAt(i + 1);
                        values.Add(0);
                    }
                    else
                        break;
                }
                //去除前面的0，去除后在最后补0
                if (i > 0)
                {
                    for (int j = i; j >= 0; j--)
                    {
                        if (values[j] == 0)
                        {
                            values.RemoveAt(j);
                            values.Add(0);
                        }
                    }
                }
                if (IsAdd) break;
            }
            return values;
        }

        private string ConvertListToString(List<int> list)
        {
            string str = string.Empty;
            foreach (int i in list)
            {
                str += i.ToString() + ",";
            }
            str = str.Substring(0, str.Length - 1);
            return str;
        }

        private bool JudgeListIsFull()
        {
            foreach (Cell cell in GameConfig.cells)
            {
                if (!cell.IsFill)
                {
                    return false;
                }
            }
            return true;
        }

        private void ShowMessageAndLoadNewGame()
        {
            MessageBox.Show("游戏结束！得分： " + GameConfig.SCORE.ToString());
            Environment.Exit(0);
            //GameConfig.cells.Clear();
            //InitializeCells();//初始化单元列表
            //PaintGameMap();
            //Graphics g = this.CreateGraphics();
            //g.DrawRectangle(GameConfig.pen, 0, 0, GameConfig.GAMEMAP_WIDTH, GameConfig.GAMEMAP_WIDTH);
            //for (int i = 1; i < GameConfig.GAMEMAP_CELLNUMBER; i++)
            //{
            //    g.DrawLine(GameConfig.pen, new Point(0, GameConfig.GAMEMAP_CELLWIDTH * i), new Point(GameConfig.GAMEMAP_WIDTH, GameConfig.GAMEMAP_CELLWIDTH * i));
            //    g.DrawLine(GameConfig.pen, new Point(GameConfig.GAMEMAP_CELLWIDTH * i, 0), new Point(GameConfig.GAMEMAP_CELLWIDTH * i, GameConfig.GAMEMAP_WIDTH));
            //}
            //CreateRandomCell(true);//随机生成两个图片
        }

        #endregion
    }
}
