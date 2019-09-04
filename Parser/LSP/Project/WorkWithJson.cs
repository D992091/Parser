using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace LSP
{
    [DataContract]
    public class WorkWithJson
    {
        [DataMember]
       public String nameofauthor;
        [DataMember]
       public String id;
        public WorkWithJson() { }
        public WorkWithJson(String Name, String Id)
        {
            nameofauthor = Name;
            id = Id;
        }
    }
    [DataContract]
    public class WorkWithJson1
    {
        [DataMember]
       public String time;
        [DataMember]
        public String text;
        [DataMember]
        public String likeCount;
        [DataMember]
        public String repostCount;
        [DataMember]
        public String views;
        [DataMember]
        public String id;
        public WorkWithJson1(String Like, String Text, String Repost, String Views, String Id, String Time)
        {
            text = Text;
            id = Id;
            likeCount = Like;
            repostCount = Repost;
            views = Views;
            time = Time;
        }
    }
    [DataContract]
    public class WorkWithJson2
    {
    [DataMember]
        public String id;
    [DataMember]
        public String[] pictures;
    [DataMember]
        public String[] path;

        public WorkWithJson2(String Id, String[] Pict, String[] Path)
        {
            id = Id;
            pictures = Pict;
            path = Path;
        }

    }
    public class JSON
    {
        public WorkWithJson[] json;
        public WorkWithJson1[] json1;
        public WorkWithJson2[] json2;
        object locker1 = new object();
        object locker2 = new object();
        object locker3 = new object();
        public JSON(
        List<String> authors,
         List<String> id)
        {
            WorkWithJson[] json11 = new WorkWithJson[50];
            for (int i = 0; i < 50; i++)
            {
                json11[i] = new WorkWithJson(authors[i],id[i]);
            }
            json = json11;
        }
        public JSON(List<String> likes,
         List<String> text, 
         List<String> repost,
         List<String> views,
         List<String> id,
         List<String> time)
        {
            WorkWithJson1[] json00 = new WorkWithJson1[50];
            for (int i = 0; i < 50; i++)
            {
                json00[i] = new WorkWithJson1(likes[i],text[i],repost[i],views[i], id[i],time[i]);
            }
            json1 = json00;
        }
        public JSON(List<String> id,
         List<String[]> pictures,
         List<String[]> path)
        {
            WorkWithJson2[] json22 = new WorkWithJson2[50];
            for (int i = 0; i < 50; i++)
                    {
                        json22[i] = new WorkWithJson2(id[i], pictures[i], path[i]);
                    }
                    json2 = json22;
        }

        public void save(String name)
        {
            lock (locker1)
            {
                if (File.Exists(name))
                {
                    WorkWithJson[] info = open(name);
                    WorkWithJson[] left = (from i in info
                                           join j in json
                            on i.id equals j.id into gj
                                           from temp in gj.DefaultIfEmpty()
                                           where temp == null
                                           select i).ToArray();

                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson[]));
                    File.Delete(name);
                    using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
                    {
                        jsonFormatter.WriteObject(fs,((from a in json select a).Union(from b in left select b)).ToArray());
                    }
                }
                else
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson[]));
                    using (FileStream fs = new FileStream(name, FileMode.Create))
                    {
                        jsonFormatter.WriteObject(fs, json);
                    }
                }
            }
        }
        public void save(String name,int id)
        {
            lock (locker2)
            {
                if (File.Exists(name))
                {

                    WorkWithJson1[] info1 = open1(name);
                    WorkWithJson1[] left = (from i in info1
                                            join j in json1
                             on i.id equals j.id into gj
                                            from temp in gj.DefaultIfEmpty()
                                            where temp == null
                                            select i).ToArray();
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson1[]));
                    File.Delete(name);
                    using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
                    {
                        jsonFormatter.WriteObject(fs, ((from a in json1 select a).Union(from b in left select b)).ToArray());
                    }
                }
                else
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson1[]));
                    using (FileStream fs = new FileStream(name, FileMode.Create))
                    {
                        jsonFormatter.WriteObject(fs, json1);
                    }
                }
            }

        }
        public void save(String name,bool v)
        {
            lock (locker3)
            {
                if (File.Exists(name))
                {
                    WorkWithJson2[] info2 = open2(name);
                    WorkWithJson2[] left = (from i in info2
                                            join j in json2
                             on i.id equals j.id into gj
                                            from temp in gj.DefaultIfEmpty()
                                            where temp == null
                                            select i).ToArray();
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson2[]));
                    File.Delete(name);
                    using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
                    {
                            jsonFormatter.WriteObject(fs, ((from a in json2 select a).Union(from b in left select b)).ToArray());
                        }
                }
                else
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson2[]));
                    using (FileStream fs = new FileStream(name, FileMode.Create))
                    {
                        jsonFormatter.WriteObject(fs, json2);
                    }
                }
            }
        }
        public WorkWithJson[] open(String name)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson[]));
            using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
            {
                WorkWithJson[] json0 = (WorkWithJson[])jsonFormatter.ReadObject(fs);
                return json0;
            }
        }
        public WorkWithJson1[] open1(String name)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson1[]));
            using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
            {
                WorkWithJson1[] json11 = (WorkWithJson1[])jsonFormatter.ReadObject(fs);
                return json11;
            }
        }
             public WorkWithJson2[] open2(String name)
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(WorkWithJson2[]));
                    using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
                    {
                WorkWithJson2[] json22 = (WorkWithJson2[])jsonFormatter.ReadObject(fs);
                return json22;
            }
                }
    }

}
