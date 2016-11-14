﻿using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tor;
using Tor.Config;

namespace SiteProcessor
{
    public class SiteDownloader
    {
        private const int CONTROL_PORT = 9051;
        private const string PATH = @"Tor\Tor\tor.exe";


        public void DownloadSite(string url, string path)
        {

            /*JUST EXAMPLE WILL BE FIXED */

            ClientCreateParams createParameters = new ClientCreateParams();
            createParameters.ConfigurationFile = "";
            createParameters.ControlPassword = "";
            createParameters.ControlPort = CONTROL_PORT;
            createParameters.DefaultConfigurationFile = "";
            createParameters.Path = PATH;

            createParameters.SetConfig(ConfigurationNames.AvoidDiskWrites, true);
            createParameters.SetConfig(ConfigurationNames.GeoIPFile, Path.Combine(Environment.CurrentDirectory, @"Tor\Data\Tor\geoip"));
            createParameters.SetConfig(ConfigurationNames.GeoIPv6File, Path.Combine(Environment.CurrentDirectory, @"Tor\Data\Tor\geoip6"));

            Client client = Client.Create(createParameters);

            Console.WriteLine(client.Proxy.WebProxy.ToString());

            Console.ReadLine();


            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();

            service.AddArguments(new string[] {
                "--proxy-type=socks5","--proxy=127.0.0.1:9050"
            });



            using (IWebDriver driver = new PhantomJSDriver(service))
            {
                driver.Navigate().GoToUrl(@"http://2ip.ru");
                Console.WriteLine("GoToURL");
                using (FileStream fs = new FileStream("123.txt", FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(driver.PageSource);
                    }
                }

            }
        }
    }
}