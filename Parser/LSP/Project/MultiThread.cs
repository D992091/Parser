using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSP
{
    public class MultiThread
    {
        public static void MakeThread(ChromeDriver chr)
        {
            Thread tr1 = new Thread(() => Search.FindAuthor(chr));
            Thread tr2 = new Thread(() => Search.FindTime(chr));
            Thread tr3 = new Thread(() => Search.FindLike(chr));
            Thread tr4 = new Thread(() => Search.FindRepost(chr));
            Thread tr5 = new Thread(() => Search.FindView(chr));
            Thread tr6 = new Thread(() => Search.FindText(chr));
            Thread tr8 = new Thread(() => Search.FindId(chr));
            Thread tr7 = new Thread(() => Search.FindPictures(chr));
            Thread tr9 = new Thread(() => Search.GetPath(chr));
            tr1.Start();
            tr1.Join();
            tr2.Start();
            tr2.Join();
            tr3.Start();
            tr3.Join();
            tr4.Start();
            tr4.Join();
            tr5.Start();
            tr5.Join();
            tr6.Start();
            tr6.Join();
            tr8.Start();
            tr8.Join();
            tr7.Start();
            tr7.Join();
            tr9.Start();
            tr9.Join();
        }
    }
}
