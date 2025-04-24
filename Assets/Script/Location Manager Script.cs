//This script act as a main manager, responsible for storing list of location information and keeping track of user's current location.

using ARLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class LocationManagerScript : MonoBehaviour
{

    [Header("Location Information")]
    public List<LocationInformation> locationList;

    [Header("Managers")]
    public UIManager UIManager;
    public VideoManager videoManager;
    public AudioManager audioManager;
    public SetTargetLocation setTarget;


    private bool isMarkerCurrentlyEnabled = true;
    private bool isInLocation = false;
    private int currentLocationIndex = -1;
    private List<GameObject> locationMarkerList = new List<GameObject>();
    private bool isInitializingLocation = false;

    void Start()
    {
        Permission.RequestUserPermission(Permission.FineLocation);
        InitializeLocation();

    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                hit.transform.gameObject.GetComponent<MarkerScript>().SetNavigation();
            }
        }
    }

    void InitializeLocation()
    {
 
        Debug.Log("Initializing Location");
        for (int index = 0; index < locationList.Count; index++)
        {
            GameObject placedMarker = locationList[index].spawnMarker(index);
            locationMarkerList.Add(placedMarker);

            UIManager.AddAttractionButton(locationList[index].attractionType, locationList[index].locationName, locationList[index].locationShortDescription, locationList[index].locationImage, index);

            Debug.Log("Created " + locationMarkerList[index].name);
        }

        isInitializingLocation = false;
    }


    public void ResetLocation()
    {
        Debug.Log("Resetting Location");

        UIManager.RemoveAllAttractionButton();
        UIManager.ExitLocationSetUI();
        UIManager.ToggleNavigationPanel(false);

        if (locationList[currentLocationIndex].GetPlacedModel() != null)
        {
            locationList[currentLocationIndex].RemoveModel();
        }

        for (int index = 0; index < locationMarkerList.Count; index++)
        {
            Debug.Log("Destroyed " + locationMarkerList[index].name);
            Destroy(locationMarkerList[index]);
            
        }
        locationMarkerList.Clear();
        
        if (!isInitializingLocation) 
        {
            isInitializingLocation = true;
            Invoke("InitializeLocation", 3f);
        }
        
    }    
 
    public void EnterLocation(int index)
    {
        isInLocation = true;
        if(index != currentLocationIndex || !UIManager.onLocationCanvas.isActiveAndEnabled)
        {
            currentLocationIndex = index;

            UIManager.EnterLocationSetUI(locationList[currentLocationIndex]);

            videoManager.SetPlaylist(locationList[currentLocationIndex]);

            audioManager.SetInfoSound(locationList[currentLocationIndex].locationInformationAudio);

            if (locationList[currentLocationIndex].disableThisMarkerOnEnter)
            {
                ToggleLocationMarker(index, false);

            }

            if (locationList[currentLocationIndex].disableOtherMarkerOnEnter)
            {
                ToggleLocationMarker(false);
            }

            Debug.Log("ENTER TRIGGERED");
        }

  
    }

    public void ToggleModel()
    {
        if (locationList[currentLocationIndex].GetPlacedModel() == null)
        {
            double currentLatitude = Input.location.lastData.latitude;
            double currentLongitude = Input.location.lastData.longitude;

            Location spawnModelLocation = new Location(currentLatitude, currentLongitude);

            locationList[currentLocationIndex].CreateModel(spawnModelLocation);
        }
        else if (locationList[currentLocationIndex].GetPlacedModel() != null)
        {
            locationList[currentLocationIndex].RemoveModel();
        }
    }
    public void DestroyModel()
    {
        locationList[currentLocationIndex].RemoveModel();
    }

    public void ExitLocation()
    {

        DestroyModel();
        isInLocation = false;
        UIManager.ExitLocationSetUI();
        ToggleLocationMarker(true);

        Debug.Log("EXIT TRIGGERED");

        currentLocationIndex = -1;

    }

    public LocationInformation GetLocationInfo(int index)
    {
        return locationList[index];
    }


    public void ToggleLocationMarker(bool isMarkerEnabled)
    {
        for (int index = 0; index < locationMarkerList.Count; index++)
        {
            SetModelRenderer(locationMarkerList[index], isMarkerCurrentlyEnabled);
            //locationMarkerList[index].SetActive(isMarkerEnabled);
        }

        isMarkerCurrentlyEnabled = isMarkerEnabled;
        UIManager.ToggleMarkerIcon(isMarkerEnabled);

        Debug.Log("CURRENT MARKER: " + isMarkerCurrentlyEnabled);
    }

    public void ToggleLocationMarker(int locationMarkerIndex, bool isMarkerEnabled)
    {
        SetModelRenderer(locationMarkerList[locationMarkerIndex], isMarkerCurrentlyEnabled);
        //locationMarkerList[locationMarkerIndex].SetActive(isMarkerEnabled);

        isMarkerCurrentlyEnabled = isMarkerEnabled;
        UIManager.ToggleMarkerIcon(isMarkerEnabled);

        Debug.Log("CURRENT MARKER: " + isMarkerCurrentlyEnabled);
    }

    public void ToggleLocationMarker()
    {
        isMarkerCurrentlyEnabled = !isMarkerCurrentlyEnabled;
        
        for (int index = 0; index < locationMarkerList.Count; index++)
        {

            SetModelRenderer(locationMarkerList[index], isMarkerCurrentlyEnabled);
            //locationMarkerList[index].SetActive(isMarkerCurrentlyEnabled);
        }

        UIManager.ToggleMarkerIcon(isMarkerCurrentlyEnabled);

        Debug.Log("CURRENT MARKER: " + isMarkerCurrentlyEnabled);

    }

    public void SetModelRenderer(GameObject model, bool isEnabled)
    {
        foreach (Transform child in model.transform)
        {
            child.gameObject.GetComponent<Renderer>().enabled = isEnabled;
        }
    }

    //DEBUGGING CLICK
    /*  private void OnMouseDown()
      {
          Debug.Log("Clicked");
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

          if (Physics.Raycast(ray, out hit, 1000))
          {
              hit.transform.gameObject.GetComponent<MarkerScript>().SetNavigation();
          }
      }*/

}
