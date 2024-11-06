//This script is used to set marker's properties. It is also used to detect if player is currently in a location zone.

using ARLocation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class MarkerScript : MonoBehaviour
{
    public string defaultText;
    public TextMeshPro markerText;
    public LocationInformation locationInformation;
    public LocationManagerScript locationManager;
    //public BoxCollider markerCollider;

    private int locationIndex;
    private bool triggeredInLocation = false;

    public float activationRange = 7f;
    public void SetMarkerText(string inputText)
    {
        markerText.text = inputText;  
    }

   

    public void SetNavigation()
    {
        locationManager = GameObject.FindWithTag("Manager").GetComponent<LocationManagerScript>();

        locationManager.GetComponent<SetTargetLocation>().setTarget(locationIndex);
    }


    public void SetLocationInfo(int index)
    {
        locationManager = GameObject.FindWithTag("Manager").GetComponent<LocationManagerScript>();

        locationInformation = locationManager.GetLocationInfo(index);
       

        locationIndex = index;
    }

    private void FixedUpdate()
    {
        double currentLatitude = Input.location.lastData.latitude;
        double currentLongitude = Input.location.lastData.longitude;



        double currentDistance = distance(locationInformation.LocationCoodinates.Latitude, locationInformation.LocationCoodinates.Longitude, currentLatitude, currentLongitude);

        currentDistance = currentDistance * 1000;
       if(currentDistance < activationRange && !triggeredInLocation)
        {
            triggeredInLocation = true;
            locationManager.EnterLocation(locationIndex);


        }
       else if (currentDistance >= activationRange && triggeredInLocation)
        {
            triggeredInLocation = false;
            locationManager.ExitLocation();
            
        }


        locationManager.SetDebugText(currentDistance);

    }


    private double distance(double lat1, double lon1, double lat2, double lon2)
    {

        double theta = lon1 - lon2;
        double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;

        dist = dist * 1.609344;

        return (dist);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts decimal degrees to radians             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts radians to decimal degrees             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }


}
