using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestImageResizing.PNGOptimizators
{
    public class DefaultSaveOptimizer
    {
        //public static byte[] Use(byte[] bytes)
        //{
        //    return UseDefault(bytes);
        //}

        public Bitmap CreateThumbnailAndCompressing(Bitmap loBMP, int lnWidth)
        {

            System.Drawing.Bitmap bmpOut = null;

            try
            {
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width <= lnWidth)
                {
                    return loBMP;
                }

                lnRatio = (decimal)lnWidth / loBMP.Width;
                lnNewWidth = lnWidth;
                decimal lnTemp = loBMP.Height * lnRatio;
                lnNewHeight = (int)lnTemp;

                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bmpOut;
        }
    }
}
