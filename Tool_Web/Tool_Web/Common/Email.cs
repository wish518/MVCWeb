using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool_Web.Common
{
    public class Email
    {
        public void UseGmail()
        {
            //建立 SmtpClient 物件 並設定 Gmail的smtp主機及Port  
            System.Net.Mail.SmtpClient MySmtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com", 587);

            //設定你的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("f6110209@kimo.com", "dbsmjyuxuycokkat");

            //Gmial 的 smtp 必需要使用 SSL
            MySmtp.EnableSsl = true;

            //發送Email
            MySmtp.Send("f6110209@kimo.com", "wishwise518@gmail.com", "Gmail發信測試", "發信測試"); MySmtp.Dispose();
        }
    }
}