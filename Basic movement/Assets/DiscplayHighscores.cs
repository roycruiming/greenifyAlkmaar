using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscplayHighscores : MonoBehaviour
{

    public Text[] highscoreText;
    SubmitScore submitScore;
    int counter = 10;



    void Start()
    {
        for (int i =0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = counter + ". Fetching...";
            counter--;
        }

        submitScore = GetComponent<SubmitScore>();

        StartCoroutine("RefreshHishscores");
    }

    public void OnHighscoresDownload(SubmitScore.Highscore[] highscoreList)
    {
        counter = 10;
        for (int i = 0; i < highscoreText.Length; i++)
        {
            
            highscoreText[i].text = counter + ". ";
            counter--;
            if(highscoreList.Length > i)
            {
                highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }

    IEnumerator RefreshHishscores()
    {
        while (true)
        {
            submitScore.DownloadHighScores();
            yield return new WaitForSeconds(30);
        }
    }


}
