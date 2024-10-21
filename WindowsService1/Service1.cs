using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }
        //...Utill.exe -u "path" ->> servisi silme
        protected override void OnStart(string[] args)
        {
            DosyaYaz("Servis çalışmaya başladı. " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;

        }

        protected override void OnStop()
        {
            DosyaYaz("Servis durdu. " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            DosyaYaz("Servis çalışıyor... " + DateTime.Now);
        }

        public void DosyaYaz(string mesaj)
        {
            //Kaydedilen dosyayı seçme
            string dosyaYolu = AppDomain.CurrentDomain.BaseDirectory + "/Logs";
            if (!Directory.Exists(dosyaYolu))
            {
                Directory.CreateDirectory(dosyaYolu);

            }
            string textYolu = AppDomain.CurrentDomain.BaseDirectory + "/Logs/servisim.txt";
            if (!File.Exists(textYolu))
            {
                using(StreamWriter sw = File.CreateText(textYolu))
                {
                    sw.WriteLine(mesaj);
                }
            }
            else
            {
                using(StreamWriter sw = File.AppendText(textYolu))
                {
                    sw.WriteLine(mesaj);
                }
            }
        }
    }
}
