using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using DataAcess;
using System.Collections.Generic;

public class GoogleMail
{
    private int _port;
    private String _email, _password, _smtp;
    private bool _ssl;

    public String Smtp
    {
        get { return _smtp; }
        set { _smtp = value; }
    }

    public String Password
    {
        get { return _password; }
        set { _password = value; }
    }

    public String Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public int Port
    {
        get { return _port; }
        set { _port = value; }
    }

    public bool SSL
    {
        get { return _ssl; }
        set { _ssl = value; }
    }

    public List<Attachment> Attachments { get; set; }

    public GoogleMail(int? custom)
    {

    }

    public GoogleMail()
    {
        try
        {
            EntityContext db = new EntityContext();
            var tt = db.ad_systemconfig.FirstOrDefault();
            if (tt != null)
            {
                this._email = tt.taikhoanemail;
                this._password = tt.passemail;
                this._smtp = tt.emailserver;
                this._port = int.Parse(tt.port);
            }
            else
            {
                throw new Exception("Lỗi: Không tìm thấy thông tin chung.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Send(String to, String subject, String body, String attachments, params object[] args)
    {
        String from = _email;
        Send2(from, to, "", "", subject, body,attachments, args);
    }

    public void Send2(String from, String to, String cc, String bcc, String subject, String body,String attachments, params object[] args)
    {

        if (Regex.IsMatch(body, "^([cCdDeEfFgGhHiIjI]:).+"))
        {
            body = File.ReadAllText(body);
        }
        body = String.Format(body, args);

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(from);
        //mail.ReplyTo = new MailAddress(from);


        int dem = 0;
        if (attachments != "" & attachments != null)
        {
            dem = attachments.Split('#').Count();
        }
        for (int i = 0; i < dem - 1; i++)
        {
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(attachments.Split('#')[i]);
            mail.Attachments.Add(attachment);
        }

        if (Attachments != null)
        {
            foreach (var attachment in Attachments)
            {
                mail.Attachments.Add(attachment);
            }
        }

        to = to.Replace(" ", "");
        string[] tos = to.Split(";,".ToArray());
        foreach (string item in tos)
        {
            mail.To.Add(new MailAddress(item));
        }

        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        if (!String.IsNullOrEmpty(cc))
        {
            mail.CC.Add(cc);
        }

        if (!String.IsNullOrEmpty(bcc))
        {
            mail.Bcc.Add(bcc);
        }

        //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
        SmtpClient client = new SmtpClient(_smtp, _port);
        //client.Timeout = 1000;
        client.EnableSsl = SSL;
        client.UseDefaultCredentials = true;
        client.Credentials = new NetworkCredential(_email, _password);
        client.Send(mail);

        if (Attachments != null)
        {
            foreach (var attachment in Attachments)
            {
                attachment.Dispose();
            }
        }

        mail.Dispose();
        client.Dispose();
    }
}
