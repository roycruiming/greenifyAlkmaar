using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Email : MonoBehaviour
{



    public string SenderPassword;
    public string SenderEmail;
    public int ProviderPort;
    public  bool sendWithPicture; 
    public TMP_InputField SendToName;
    public TMP_InputField SendToMail;
    public TMP_InputField RoomName;


    public GameObject camera; 


    private void Awake()
    {

    }


    public void SendEmailWithPicture() {
        bool parametersAreMissing =  SenderPassword.Length == 0 || SenderEmail.Length == 0 || ProviderPort == 0 || SendToMail == null || SendToName == null;
        Debug.Assert(!parametersAreMissing, "parameters missing");
        Debug.Assert(IsEmail(SendToMail.text), "not a valid email");

        if (parametersAreMissing) return;

        


        string mailReciever = SendToMail.text.Trim();

     
        if (!IsEmail(mailReciever))
        {
            print(SendToMail.text); return;
        }

        camera.GetComponent<TakeScreenshot>().TakeScreenShot();



        try
        {
         

            SmtpClient smtp = new SmtpClient();
            smtp.Port = ProviderPort;
            smtp.Host = "smtp.gmail.com"; //for gmail host
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


            MailMessage message = new MailMessage();
            message.From = new MailAddress(SenderEmail);
            message.To.Add(new MailAddress(mailReciever));
            message.Subject = "Come play Greenify Alkmaar with me!";
            message.IsBodyHtml = true; //to make message body as html

            string path = Application.persistentDataPath + "/FotoTemp.jpg";

            message.AlternateViews.Add(GetEmbeddedImage(path));
       

            string name = SendToName.text.Trim();


            message.Body = getBody(name);



            smtp.Send(message);
            SendToName.text = "";
            SendToMail.text = "";



        }
        catch (Exception e)
        {


        }



     






    }

    private AlternateView GetEmbeddedImage(string filePath)
    {
        LinkedResource res = new LinkedResource(filePath);
        res.ContentId = Guid.NewGuid().ToString();
        string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
        AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
        alternateView.LinkedResources.Add(res);
        return alternateView;

    }




    public void SendEmail()

    {


        string pin = RoomName.text;
        pin = pin.Trim();


        bool parametersAreMissing = pin.Length == 0 || SenderPassword.Length == 0 || SenderEmail.Length == 0 || ProviderPort == 0 || SendToMail == null || SendToName == null ;
        Debug.Assert(!parametersAreMissing, "parameters missing");
        Debug.Assert(IsEmail(SendToMail.text), "not a valid email");

        string mailReciever = SendToMail.text.Trim();

        if (parametersAreMissing) return;
        if (!IsEmail(mailReciever)) {
            print(SendToMail.text); return; }



        try
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(SenderEmail);
            message.To.Add(new MailAddress(mailReciever));
            message.Subject = "Come play Greenify Alkmaar with me!";
            message.IsBodyHtml = true; //to make message body as html

            string name = SendToName.text.Trim();


            message.Body = getBody(name, pin);


            smtp.Port = ProviderPort;
            smtp.Host = "smtp.gmail.com"; //for gmail host
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            SendToName.text = "";
            SendToMail.text = "";



        }
        catch (Exception e) {


        }
    }

    public bool IsEmail(string emailString) {


        emailString = emailString.Trim();


         return Regex.IsMatch(emailString,
             @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
             RegexOptions.IgnoreCase);
    }

    public string getBody(string name, string roomname)
    {
        string lang = GlobalGameHandler.GetCurrentLanguage();


        if (lang == "nederlands") {

            return "Hallo, " +  name + "! Heb jij toevallig zin om samen greenify alkmaar te spelen? De joincode is: " +
                roomname + " en het spel kan worden gedownload via: testwebsite.nl";
        }

        if (lang == "english") {
            return "Hallo,"  +  name +
               "! Do you want to play greenify Alkmaar with me? My Joincode is:, " + roomname + "and the game can be downloaded on: " +
                "testwebsite.nl";
        }

        return "something went wrong, ignore this message ";

    }

    public string getBody(string name)
    {
        string lang = "nederlands";


        if (lang == "nederlands")
        {

            return "test";
        }

        if (lang == "english")
        {
            return "Hallo," + name +
               "! Do you want to play greenify Alkmaar with me? My Joincode is:, and the game can be downloaded on: " +
                "testwebsite.nl";
        }

        return "something went wrong, ignore this message ";

    }
}
