using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;

namespace NetworkProjectServer
{
    class RestHandler
    {
        //Constructor
        public RestHandler(){ }

        //Brugernavn bruges kun til at oprette en ny bruger i tilfælde af at den ikke kan finde en med det ID man søger efter.
        public Person GetUser(long id, string brugernavn)
        {
            string urlresult = GetHttp("http://localhost:61565/api/person/" + id);

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Person));
            MemoryStream ms = new MemoryStream(UTF8Encoding.UTF8.GetBytes(urlresult));
            Person p = (Person)jsonSerializer.ReadObject(ms);
            if (p == null)
            {
                p = CreateUser(brugernavn);
                p.username = brugernavn;
                return p;
            }

            return p;
        }

        //bliver kun kaldt igennem GetUser for at sikre, at man ikke selv opretter en hel masse brugere selv.
        private Person CreateUser(string brugernavn)
        {
            DataContractJsonSerializer jsonPersonSerializer = new DataContractJsonSerializer(typeof(Person));
            Person p = new Person();
            p.username = brugernavn;
            p.goblin = 0;
            p.orc = 0;
            p.troll = 0;
            MemoryStream ms = new MemoryStream();
            jsonPersonSerializer.WriteObject(ms, p);
            string PostData = UTF8Encoding.UTF8.GetString(ms.GetBuffer());
            string urlresult = PostHttp("http://localhost:61565/api/person", PostData);

            ms = new MemoryStream(UTF8Encoding.UTF8.GetBytes(urlresult));
            p = (Person)jsonPersonSerializer.ReadObject(ms);

            return p;

        }

        //tager imod en Person objekt og bruger dette til at opdatere et objekt i databasen med hvad end nu man har fra dette objekt.
        public void UpdateUser(Person p)
        {
            DataContractJsonSerializer jsonPersonSerializer = new DataContractJsonSerializer(typeof(Person));
            MemoryStream ms = new MemoryStream();
            jsonPersonSerializer.WriteObject(ms, p);
            string PostData = UTF8Encoding.UTF8.GetString(ms.GetBuffer());
            string url = "http://localhost:61565/api/person/" + p.ID;
            string urlresult = PutHttp(url, PostData);
        }

        //metoden bag GetUser som egentlig får fat i et objekt ved at bede om objektet der tilhører dette id
        public static string GetHttp(string url)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = "GET";
            string result = null;

            using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

        //metoden bag CreateUser som egentlig sender en anmodning til REST-serveren om at lave et nyt objekt.
        public static string PostHttp(string url, string data)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            byte[] PostData = UTF8Encoding.UTF8.GetBytes(data);
            webRequest.ContentLength = PostData.Length;

            using (Stream post = webRequest.GetRequestStream())
            {
                post.Write(PostData, 0, PostData.Length);
            }

            string result;

            using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        //metoden bag UpdateUser som egentlig sender informationerne omkring hvad der skal opdateres ved et objekt.
        public static string PutHttp(string url, string data)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/json";

            byte[] PostData = UTF8Encoding.UTF8.GetBytes(data);
            webRequest.ContentLength = PostData.Length;

            using (Stream post = webRequest.GetRequestStream())
            {
                post.Write(PostData, 0, PostData.Length);
            }

            string result;

            using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        public static string GetAllHttp(string url)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            string result = null;

            using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
