
using BitirmeProjesiErp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Xml;

//bazen program a��l�rken kurlardan dolay� hata veriyor, kurlar� veritaban�na max 7-8 g�n kaydedecek �ekilde model mi a�sam bu �ekil devam m� etse
// g�lay hocaya sorulacak.
namespace BitirmeProjesiErp

{
    class Sabitler
    {
        // Post metodlar� i�in gerekli adresler
        public static string sisEk = "/api/v3/sis/json";
        public static string scfEk = "/api/v3/scf/json";
        public static string rstEk = "/api/v3/rst/json";
        public static string rprEk = "/api/v3/rpr/json";
        public static string sunucuAdi = "";
        public static string sunucuOn = "http://";
        public static string sunucuArka = ".dia.gen.tr";
        public static string sunucuAdresi;
        public static string apiKey = "736c1d33-e92f-4131-86f1-775dd7885d47";
        //public static string sunucuAdresi = "http://rcuretimo.dia.gen.tr";

        public static string kurlar;
        public static string eur;
        public static string usd;


        
        //  session_id her istek gonderilirken kullan�lacak.
        public static string session_id = "";

        // Se�ili firma ve d�nem kodu isteklerde s�kl�kla kullan�ld��� ortak alanda tutaca��m.
        public static int seciliFirmaKodu;
        public static int seciliDonemKodu;
        // Server'dan cekilen firma_sube_donem listesi bunu ilerde veritaban�na kaydedece�im
        public static dynamic firma_sube_donem_listesi = null;      
        public static dynamic carikartlistesi = null;
        public static dynamic stokkartlistesi = null;
        // Server'dan cekilen carikart listesi bunu ilerde veritaban�na kaydedece�im
        public static dynamic tekliflistesi = null;  

       

        
        // Server ile ileti�imi kuran fonksiyon. Parametre olarak g�nderilen obje JSON string'ine �evrilir.
        // Ba�lant� ayarlar� yap�l�r ve istek g�nderilir. Gelen cevap yine dinamik bir yap�da geri d�nderilir.
        // sunucuya i�ine girilen expando objecti json stringine �evirip uygun ek ile servera yollar
        public static dynamic sendMessageToServer(dynamic obj, string ek)
        {
            
            string jsonWS = "";
            sunucuAdresi = ("http://" + sunucuAdi + ".dia.get.tr");
            try
            {
                
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(sunucuOn + sunucuAdi + sunucuArka + ek);
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create(Sabitler.sunucuAdresi + ek);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    jsonWS = JsonConvert.SerializeObject(obj);


                    
                    // debug i�in jsonu yazar
                    //Console.WriteLine("JSON: " + jsonWS);

                    streamWriter.Write(jsonWS);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    dynamic ret = JsonConvert.DeserializeObject<ExpandoObject>(result);
                    return ret;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("sendMessageToServer --- sunucu: " + sunucuAdresi + ek + " --- json: " + jsonWS);
                Console.WriteLine(e);
            }
            return null;
        }
        public static dynamic DiaLogin(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.login = new ExpandoObject();
            // burda @i�areti kullanmazsam params� method olarak al�yor ve hata veriyor.
            request.login.@params = new ExpandoObject();
            request.login.@params.apikey = apiKey;
            request.login.username = webServisBilgi.UserName;
            request.login.password = webServisBilgi.Sifre;

            // request.login.apikey = "736c1d33-e92f-4131-86f1-775dd7885d47";
            request.login.disconnect_same_user = "True";
            request.login.lang = "tr";
            Sabitler.sunucuAdi = webServisBilgi.SunucuAdi;

            dynamic response = Sabitler.sendMessageToServer(request, Sabitler.sisEk);
            if (response == null)
            {
                Console.WriteLine("Bir hata olu�tu. L�tfen daha sonra tekrar deneyiniz.");

            }
            else if (response.code == "200")
            {
                Console.WriteLine("Login ba�ar�l� oldu. session_id: " + response.msg);
                Sabitler.session_id = response.msg;
            }
            else
            {
                
                Console.WriteLine("Giri� yaparken bir hata olu�tu. Code: " + response.code + " Message: " + response.msg);

            }
            return (response);
        }
        // buldugum bir kur �ekici, daha denemedim.
        public static void Kurlar()
        {
            string bugun = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(bugun);

            string EURO_Alis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            string EURO_Satis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;

            string USD_Alis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            string USD_Satis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;

            //Console.WriteLine(string.Format("Euro Al��: {0:C2}", EURO_Alis));
            //Console.WriteLine(string.Format("Euro Sat��: {0:C2}", EURO_Satis));
            //Console.WriteLine(string.Format("USD Al��: {0:C2}", USD_Alis));
            //Console.WriteLine(string.Format("USD Sat��: {0:C2}", USD_Satis));
            //Console.ReadKey();
            usd = USD_Satis;
            eur = EURO_Satis;
            kurlar = "Usd :" + USD_Satis + " Eur :" + EURO_Satis;
            //return (kurlar);

        }
                
    }
}
