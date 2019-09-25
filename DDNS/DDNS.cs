using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DDNS
{
    class DDNS
    {
        public static string Version = "2015-01-09";
        public static string SignatureMethod = "HMAC-SHA1";

        public static bool updateIP(string localIP, string accessSecret, string accessKeyId, string recordId,string signature, string signatureNonce, string timestamp)
        {
            bool updateResult = true;
            string getUrl = "http://alidns.aliyuncs.com/?AccessKeyId=" + accessKeyId + "&Action=UpdateDomainRecord&Format=JSON&Line=default&RecordId=" + recordId+ "&RR=www&SignatureMethod=HMAC-SHA1&SignatureNonce=" + signatureNonce + "&SignatureVersion=1.0&TTL=600&Type=A&Value=" + localIP + "&Version=2015-01-09&Signature=" + signature + "&Timestamp=" + timestamp;
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(getUrl);
            HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
            Stream stream = hwp.GetResponseStream();
            string result = "";
            try
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                JObject obj = JObject.Parse(result);
                try
                {
                    string recordid = obj["RecordId"].ToString();
                    string requestid = obj["RequestId"].ToString();
                    updateResult = true;
                }
                catch(Exception)
                {
                    updateResult = false;
                }
            }
            return updateResult;
        }
        public static string getLocalIP()
        {
            WebRequest request = WebRequest.Create("http://pv.sohu.com/cityjson?ie=utf-8");
            request.Timeout = 10000;
            WebResponse response = request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream, Encoding.Default);
            string htmlinfo = sr.ReadToEnd();
            //匹配IP的正则表达式
            Regex r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
            Match mc = r.Match(htmlinfo);
            //获取匹配到的Ip
            resStream.Close();
            sr.Close();
            //Console.WriteLine(mc.Groups[0].Value);//打印获取到的ip
            string localIP = mc.Groups[0].Value;
            return localIP;
        }

        public static string updateSignatureStr(string localIP, string accessSecret, string accessKeyId, string recordId, string signatureNonce, string timestamp)
        {
            string key = accessSecret + "&";
            StringBuilder builder = new StringBuilder();
            string para = "AccessKeyId=" + accessKeyId + "&Action=UpdateDomainRecord&Format=JSON&Line=default&RR=www&RecordId="+recordId+ "&SignatureMethod=HMAC-SHA1&SignatureNonce=" + signatureNonce + "&SignatureVersion=1.0&TTL=600&Timestamp=" + timestamp + "&Type=A&Value=" + localIP + "&Version="+Version;
            foreach (char c in para)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            para = builder.ToString();
            //因为不需要对第一个“&”编码，但是需要对第一个“/”编码，所以直接处理好当成字符串放在前边（GET&%2F&）。
            string stringToSign = "GET&%2F&" + para.Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] byteText = myHMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
            return HttpUtility.UrlEncode(Convert.ToBase64String(byteText));
        }

        public static string getRecord(string accessKeyId,string accessSecret,string recordId,string signature,string timestamp,string signatureNonce)
        {
            string recordIP;
            string getRecordUrl = "http://alidns.aliyuncs.com/?AccessKeyId=" + accessKeyId + "&Action=DescribeDomainRecordInfo&Format=JSON&RecordId="+recordId+ "&SignatureMethod=HMAC-SHA1&SignatureNonce=" + signatureNonce + "&SignatureVersion=1.0&Version=2015-01-09&Signature=" + signature + "&Timestamp=" + timestamp;
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(getRecordUrl);
            HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
            Stream stream = hwp.GetResponseStream();
            string result = "";
            try
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                string jsonStr = result.ToString();
                JObject jobj = JObject.Parse(jsonStr);
                //获取当前阿里云解析记录的IP
                recordIP = jobj["Value"].ToString();
            }
            return recordIP;
        }
        public static string signature(string accessSecret,string accessKeyId,string recordId,string signatureNonce,string timestamp)
        {
            string key = accessSecret + "&";
            string para = "AccessKeyId="+accessKeyId+ "&Action=DescribeDomainRecordInfo&Format=JSON&RecordId="+recordId+ "&SignatureMethod=HMAC-SHA1&SignatureNonce=" + signatureNonce + "&SignatureVersion=1.0&Timestamp=" + timestamp + "&Version="+Version;
            StringBuilder builder = new StringBuilder();
            //C#urlencode是小写，但是阿里云要求的是大写，foreach把小写换成大写
            foreach (char c in para)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            para = builder.ToString();
            //因为不需要对第一个“&”编码，但是需要对第一个“/”编码，所以直接处理好当成字符串放在前边（GET&%2F&）。
            string stringToSign = "GET&%2F&" + para.Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] byteText = myHMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
            return HttpUtility.UrlEncode(Convert.ToBase64String(byteText));
        }
        //获取UTC时间，url中的时间必须和签名中的随机数一致
        public static string timeSpan()
        {
            string time = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
            StringBuilder builder = new StringBuilder();
            foreach (char c in time)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
        //生成14位随机数，url中的随机数必须和签名中的随机数一样！！！
        public static string uuNum()
        {
            string uuNum = "";
            for (int i = 0; i < 14; i++)
            {
                if (i == 0)
                {
                    uuNum += new Random(Guid.NewGuid().GetHashCode()).Next(1, 9);
                }
                else
                {
                    uuNum += new Random(Guid.NewGuid().GetHashCode()).Next(0, 9);
                }
            }
            return uuNum;
        }
    }
}
