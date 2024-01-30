using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Endscreen : MonoBehaviour
{

    [SerializeField] private bool playerWin;
    [SerializeField] private GameObject WinImage;
    [SerializeField] private GameObject LooseImage;
    [SerializeField] private GameObject LooseImage2;
    [SerializeField] private GameObject returnButton;

    private float timer;
    [SerializeField] private float timeUntillImage2;
    private bool shownImage = false;

    [SerializeField] private float fadeSpeed = 0.4f;

    private CanvasGroup WinAlpha;
    private CanvasGroup LooseAlpha;
    private CanvasGroup LooseAlpha2;
    private CanvasGroup returnAlpha;


    // Start is called before the first frame update

    private void Awake()
    {
        //WinImage.SetActive(false);
        //LooseImage.SetActive(false) ;
        //LooseImage2.SetActive(false) ;
        // returnButton.SetActive(false) ;

        WinAlpha = WinImage.GetComponent<CanvasGroup>();
        LooseAlpha = LooseImage.GetComponent<CanvasGroup>();
        LooseAlpha2 = LooseImage2.GetComponent<CanvasGroup>();
        returnAlpha = returnButton.GetComponent<CanvasGroup>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }

    void Start()
    {

        WinAlpha.alpha = 0;
        LooseAlpha.alpha = 0;
        LooseAlpha2.alpha = 0;
        returnAlpha.alpha = 0;

        timer = timeUntillImage2;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playerWin)
        {
            FadeImage(WinAlpha, false);
        }
        else
        {
            FadeImage(LooseAlpha, false);

           
        }

        if (timer < 0)
        {
            
            if (!playerWin)
            {
                FadeImage(LooseAlpha, true);

                FadeImage(LooseAlpha2, false);
                //shownImage = true;
            }
                
            FadeImage(returnAlpha, false);
            //shownImage = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }


    public void FadeImage(CanvasGroup img, bool fadeOut)
    {
        if (fadeOut)
        {
            //img.alpha = 1;
            if (img.alpha < 0 ) 
            {
                if (img.alpha !=0 ) 
                {
                    img.alpha -= Time.deltaTime * fadeSpeed;
                }
               
            }


        }
        else
        {
            //img.alpha = 0;
            //img.alpha = 1;
            if (img.alpha < 1)
            {
                if (img.alpha != 1)
                {
                    img.alpha += Time.deltaTime * fadeSpeed;
                }
            }
        }
    }


    //IEnumerator (Image img, bool fadeAway)
    //{
    //    // fade from opaque to transparent
    //    if (fadeAway)
    //    {
    //        img.color = new Color(1, 1, 1, 0);
    //        // loop over 1 second backwards
    //        for (float i = 1; i >= 0; i -= Time.deltaTime)
    //        {
    //            // set color with i as alpha
    //            img.color = new Color(1, 1, 1, i);
    //            yield return null;
    //        }
    //    }
    //    // fade from transparent to opaque
    //    else
    //    {
    //        img.color = new Color(1, 1, 1, 1);
    //        // loop over 1 second
    //        for (float i = 0; i <= 1; i += Time.deltaTime)
    //        {
    //            // set color with i as alpha
    //            img.color = new Color(1, 1, 1, i);
    //            yield return null;
    //        }
    //    }
    //}

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
