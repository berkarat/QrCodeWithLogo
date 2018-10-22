using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QrCodeCreate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();

           
       
        }

        private void button_Click (object sender, RoutedEventArgs e)
        {

            //İSTEĞE BAĞLI OLARAK JSON SERİLİAZE olabilir
            //var test = new JsonTest ()
            //{
            //    ReferansNo=Methods.RandomString (8),
            //    Terminal_ID="00011",
            //    date=DateTime.Now,
            //    Language_ID="1",
            //    Banka_ID="010102"
            //};


            //string js = JsonConvert.SerializeObject (test, Formatting.Indented);
            string err = "";
            image.Source=Methods.BitmapToImageSource (Methods.GenerateQR (290, 290, textBox.Text,out err));
            if (err.Length>0)
            {
                MessageBox.Show (err);
            }
        }

        private void button1_Click (object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click_1 (object sender, RoutedEventArgs e)
        {
            string err = "";
            OpenFileDialog openFileDialog = new OpenFileDialog ();
           
            if (openFileDialog.ShowDialog ()==true) { 
                string path = openFileDialog.FileName;


                FileInfo fi = new FileInfo (path);
                string ext = fi.Extension;
                
                if (ext.ToLower ()==".jpg"||ext.ToLower()==".jpeg" ||ext.ToLower ()==".png")
                {
                    if (File.Exists (path))
                    {


                        long length = new System.IO.FileInfo (path).Length;

                        if (length<2000)
                        {
                            image.Source=Methods.BitmapToImageSource (Methods.GenerateQR_WithLogo (290, 290, path, textBox.Text, out err));


                        }
                        else
                        {
                            MessageBox.Show ("daha küçük boyutlu dosya seçiniz");
                        }


                        if (err.Length>0)
                        {
                            MessageBox.Show (err);
                        }




                    }

                }
                else
                {
                    MessageBox.Show ("GEÇERLİ BİR UZANTI GİRİNİZ");
                }
              
            }

        }
    }
}
