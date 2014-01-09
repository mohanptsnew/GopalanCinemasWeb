using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace GopalanCinemasEntities
{
    public partial class commonfunctions
    {
        string MailServer = "smtp.gmail.com";
        //string MailServer = "216.139.220.106";
        string MailComponent = "net";
        string SmtpMailId = "support@gopalancinemas.com";
        string SmtpMailPwd = "gop@%123";
        string SmtpMailPort = "587";
        public int _intFetchRecordFrom = 0;
        public int _intFetchRecordTo = 0;
        public int _intStartPosition = 0;
        public static bool IsNumeric(string strToCheck)
        {
            try
            {
                //return Regex.IsMatch(strToCheck, "^\\d+(\\.\\d+)?$");
                int result;
                return Int32.TryParse(strToCheck, out result);
            }
            catch
            {
                return false;
            }
        }
        public static bool IsDate(string dtToCheck)
        {
            string strDate = dtToCheck;
            try
            {
                //DateTime dt = DateTime.Parse(strDate);
                //if ((dt.Month <= System.DateTime.Now.Month || dt.Month >= System.DateTime.Now.Month) && (dt.Day < 1 && dt.Day > 31) && (dt.Year <= System.DateTime.Now.Year || dt.Year >= System.DateTime.Now.Year))
                //    return false;
                //else
                //    return true;
                DateTime dt;
                return DateTime.TryParse(dtToCheck, out dt);
            }
            catch
            {
                return false;
            }
        }
        public static bool IsNextDate(string dtToCheck)
        {
            string strDate = dtToCheck;
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                DateTime ndt = DateTime.Now;
                if (dt > ndt)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        #region Send SMS

        public string sendsms(string mobileno, string msg)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://websms.one97.net/sendsms/push_sms_new.php?user=pvr_app&pwd=pvr_app&from=PVR&to=" + mobileno + "&msg=" + msg);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);
            return sb.ToString();
        }
        #endregion

        public string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            Message = Message.Replace(" ", "+");
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }


        public string SendNetMail(string mailFrom, string mailTo, string copyTo, string htmlBody, string subject, string sFile)
        {
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress("support@gopalancinemas.com", "Gopalan Cinemas");

            smtpClient.Host = MailServer;
            smtpClient.Port = int.Parse(SmtpMailPort);
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("support@gopalancinemas.com", "gop@%123");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = SMTPUserInfo;


            message.From = fromAddress;
            message.To.Add(mailTo);

            if (copyTo.Trim() != "" && copyTo.Trim() != null)
            {
                string[] list_of_email = copyTo.Split(',');
                System.Net.Mail.MailAddress bcc = new System.Net.Mail.MailAddress(list_of_email[0].ToString());
                message.Bcc.Add(bcc);
                for (int y = 1; y < list_of_email.Length; y++)
                {
                    bcc = new System.Net.Mail.MailAddress(list_of_email[y].ToString());
                    message.Bcc.Add(bcc);
                }
            }
            if (sFile != "" && sFile != null)
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(sFile);
                message.Attachments.Add(attachment);
            }
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = htmlBody;
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
            message.Dispose();
            return "success";
        }
        public string paging(int _intStartPosition, int _intRecordsPerPage)
        {
            StringBuilder sb = new StringBuilder();
            if (_intStartPosition == 1)
            {
                _intFetchRecordFrom = 0;
                _intFetchRecordTo = _intRecordsPerPage;
                _intStartPosition = 1;
            }
            else
            {
                _intFetchRecordFrom = ((_intStartPosition - 1) * _intRecordsPerPage);
                _intFetchRecordTo = _intFetchRecordFrom + _intRecordsPerPage;
            }
            sb.Append(_intFetchRecordFrom);
            sb.Append("|");
            sb.Append(_intFetchRecordTo);
            return sb.ToString();
        }
    }
}
