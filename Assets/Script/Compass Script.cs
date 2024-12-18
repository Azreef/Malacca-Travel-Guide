// This script is used when player tries to find a location using attraction. This script calculate direction and distance to the target location

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Video;
using UnityEngine.UIElements.Experimental;
using ARLocation;


public class CompassScript : MonoBehaviour
{

    public struct Coordinate
    {
        public float latitude;
        public float longitude;

    }

    [Header("Arrow Object")]
    [SerializeField]
    public GameObject arrow;
    public GameObject arrow3D;

    [Header("Text")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI compassDebugText;

    float trueNorth = 0;
    float timeDelayTimer = 0f;
    float timeDelaySet = 0.10f;
    float compassAcc = 0;

    [Header("Location Targer")]
    private Coordinate targetLoc;
    public Coordinate currLoc;

    private GameObject currentTargetMarker;

    private float latestAngle;
    private Quaternion gyroRotation;

    IEnumerator Start()
    {
  
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            //Debug.Log("Location not enabled on device or app does not have permission to access location");
           
        }
        // Starts the location service.
        Input.location.Start();
        Input.compass.enabled = true;


        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {  
            //yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
           // Debug.LogError("Unable to determine device location");
          
            //yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            //Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currLoc.latitude = Input.location.lastData.latitude;
        currLoc.longitude = Input.location.lastData.longitude;

        timeDelayTimer -= Time.deltaTime; //update every 1/4 second - reduces jitter, could average instead? 

        if (timeDelayTimer < 0)
        {
            timeDelayTimer =timeDelaySet; //reset timer
            trueNorth = Math.Abs(Input.compass.trueHeading);

            //update UI 
            //arrow3D.transform.localEulerAngles = new Vector3(0, -(float)trueNorth, 0);



            arrow.transform.localEulerAngles = new Vector3(0, 0, (float)trueNorth);

        }

        //gyroRotation = GyroToUnity(Input.gyro.attitude);
        //trueNorth = Math.Abs(Input.compass.trueHeading);
        //double bearing = getBearing(currLoc, targetLoc);
        compassAcc = Input.compass.headingAccuracy;

        double bearing = CalculateBearing(currLoc.latitude, currLoc.longitude, targetLoc.latitude, targetLoc.longitude);


        //calculate the offset angle 
        float waypointDir = (float)bearing - trueNorth;

        arrow.transform.localEulerAngles = new Vector3(0, 0, -waypointDir);
        // arrow3D.transform.localEulerAngles = new Vector3(0, waypointDir, 0);

        if (currentTargetMarker != null)
        {
            //Debug.Log(currentTargetMarker.transform.position);

            Vector3 targetDir = currentTargetMarker.transform.position - Camera.main.transform.position;
            Quaternion lookDir = Quaternion.LookRotation(targetDir);

            //Vector3 directionVector = (currentTargetMarker.transform.position - new Vector3 (0,0,0).normalized);
            arrow3D.transform.rotation = lookDir;
        }
       
        
        compassDebugText.text = "Latitude: " + currLoc.latitude + " | " + "Longitude: " + currLoc.longitude + "\n" + "True North: " + trueNorth + "\n" + "Bearing: " + (float)bearing + "\n" + "Current Target: " + targetLoc.latitude + " | " + targetLoc.longitude + "\n" + "Compass Accuracy: " + compassAcc;
       

        if (distanceText.IsActive())
        {
            distanceText.text = ((distance(currLoc.latitude,currLoc.longitude,targetLoc.latitude,targetLoc.longitude)* 1000).ToString("F0") + "m");

        }
    }

    double getBearing(Coordinate loc1, Coordinate loc2)
    {
        double angle = getBearing(loc1.latitude, loc1.longitude, loc2.latitude, loc2.longitude);
        return angle;
    }


    double getBearing(double lat, double lon, double lat2, double lon2)
    {
        double dy = lat2 - lat;
        double dx = Math.Cos(Math.PI / 180 * lat) * (lon2 - lon);
        double angle = Math.Atan2(dy, dx);

        angle = angle * 180 / Math.PI;
        angle = 360 - Math.Abs(angle);

        return angle;
    }

    public double CalculateBearing(double latitude1, double longitude1, double latitude2, double longitude2)
    {
        // Convert latitude and longitude from degrees to radians
        double lat1 = deg2rad(latitude1);
        double lon1 = deg2rad(longitude1);
        double lat2 = deg2rad(latitude2);
        double lon2 = deg2rad(longitude2);

        // Calculate the difference in longitude
        double deltaLon = lon2 - lon1;

        // Calculate the bearing
        double x = Math.Sin(deltaLon) * Math.Cos(lat2);
        double y = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(deltaLon);

        // Convert the result from radians to degrees
        double bearing = rad2deg(Math.Atan2(x, y));

        // Normalize the bearing to 0-360 degrees
        bearing = (bearing + 360) % 360;

        return bearing;
    }

    double calcBearing(Coordinate loc1, Coordinate loc2)
    {
        double bearing = 0;
        double long1 = loc1.longitude;
        double long2 = loc2.longitude;
        double lat1 = loc1.latitude;
        double lat2 = loc2.latitude;

        double y = Math.Sin(long2 - long1) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(long2 - long1);

        bearing = Math.Atan2(y, x);


        return bearing;
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


    public void SetCompassTarget(LocationInformation targetData)
    {
        Location targetCoordinate = targetData.LocationCoodinates;

        targetLoc.latitude = (float)targetCoordinate.Latitude;
        targetLoc.longitude = (float)targetCoordinate.Longitude;

        Debug.Log("UPDATED LA: " + targetLoc.latitude);
        Debug.Log("UPDATED LON: " + targetLoc.longitude);

        currentTargetMarker = targetData.GetPlacedMarker();
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
