using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LSP
{
    public class Search
    {
        public static List<String> likes;
        public static List<String> reposts;
        public static List<String> viewes;
        public static List<String> authors;
        public static List<String> time;
        public static List<String> text;
        public static List<String[]> pictures = new List<string[]>();
        public static List<String> id = new List<string>();
        public static List<String[]> path = new List<string[]>();
        public static void FindLike(ChromeDriver chr)
        {
            List<IWebElement> list = (chr.FindElements(By.CssSelector("div.like_wrap:not(.lite) a.like_btn.like._like div.like_button_count"))).ToList();
            likes = (from li in list select li.Text).ToList();
        }
        public static void FindRepost(ChromeDriver chr)
        {
            List<IWebElement> list = chr.FindElementsByCssSelector("a.like_btn.share._share").ToList();
            reposts = (from li in list select li.Text).ToList();
        }
        public static void FindView(ChromeDriver chr)
        {
            List<IWebElement> list = chr.FindElementsByCssSelector(".like_wrap:not(.lite) .like_cont .like_views._views").ToList();
            viewes = (from li in list select li.Text).ToList();
        }
        public static void FindAuthor(ChromeDriver chr)
        {
            List<IWebElement> list = chr.FindElementsByClassName("post_header").ToList();
            authors = (from it in list select it.FindElement(By.ClassName("author")).Text).ToList();
        }
        public static void FindTime(ChromeDriver chr)
        {
            List<IWebElement> list = chr.FindElementsByClassName("post_link").ToList();
            time = (from it in list select it.Text).ToList();
        }
        public static void FindText(ChromeDriver chr)
        {
            List<IWebElement> list = chr.FindElementsByClassName("wall_text").ToList();
            text = (from li in list select li.Text).ToList();
        }
        public static void FindPictures(ChromeDriver chr)
        {
            for (int i = 0; i < id.Count; i++)
            {
               List<IWebElement> url = (chr.FindElements(By.CssSelector($"#{id[i]} div.page_post_sized_thumbs.clear_fix > a"))).ToList();
                List<String> lst = (from it in url select it.GetAttribute("style")).ToList();
                String[] arr = new string[lst.Count];
                Regex myReg = new Regex(@"https\S+jpg");
                for (int a = 0; a < lst.Count; a++)
                {

                        foreach (Match m in myReg.Matches(lst[a]))
                        {
                            lst[a] = m.Value.Replace('\\', ' ');
                        }

                    for(int j = 0; j < lst.Count; j++)
                    {
                        arr[j] = lst[j];
                    }
                }
                pictures.Add(arr);
            }

        }
        public static void FindId(ChromeDriver chr)
        {
            List<IWebElement> list = chr.FindElementsByCssSelector("div[class = '_post post page_block']").ToList();
            id = (from li in list select li.GetAttribute("id")).ToList();
        }
        public static void GetPath(ChromeDriver chr)
        {
            Regex myReg = new Regex(@"\w+\.jpg", RegexOptions.Compiled |
            RegexOptions.Singleline);
            WebClient web = new WebClient();
            string path1 = @"C:\Users\Daniil\source\repos\LSP\LSP\bin\Debug\Pictures\";
            for (int i = 0; i < pictures.Count; i++)
            {
                String[] arr = new string[pictures[i].Length];
                for (int j = 0; j < pictures[i].Length; j++)
                {
                    if ((pictures[i][j].Length> 1))
                    {
                        foreach (Match m in myReg.Matches(pictures[i][j]))
                        {
                            if (m.Success)
                            {
                                web.DownloadFile(pictures[i][j], path1 + m.Value);
                                String str1 = path1 + m.Value;
                                arr[j] = str1;
                            }
                        }
                    }
                    else
                    {
                        arr[j] = "";
                    }
                }
                path.Add(arr);
            }
        }
    }
}
