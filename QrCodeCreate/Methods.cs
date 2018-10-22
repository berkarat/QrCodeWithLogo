using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace QrCodeCreate
{
    public class Methods
    {
        public static Bitmap GenerateQR (int width, int height, string js, out string err)
        {
            err="";
            try
            {
                if (js!=null)
                {
                    IBarcodeWriter barcodeWriter = new BarcodeWriter ();
                    barcodeWriter.Format=BarcodeFormat.QR_CODE;

                    QrCodeEncodingOptions options = new QrCodeEncodingOptions ();
                    options=new QrCodeEncodingOptions
                    {

                        Width=width,
                        Height=height,
                        Margin=0


                    };
                    options.Hints.Add (EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

                    barcodeWriter.Options=options;

                    var result = new Bitmap (barcodeWriter.Write (js));

                    return result;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                err="METİN GİRİNİZ!";
                return null;
            }

            }


        public static Bitmap GenerateQR_WithLogo (int width, int height, string imagePath, string js, out string err)
        {
            err="";
            try
            {

                if (js!=null)
                {
                    var bw = new ZXing.BarcodeWriter ();

                    var encOptions = new ZXing.Common.EncodingOptions
                    {
                        Width=width,
                        Height=height,



                    };


                    encOptions.Hints.Add (EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
                    encOptions.Hints.Add (EncodeHintType.CHARACTER_SET, "UTF-8");

                    encOptions.Hints.Add (EncodeHintType.MAX_SIZE, 15);
                    encOptions.Hints.Add (EncodeHintType.MARGIN, 0);



                    bw.Renderer=new BitmapRenderer ();
                    bw.Options=encOptions;
                    bw.Format=ZXing.BarcodeFormat.QR_CODE;
                    Bitmap bm = bw.Write (js);
                    Bitmap overlay = new Bitmap (imagePath);

                    int deltaHeigth = bm.Height-overlay.Height;
                    int deltaWidth = bm.Width-overlay.Width;

                    Graphics g = Graphics.FromImage (bm);
                    g.DrawImage (overlay, new System.Drawing.Point (deltaWidth/2, deltaHeigth/2));

                    return bm;

                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {
                err="METİN GİRİNİZ!";
                return null;

            }
        }

        public static string js (string TerminalID, DateTime date, string LanguageID, string BankaID, string refno)
        {
            var test = new JsonTest ()
            {
                ReferansNo=refno,
                Terminal_ID=TerminalID,
                date=date,
                Language_ID=LanguageID,
                Banka_ID=BankaID
            };


            return JsonConvert.SerializeObject (test, Formatting.Indented);
        }
        public static BitmapImage BitmapToImageSource (Bitmap bitmap)
        {
            if (bitmap!=null)
            {
                using (MemoryStream memory = new MemoryStream ())
                {
                    bitmap.Save (memory, ImageFormat.Jpeg);
                    memory.Position=0;
                    BitmapImage bitmapimage = new BitmapImage ();

                    memory.Seek (0, SeekOrigin.Begin);
                    bitmapimage.BeginInit ();
                    bitmapimage.StreamSource=memory;
                    bitmapimage.CacheOption=BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit ();

                    return bitmapimage;
                }
            }
            else
            {
                return null;
            }

        }

        private static Random random = new Random ();
        public static string RandomString (int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string (Enumerable.Repeat (chars, length)
              .Select (s => s[random.Next (s.Length)]).ToArray ());
        }
    }
}
