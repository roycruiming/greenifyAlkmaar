using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SubmitScore : MonoBehaviour
{


    const string privateCode = "CWbGB-vmdUukd35Xm-FDbgK7fNBqKjKUu9XQeqAYP9pQ";
    const string publicCode = "624573208f40bc123c45e13e";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;
    static SubmitScore instance;
    DiscplayHighscores highscoresDisplay;


    public struct Highscore
    {
        public string username;
        public int score;
        public Highscore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }

    public void Awake()
    {
        
        highscoresDisplay = GetComponent<DiscplayHighscores>();
        instance = this;
    }

    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
        Debug.Log(username);
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Succesful");
        }
        else
        {
            print("Error Uploading" + www.error);
        }
    }

    public void DownloadHighScores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    [System.Obsolete]
    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            highscoresDisplay.OnHighscoresDownload(highscoresList);
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i <entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }

		public void SubmitScores(string name, int result)
    {
        //dreamlo.AddScore(name, result);
    }
}
