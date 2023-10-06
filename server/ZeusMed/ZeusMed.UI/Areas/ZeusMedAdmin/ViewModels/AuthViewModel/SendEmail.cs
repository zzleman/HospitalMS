//using System;
//using System.Net;
//using System.Net.Mail;

//namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AuthViewModel;

//public class Sendmail
//{
//    public static void Email(string to, string subject, string body)
//    {
//        MailMessage mm = new();
//        mm.To.Add(new MailAddress(to));
//        mm.From = new MailAddress("zeusmed2023@outlook.com");
//        mm.Subject = subject;
//        mm.Body = body;
//        mm.IsBodyHtml = true;
//        mm.Priority = MailPriority.High;
//        SmtpClient smtpClient = new();
//        smtpClient.Host = "smtp.gmail.com";
//        smtpClient.Port = 587;
//        smtpClient.EnableSsl = true;
//        smtpClient.Credentials = new NetworkCredential("zeusmed2023@outlook.com", "D5BD51223F39831190486977A79B7E5DE1B4");
//        smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
//        smtpClient.Send(mm);

//    }

//}