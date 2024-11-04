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
    public SetTargetLocation setTarget;

    [Header("Debug")]
    public TextMeshProUGUI debugTextDistance;

    private bool isInLocation = false;
    private int currentLocationIndex;

    void Start()
    {
        Permission.RequestUserPermission(Permission.FineLocation);
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            
        }
        
        for (int index = 0; index < locationList.Count; index++)
        {
            InitializeLocation(index);
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

        videoManager.SetPlaylist(locationList[currentLocationIndex]);
        
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

    public LocationInformation GetLocationInfo(int index)
    {
        return locationList[index];
    }

}
