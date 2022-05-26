using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);



            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            RawImage image = GameObject.Find("RawImage").GetComponent<RawImage>();
            image.color = new Color32(255, 255, 225, 255);
            image.texture = tex;
            image.enabled = true;

        }
        return tex;
    }



    public void SavePictureAs() {

        //var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "");

        // Save file async
        StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", "jpg", (string path) => {

            byte[] arr = File.ReadAllBytes(Application.persistentDataPath + "/FotoTemp.jpg");

            System.IO.File.WriteAllBytes(path, arr);

        });

    } 

    public void MailPicture() {


        bool parametersAreMissing = SenderPassword.Length == 0 || SenderEmail.Length == 0 || ProviderPort == 0 || SendToMail == null || SendToName == null;
        Debug.Assert(!parametersAreMissing, "parameters missing");
        Debug.Assert(IsEmail(SendToMail.text), "not a valid email");

        if (parametersAreMissing) return;

        string mailReciever = SendToMail.text.Trim();

        if (!IsEmail(mailReciever))
        {
            print(SendToMail.text); return;
        }

        try
        {
            SmtpClient smtp = new SmtpClient();
            smtp.TargetName = "STARTTLS/smtp.office365.com";
            smtp.Port = ProviderPort;
            smtp.Host = "smtp.office365.com"; //for outlook test
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage();
            print(SenderEmail); 
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
            print(e);

        }






    }


    public void SavePicture() {

        camera.GetComponent<TakeScreenshot>().TakeScreenShot();
        LoadPNG(Application.persistentDataPath + "/FotoTemp.jpg"); 
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
                roomname + " en het spel kan worden gedownload via: https://drive.google.com/drive/folders/1ixO7WjGG6PoE7e2mYd4s0v1dHKs4U9d1?usp=sharing";
        }

        if (lang == "english") {
            return "Hallo,"  +  name +
               "! Do you want to play greenify Alkmaar with me? My Joincode is:, " + roomname + "and the game can be downloaded on: " +
                "https://drive.google.com/drive/folders/1ixO7WjGG6PoE7e2mYd4s0v1dHKs4U9d1?usp=sharing";
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
