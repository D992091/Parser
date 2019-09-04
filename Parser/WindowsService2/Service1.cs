using LSP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService2
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
         public Models.Database1Entities db = new Models.Database1Entities();

        protected override void OnStart(string[] args)
        {
              MultiStart();
        }

        protected override void OnStop()
        {
           //db.Table1.RemoveRange(db.Table1);
           // db.Table2.RemoveRange(db.Table2);
           // db.Table3.RemoveRange(db.Table3);
           // db.SaveChanges();
        }
        public  void run()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson[]));
            using (FileStream fs = new FileStream(@"C:\Users\Daniil\source\repos\LSP\LSP\bin\Debug\First.json", FileMode.OpenOrCreate))
            {
                WorkWithJson[] json = (WorkWithJson[])jsonFormatter.ReadObject(fs);
                WorkWithJson[] left = (from i in json
                                      join db in db.Table1
                       on i.id equals db.IdNews into gj
                                      from temp in gj.DefaultIfEmpty()
                                      where temp == null
                                      select i).ToArray();
                    foreach(WorkWithJson l in left)
                    {
                        Models.Table1 tb = new Models.Table1();
                        tb.IdNews = l.id;
                        tb.Author = l.nameofauthor;
                        db.Table1.Add(tb);
                        db.SaveChanges();
                }
            }
        }
        public void run1()
        {
            DataContractJsonSerializer jsonFormatter1 = new DataContractJsonSerializer(typeof(WorkWithJson1[]));
            using (FileStream fs = new FileStream(@"C:\Users\Daniil\source\repos\LSP\LSP\bin\Debug\Second.json", FileMode.OpenOrCreate))
            {
                WorkWithJson1[] json = (WorkWithJson1[])jsonFormatter1.ReadObject(fs);
                WorkWithJson1[] left = (from i in json
                                       join db in db.Table2
                        on i.id equals db.IdNews1 into gj
                                       from temp in gj.DefaultIfEmpty()
                                       where temp == null
                                       select i).ToArray();
                foreach (WorkWithJson1 l in left)
                {

                    Models.Table2 tb = new Models.Table2();
                    tb.IdNews1 = l.id;
                    tb.LikeCount = l.likeCount;
                    tb.RepostCount = l.repostCount;
                    tb.Text = l.text;
                    tb.Time = l.time;
                    tb.Viewes = l.views;
                    db.Table2.Add(tb);
                    db.SaveChanges();
                }
            }
        }
        public void run2()
        {
            DataContractJsonSerializer jsonFormatter2 = new DataContractJsonSerializer(typeof(WorkWithJson2[]));
            using (FileStream fs = new FileStream(@"C:\Users\Daniil\source\repos\LSP\LSP\bin\Debug\Third.json", FileMode.OpenOrCreate))
            {
                WorkWithJson2[] json = (WorkWithJson2[])jsonFormatter2.ReadObject(fs);
                WorkWithJson2[] left = (from i in json
                                        join db in db.Table3
                         on i.id equals db.IdNews2 into gj
                                        from temp in gj.DefaultIfEmpty()
                                        where temp == null
                                        select i).ToArray();
                foreach(WorkWithJson2 r in left)
                    {

                        for (int j = 0; j < r.pictures.Length; j++)
                        {
                            Models.Table3 tb2 = new Models.Table3();
                            tb2.IdNews2 = r.id;
                            tb2.Picture = r.pictures[j];
                            tb2.Path = r.path[j];
                            db.Table3.Add(tb2);
                            db.SaveChanges();
                        }
                    }
                }
            }
        public void MultiStart()
        {
            Thread thr = new Thread(run);
            Thread thr1 = new Thread(run1);
            Thread thr2 = new Thread(run2);
            thr.Start();
            thr.Join();
            thr1.Start();
            thr1.Join();
            thr2.Start();
            thr2.Join();
        }
        public void start()
        {
            run();
        }
    }
}
