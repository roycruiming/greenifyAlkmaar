using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutScript : MonoBehaviour
{
    private Image image;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        Color transZeroCol = image.color;
        transZeroCol.a = 0f;
        image.color = transZeroCol;
    }

        // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    public void StartFading() {
        StartCoroutine(FadeImage(false));
    }

    public void StartFadingOut() {
        StartCoroutine(FadeImage(true));
    }


}
