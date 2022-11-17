
using BitirmeProjesiErp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Xml;

//bazen program açýlýrken kurlardan dolayý hata veriyor, kurlarý veritabanýna max 7-8 gün kaydedecek þekilde model mi açsam bu þekil devam mý etse
// gülay hocaya sorulacak.
namespace BitirmeProjesiErp

{
    class Sabitler
    {
        // Post metodlarý için gerekli adresler
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


        
        //  session_id her istek gonderilirken kullanýlacak.
        public static string session_id = "";

        // Seçili firma ve dönem kodu isteklerde sýklýkla kullanýldýðý ortak alanda tutacaðým.
        public static int seciliFirmaKodu;
        public static int seciliDonemKodu;
        // Server'dan cekilen firma_sube_donem listesi bunu ilerde veritabanýna kaydedeceðim
        public static dynamic firma_sube_donem_listesi = null;      
        public static dynamic carikartlistesi = null;
        public static dynamic stokkartlistesi = null;
        // Server'dan cekilen carikart listesi bunu ilerde veritabanýna kaydedeceðim
        public static dynamic tekliflistesi = null;  

       

        
        // Server ile iletiþimi kuran fonksiyon. Parametre olarak gönderilen obje JSON string'ine çevrilir.
        // Baðlantý ayarlarý yapýlýr ve istek gönderilir. Gelen cevap yine dinamik bir yapýda geri dönderilir.
        // sunucuya içine girilen expando objecti json stringine çevirip uygun ek ile servera yollar
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


                    
                    // debug için jsonu yazar
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
            // burda @iþareti kullanmazsam paramsý method olarak alýyor ve hata veriyor.
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
                Console.WriteLine("Bir hata oluþtu. Lütfen daha sonra tekrar deneyiniz.");

            }
            else if (response.code == "200")
            {
                Console.WriteLine("Login baþarýlý oldu. session_id: " + response.msg);
                Sabitler.session_id = response.msg;
            }
            else
            {
                
                Console.WriteLine("Giriþ yaparken bir hata oluþtu. Code: " + response.code + " Message: " + response.msg);

            }
            return (response);
        }
        // buldugum bir kur çekici, daha denemedim.
        public static void Kurlar()
        {
            string bugun = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(bugun);

            string EURO_Alis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            string EURO_Satis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;

            string USD_Alis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            string USD_Satis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;

            //Console.WriteLine(string.Format("Euro Alýþ: {0:C2}", EURO_Alis));
            //Console.WriteLine(string.Format("Euro Satýþ: {0:C2}", EURO_Satis));
            //Console.WriteLine(string.Format("USD Alýþ: {0:C2}", USD_Alis));
            //Console.WriteLine(string.Format("USD Satýþ: {0:C2}", USD_Satis));
            //Console.ReadKey();
            usd = USD_Satis;
            eur = EURO_Satis;
            kurlar = "Usd :" + USD_Satis + " Eur :" + EURO_Satis;
            //return (kurlar);

        }
                
    }
}
