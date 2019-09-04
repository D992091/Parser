using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace LSP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ChromeOptions op = new ChromeOptions();
            op.AddArgument("--start-maximized");
            ChromeDriver chr = new ChromeDriver(op);
            chr.Navigate().GoToUrl(Storage.URL());
            Moves.setTextInField(chr, "index_email", Storage.getLogin());
            Moves.setTextInField(chr, "index_pass", Storage.getPass());
            Thread.Sleep(2500);
           Moves.setClickOnField(chr, "index_login_button");
            Thread.Sleep(3500);
            while (Moves.posts < 60)
            {
               Moves.MoveDown(chr, 10000, 10000);
            }
           MultiThread.MakeThread(chr);
            JSON js = new JSON(Search.authors,Search.id);
            js.save("First.json");
            JSON js1 = new JSON(Search.likes, Search.text,Search.reposts,Search.viewes,Search.id,Search.time);
            js1.save("Second.json",1);
            JSON js2 = new JSON(Search.id,Search.pictures,Search.path);
            js2.save("Third.json",true);
         }
    }
}
