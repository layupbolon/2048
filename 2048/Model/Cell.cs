using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2048
{
    public class Cell
    {
        private Rectangle _rectangle = new Rectangle();
        private int x = 0;
        private int y = 0;
        private int width = 0;
        private int height = 0;
        private int _value = 0;
        private bool _isFill = false;

        #region 构造函数

        public Cell()
        {

        }

        public Cell(int id, Rectangle rec)
        {
            this.ID = id;
            this._rectangle = rec;
            this.x = rec.X;
            this.y = rec.Y;
            this.width = rec.Width;
            this.height = rec.Height;
        }

        public Cell(int id, Rectangle rec,int value)
        {
            this.ID = id;
            this._rectangle = rec;
            this.x = rec.X;
            this.y = rec.Y;
            this.width = rec.Width;
            this.height = rec.Height;
            this.Value = value;
        }
        
        #endregion

        #region 属性
        
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        public Rectangle rectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        /// <summary>
        /// 所在行
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 所在列
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 坐标X
        /// </summary>
        public int X 
        {
            get { return x; }
            set 
            {
                _rectangle.X = this.x;
                x = value; 
            }
        }

        /// <summary>
        /// 坐标Y
        /// </summary>
        public int Y
        {
            get { return y; }
            set 
            {
                _rectangle.Y = this.y;
                y = value; 
            }
        }

        /// <summary>
        /// 宽
        /// </summary>
        public int Width
        {
            get { return width; }
            set 
            {
                _rectangle.Width = this.width;
                width = value; 
            }
        }

        /// <summary>
        /// 高
        /// </summary>
        public int Height
        {
            get { return height; }
            set 
            {
                _rectangle.Height = this.height;
                height = value; 
            }
        }

        /// <summary>
        /// 是否填充
        /// </summary>
        public bool IsFill
        {
            get { return _isFill; }
            set 
            {
                _isFill = _value > 0;
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public int Value
        {
            get { return _value; }
            set 
            {
                IsFill = value > 0;
                _value = value; 
            } 
        }
        #endregion


    }
}
