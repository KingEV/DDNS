using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DDNS
{
    class WaterWave
    {
        PictureBox pictureBox1;
        Timer timerDraw = new Timer();
        Bitmap m_bmp;
        byte[] byteBitmap1;      //存储原始图像数据
        byte[] byteBitmap2;      //存储根据波幅运算后的图像数据
        int[,,] bofu;      //记录波的振幅 一页振幅是与图片长宽相等的二维数组 这里需要两页分别表示前一时刻和当前时刻的波幅 所以是个三维数组只是第三维只有两页
        int nowbofu = 0;    //记录当前作用波幅在bofu数组的哪一页
        bool IsWave = false;  //标记当前是否有波动 没波动就不用计算图像偏移了
        int picWidth;           //图片宽度（像素）
        int picHeight;          //图片高度（像素）
        int picWidthByByte;     //图片宽度（内存字节数，比如24位图像一个像素点就是3个字节）

        public WaterWave()
        {//构造函数 设置了运算频率 
            timerDraw.Tick += new EventHandler(timerDraw_Tick);
            timerDraw.Interval = 40;//1000/40=25 每秒25帧图像肉眼观察不到停滞感;
            timerDraw.Enabled = true;
        }
        /// <summary>
        /// 指定需要呈现水波效果的图片控件PictureBox
        /// </summary>
        /// <param name="pbx">需要呈现水波效果的PictureBox控件</param>
        public void SetImage(ref PictureBox pbx)
        {
            pictureBox1 = pbx;
            //加载图像
            Bitmap bmp = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
            //将图片转换为24位也就是RGB，也可以是32位即多了一位表示透明度这里透明度是用不到的。
            m_bmp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format24bppRgb);
            //加载图像信息 初始化变量
            BitmapData bmpData = m_bmp.LockBits(new Rectangle(0, 0, m_bmp.Width, m_bmp.Height), ImageLockMode.ReadOnly, m_bmp.PixelFormat);
            byteBitmap1 = new byte[bmpData.Stride * bmpData.Height];//图像宽字节数*高字节数=总字节数
            byteBitmap2 = new byte[byteBitmap1.Length];//同上
            //波幅数组本是一个二维数组，行数和列数与图片像素相同，用来存放每一个像素点对应的振幅值
            //运算波纹扩散的过程中需要通过前一帧振幅值计算后一帧振幅值，所以需要两个这样的二维数组
            //所以就成了三维数组，第三维就两层 分别表示当前和下一时刻的波幅
            bofu = new int[m_bmp.Width, m_bmp.Height, 2];
            picWidth = m_bmp.Width;
            picHeight = m_bmp.Height;
            picWidthByByte = bmpData.Stride;
            Marshal.Copy(bmpData.Scan0, byteBitmap1, 0, byteBitmap1.Length);//原图复制到byteBitmap1存起来 内存操作效率高
            m_bmp.UnlockBits(bmpData);
        }
        //绘制水波
        private void timerDraw_Tick(object sender, EventArgs e)
        {//逐个像素点根据上一帧的波幅计算出新的（当前帧的）波幅，再根据新的波幅计算图像偏移量
            if (!IsWave)
                return;//如果前一帧运算结果中水波已经平静了（没一点的振幅都为0）就不用进行计算了节省资源
            IsWave = false;
            int newbofu = nowbofu == 0 ? 1 : 0;//两层波幅切换着使用，比如上一帧作用波幅是0层，那么当前帧的作用波幅就运算后存放在1层，反之亦然
            int nNewX = 0;
            int nNewY = 0;
            BitmapData bmpData = m_bmp.LockBits(new Rectangle(0, 0, m_bmp.Width, m_bmp.Height), ImageLockMode.ReadOnly, m_bmp.PixelFormat);
            Marshal.Copy(bmpData.Scan0, byteBitmap2, 0, byteBitmap2.Length);//原图复制到byteBitmap2 这时候byteBitmap2和byteBitmap1还是一样的
            //开始逐个像素点进行运算，为节省资源将新振幅的计算和图像的偏移放在同一个循环里面做了
            for (int y = 1; y < picHeight - 1; y++)
            {
                for (int x = 1; x < picWidth - 1; x++)
                {
                    //水波振幅扩散 根据当前波幅计算下一刻波幅
                    //当前点周围一圈8个点的能量加起来除以4减去前一次自己点的能量（推导得到的扩散近似公式）
                    //也有根据上下左右四个点来计算新增幅的 好处是运算量减少了 坏处是波纹太生硬
                    //除法用位运算实现以提高运算效率
                    //看清楚newbofu和nowbofu
                    bofu[x, y, newbofu] = ((
                        bofu[x - 1, y - 1, nowbofu] +
                        bofu[x - 1, y, nowbofu] +
                        bofu[x - 1, y + 1, nowbofu] +
                        bofu[x, y - 1, nowbofu] +
                        bofu[x, y + 1, nowbofu] +
                        bofu[x + 1, y - 1, nowbofu] +
                        bofu[x + 1, y, nowbofu] +
                        bofu[x + 1, y + 1, nowbofu]
                        ) >> 2) - bofu[x, y, newbofu];
                    //振幅是需要衰减的不然水波就扩散个没完了
                    //振幅衰减 位移越多衰减越少 衰减越少扩散得越开越持久
                    if (bofu[x, y, newbofu] != 0)
                    {
                        bofu[x, y, newbofu] -= bofu[x, y, newbofu] >> 5;
                        IsWave = true;//标记当前是否有波动 只要一个像素点有波动就标记为true 下一帧的时候就还需要运算
                    }
                    //运算像素偏移 (模拟折射)
                    //根据左右两点的波幅计算x轴偏移向量 根据上下两点的波幅计算y轴偏移向量
                    //两个点减一下“向”是定下来了“量”是有点大的需要衰减
                    //当前点引用的像素点坐标（偏移后）就是（偏移量x+当前点x，偏移量y+当前点y）
                    nNewX = ((bofu[x + 1, y, newbofu] - bofu[x - 1, y, newbofu]) >> 3) + x;   //位运算右移越大 衰减越大 偏移量越小 波纹越不明显
                    nNewY = ((bofu[x, y + 1, newbofu] - bofu[x, y - 1, newbofu]) >> 3) + y;   //偏移量小了波纹不明显 偏移量大了波纹太生硬

                    //偏移后的点坐标超出边界处理
                    if (nNewX < 0) nNewX = -nNewX;              //也可将其赋值为 0
                    if (nNewX >= picWidth) nNewX = picWidth - 1;
                    if (nNewY < 0) nNewY = -nNewY;
                    if (nNewY >= picHeight) nNewY = picHeight - 1;
                    //模拟光的反射 也可以跳过 不过波纹明暗度不明显
                    //byteBitmap2[y * picWidthByByte + x * 3] = byteBitmap1[nNewY * picWidthByByte + nNewX * 3];
                    //byteBitmap2[y * picWidthByByte + x * 3 + 1] = byteBitmap1[nNewY * picWidthByByte + nNewX * 3 + 1];
                    //byteBitmap2[y * picWidthByByte + x * 3 + 2] = byteBitmap1[nNewY * picWidthByByte + nNewX * 3 + 2];

                    //如果纯粹靠像素偏移来实现波纹效果，若图像是纯色的 再怎么偏移也是相同颜色 看不出波纹
                    //所以还要有明暗变化 图像变暗 
                    int nIncrement = bofu[x, y, newbofu];      //用当前像素点的波幅作为光线明暗度变化的参考值 负数变暗 正数变亮
                    nIncrement >>= nIncrement < 0 ? 3 : 2;     //适当的衰减 不然就太黑或太白了
                    //根据明暗值重置RGB值
                    int r = byteBitmap1[nNewY * picWidthByByte + nNewX * 3] + nIncrement;
                    int g = byteBitmap1[nNewY * picWidthByByte + nNewX * 3 + 1] + nIncrement;
                    int b = byteBitmap1[nNewY * picWidthByByte + nNewX * 3 + 2] + nIncrement;
                    //重置后的RGB值边界处理0-255
                    if (nIncrement < 0)
                    {
                        r = r < 0 ? 0 : r;
                        g = g < 0 ? 0 : g;
                        b = b < 0 ? 0 : b;
                    }
                    else
                    {
                        r = r > 255 ? 255 : r;
                        g = g > 255 ? 255 : g;
                        b = b > 255 ? 255 : b;
                    }
                    //到这里 这个像素点该显示什么像素值已经算好了 将新像素点信息保存在byteBitmap2中
                    byteBitmap2[y * picWidthByByte + x * 3] = (byte)r;
                    byteBitmap2[y * picWidthByByte + x * 3 + 1] = (byte)g;
                    byteBitmap2[y * picWidthByByte + x * 3 + 2] = (byte)b;
                }
            }
            Marshal.Copy(byteBitmap2, 0, bmpData.Scan0, byteBitmap2.Length);
            m_bmp.UnlockBits(bmpData);
            //pictureBox1.Refresh();
            pictureBox1.Image = m_bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //新计算出来的层做为作用层
            nowbofu = newbofu;
        }
        //设置波源 x,y波源坐标  r波源半径 h波源的能量大小
        /// <summary>
        /// 设置起波作用点点
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="r">作用点半径</param>
        /// <param name="h">作用点力度</param>
        public void SetWavePoint(int x, int y, int r, int h)
        {
            //判断波源所在矩形位置是否越出图像 以便将越出部分坐标重置
            int nXStart = x - r < 0 ? 0 : x - r;        //波源矩形位置x轴起点
            int nYStart = y - r < 0 ? 0 : y - r;        //波源矩形位置y轴起点
            int nXLen = x + r >= picWidth ? picWidth - 1 : x + r;     //波源x轴矩形长度
            int nYlen = y + r >= picHeight ? picHeight - 1 : y + r;   //波源y轴矩形长度
            for (int posX = nXStart; posX < nXLen; posX++)
            {
                for (int posY = nYStart; posY < nYlen; posY++)
                {    //以点(x,y)半径为r内的点赋值一个能量
                    if ((posX - x) * (posX - x) + (posY - y) * (posY - y) < r * r)
                    {
                        bofu[posX, posY, nowbofu] = -h;
                        IsWave = true;
                    }
                }
            }
        }
    }
}