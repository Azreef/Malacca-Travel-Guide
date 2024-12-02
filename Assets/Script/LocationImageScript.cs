using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationImageScript : MonoBehaviour
{
    public bool isLarge = false;
    public GameObject largeImage;
    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    public void toggleLarge()
    {
        if (isLarge) 
        {
            largeImage.SetActive(false);
        }
        else
        {
            largeImage.SetActive(true);
        }
    }

}
