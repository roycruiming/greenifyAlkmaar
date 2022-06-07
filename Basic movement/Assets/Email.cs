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



    private enum EmailSuccessOrFailure{ Success, No_Parameters, Could_Not_Reach, Invalid_Mail }

    



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

        if (parametersAreMissing) { Debug.Assert(!parametersAreMissing, "parameters missing");
            return; 
        }


        if (SendToMail.text == "" || SendToName.text == "") {

            StartCoroutine(ShowMailStatus(EmailSuccessOrFailure.No_Parameters));
            return;

        }

        

        string mailReciever = SendToMail.text.Trim();

        if (!IsEmail(mailReciever))
        {
            StartCoroutine(ShowMailStatus(EmailSuccessOrFailure.Invalid_Mail));
            return;
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
            StartCoroutine(ShowMailStatus(EmailSuccessOrFailure.Success));


        }
        catch (Exception e)
        {
            StartCoroutine(ShowMailStatus(EmailSuccessOrFailure.Could_Not_Reach));

        }






    }



    private IEnumerator ShowMailStatus(EmailSuccessOrFailure e ) {

        GameObject g = GameObject.Find("Uncaptureable UI");
        if (g == null) 
            yield return null;


        Transform messageContainer = g.transform.Find("PressFContainer");
        if (messageContainer == null) 
            yield return null;



        SetMailMessageContainerValues(messageContainer.gameObject, e); 

       messageContainer.gameObject.SetActive(true);

         

       yield return new WaitForSeconds(3);

        g.transform.Find("PressFContainer").gameObject.SetActive(false); 
    }


    private void SetMailMessageContainerValues(GameObject g,  EmailSuccessOrFailure e) {

        if (e == EmailSuccessOrFailure.Success)
        {
            g.transform.Find("ScreenDisplayText").GetComponent<TextMeshProUGUI>().text = "success";
        }
        else if (e == EmailSuccessOrFailure.Could_Not_Reach)
        {
            g.transform.Find("ScreenDisplayText").GetComponent<TextMeshProUGUI>().text = "failure";
        }

        else if (e == EmailSuccessOrFailure.No_Parameters)
        {
            g.transform.Find("ScreenDisplayText").GetComponent<TextMeshProUGUI>().text = "parameters are mmissing";
        }

        else {
            g.transform.Find("ScreenDisplayText").GetComponent<TextMeshProUGUI>().text = "invalid mail";
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



        if (lang == "Nederlands")
            {

                return "<h1 style=font-family:verdana; align=center> Hallo, " + char.ToUpper(name[0]) + name.Substring(1) + "! </h1> " +
                    "<h2 align=center> Heb jij toevallig zin om samen greenify Alkmaar te spelen? </h2>  <p align=center> De joincode is: <b>" +
                    roomname + " </b> </p>  <p align=center>  Het spel kan worden gedownload via:  <a href=https://drive.google.com/drive/folders/1ixO7WjGG6PoE7e2mYd4s0v1dHKs4U9d1?usp=sharing> deze link </a> </p>";
            }

            if (lang == "English")
            {
                return "<h1 style=font-family:verdana; align=center> Hello, " + char.ToUpper(name[0]) + name.Substring(1) + "! </h1> " +
                    "<h2 align=center> Do you want to play Greenify Alkmaar with me?</h2>  <p align=center> The roomname is: <b>" +
                    roomname + " </b> </p> " +
                    " <p align=center> and the game can be download with <a href=https://drive.google.com/drive/folders/1ixO7WjGG6PoE7e2mYd4s0v1dHKs4U9d1?usp=sharing> this link </a> </p>";
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

   

