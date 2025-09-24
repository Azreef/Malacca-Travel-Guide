using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenTutorialManager : MonoBehaviour
{
    public Image tutorialImage;       
    public Sprite[] tutorialSprites;
    public GameObject tutorialSpriteCanvas;
    private int currentIndex = 0;

    void Start()
    {
        ShowImage(0);
    }

    void OnEnable()
    {
        currentIndex = 0;
        ShowImage(0);
    }

    public void ShowImage(int index)
    {
        if (index >= 0 && index < tutorialSprites.Length)
        {
            tutorialImage.sprite = tutorialSprites[index];
        }
        else
        {
            tutorialSpriteCanvas.SetActive(false);
            currentIndex = 0;
        }
           
    }

    public void NextImage()
    {
        currentIndex++;
        if (currentIndex >= tutorialSprites.Length)
        {
            tutorialSpriteCanvas.SetActive(false); 
            return;
        }
        ShowImage(currentIndex);
    }

    public void SkipTutorial()
    {
        tutorialSpriteCanvas.SetActive(false);
        currentIndex = 0;
    }
}
