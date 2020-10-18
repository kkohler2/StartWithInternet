using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace StartWithInternet
{
    public class Executable
    {
        public string Name { get; set; }
        public string Parameters { get; set; }
    }

    public class Executables
    {
        public Executable[] Programs { get; set; }
    }

    class Program
    {
        static string IPAddress;
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();

            var executables = new Executables();

            config.GetSection("Executables").Bind(executables);

            Console.Write("Checking for internet");
            while (!HavePublicIP())
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine($"\n{IPAddress}");

            foreach(Executable executable in executables.Programs)
            {
                Process process = new Process();
                process.StartInfo.FileName = executable.Name;
                //process.StartInfo.UseShellExecute = true;
                if (!string.IsNullOrWhiteSpace(executable.Parameters))
                {
                    process.StartInfo.Arguments = executable.Parameters;
                }
                process.Start();
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/3253701/get-public-external-ip-address
        /// </summary>
        /// <returns></returns>
        public static bool HavePublicIP()
        {
            try
            {
                string url = "http://checkip.dyndns.org";
                WebRequest req = WebRequest.Create(url);
                WebResponse resp = req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                string response = sr.ReadToEnd().Trim();
                string[] a = response.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                IPAddress = a3[0];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}