//This script act as a main manager, responsible for storing list of location information and keeping track of user's current location.

using ARLocation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class LocationManagerScript : MonoBehaviour
{

    [Header("Location Information")]
    public List<LocationInformation> locationList;

    [Header("Managers")]
    public UIManager UIManager;
    public VideoManager videoManager;
    public AudioManager audioManager;
    public SetTargetLocation setTarget;

    [Header("Debug")]
    public TextMeshProUGUI debugTextDistance;

    private bool isInLocation = false;
    private int currentLocationIndex;

    void Start()
    {
        Permission.RequestUserPermission(Permission.FineLocation);
       
        
        for (int index = 0; index < locationList.Count; index++)
        {
            InitializeLocation(index);
        }
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

    void InitializeLocation(int index)
    {
      
        locationList[index].spawnMarker(index);

        UIManager.AddAttractionButton(locationList[index].attractionType, locationList[index].locationName, locationList[index].locationShortDescription, index);

    }


    public void SetDebugText(double distance)
    {

        debugTextDistance.text= distance.ToString();

    }

    public void EnterLocation(int index)
    {
        isInLocation = true;
        currentLocationIndex= index;

        UIManager.EnterLocationSetUI(locationList[currentLocationIndex]);

        //videoManager.SetPlaylist(locationList[currentLocationIndex]);

        audioManager.SetInfoSound(locationList[currentLocationIndex].locationInformationAudio);
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
        videoManager.StopVideo();

    }


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

    public LocationInformation GetLocationInfo(int index)
    {
        return locationList[index];
    }

}
