//This script is used to manage the program's UI

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Canvases")]
    public Canvas menuCanvas;
    public Canvas ARCanvas;
    public Canvas attractionCanvas;
    public Canvas onLocationCanvas;
    public Canvas informationCanvas;

    [Header("Panels")]
    public GameObject navigationPanel;

    [Header("Attraction Panel")]
    public GameObject historicalPanel;
    public GameObject museumPanel;
    public GameObject galleryPanel;
    public GameObject recreationPanel;


    [Header("Template")]
    public GameObject attractionButtonTemplate;

    [Header("On Location Text")]
    public TextMeshProUGUI onLocationText;


    [Header("Location Information Text")]
    public TextMeshProUGUI locationNameText;
    public TextMeshProUGUI locationDescriptionText;


    public void HideAllCanvas()
    {
        menuCanvas.gameObject.SetActive(false);
        ARCanvas.gameObject.SetActive(false);
        attractionCanvas.gameObject.SetActive(false);
        onLocationCanvas.gameObject.SetActive(false);
        informationCanvas.gameObject.SetActive(false);
    }

    public void ShowMenuCanvas()
    {
        menuCanvas.gameObject.SetActive(true);

    }

    public void ShowARCanvas()
    {
        ToggleNavigationPanel(false);
        ARCanvas.gameObject.SetActive(true);
    }

    public void ShowAttractionCanvas()
    {
        attractionCanvas.gameObject.SetActive(true);
    }

    public void ShowOnLocationCanvas(bool toggle) 
    {
        if (toggle)
        {
            onLocationCanvas.gameObject.SetActive(true);
        }
        else
        {
            onLocationCanvas.gameObject.SetActive(false);
        }
            
    }

    public void ShowInformationCanvas()
    {
        if (informationCanvas.gameObject.activeSelf)
        {
            informationCanvas.gameObject.SetActive(false);
        }
        else
        {
            informationCanvas.gameObject.SetActive(true);
        }
    }

    public void EnterLocationSetUI(LocationInformation locationInformation)
    {
        ShowOnLocationCanvas(true);

        onLocationText.text = locationInformation.locationName;
        locationNameText.text = locationInformation.locationName;
        locationDescriptionText.text = locationInformation.locationDescription;

    }

    public void ExitLocationSetUI()
    {
        ShowOnLocationCanvas(false);
    }

    public void ToggleNavigationPanel(bool navigationOn)
    {
        if (navigationOn)
        {
            navigationPanel.SetActive(true);
        }
        else
        {
            navigationPanel.SetActive(false);
        }
    }

    public void AddAttractionButton(AttractionType locationType ,string locationName, string locationDescription, int locationIndex)
    {
        GameObject newButton;
        newButton = Instantiate(attractionButtonTemplate);

        if (locationType == AttractionType.museum)
        {
            newButton.transform.SetParent(museumPanel.transform, false);
        }
        else if (locationType == AttractionType.historicalSite)
        {
            newButton.transform.SetParent(historicalPanel.transform, false);
            
        }
        else if (locationType == AttractionType.recreation)
        {
            newButton.transform.SetParent(recreationPanel.transform, false);
        }
        else if (locationType == AttractionType.gallery)
        {
            newButton.transform.SetParent(galleryPanel.transform, false);
        }

        newButton.GetComponent<AttractionButtonScript>().setLocationName(locationName);
        newButton.GetComponent<AttractionButtonScript>().setLocationDescription(locationDescription);

        newButton.GetComponent<AttractionButtonScript>().setLocationIndex(locationIndex);
        newButton.name = locationName + " Button";

    }

    public void HideAttractionPanelCategory()
    {
        historicalPanel.gameObject.SetActive(false);
        museumPanel.gameObject.SetActive(false);
        galleryPanel.gameObject.SetActive(false);
        recreationPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}