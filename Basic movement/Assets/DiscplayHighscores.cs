using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscplayHighscores : MonoBehaviour
{

    public Text[] highscoreText;
    SubmitScore submitScore;



    void Start()
    {
        for (int i =0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". Fetching...";
        }

        submitScore = GetComponent<SubmitScore>();

        StartCoroutine("RefreshHishscores");
    }

    public void OnHighscoresDownload(SubmitScore.Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";
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
