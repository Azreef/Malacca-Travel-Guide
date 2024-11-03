//This script is used to store the location information

using ARLocation;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Location Info", menuName = "Add New Location")]
public class LocationInformation : ScriptableObject
{

    [Header("Location Data")]
    public string locationName;
    public ARLocation.Location LocationCoodinates;
    public AttractionType attractionType;

    [Header("Location Learning Materials")]
    public string locationDescription;
    public List<VideoClip> videoClipList;

    [Header("Prefabs")]
    public GameObject modelPrefab;
    public GameObject markerPrefab;

    private GameObject placedModel;
    private GameObject placedMarker;

    public GameObject spawnMarker(int index)
    {
        var option = new PlaceAtLocation.PlaceAtOptions()
        {
            HideObjectUntilItIsPlaced = false,
            MaxNumberOfLocationUpdates = 10,
            MovementSmoothing = 10,
            UseMovingAverage = true
        };

        placedMarker = PlaceAtLocation.CreatePlacedInstance(markerPrefab, LocationCoodinates, option);
        MarkerScript markerScript = placedMarker.GetComponent<MarkerScript>();
        

        placedMarker.name = locationName + " Marker";

        markerScript.setLocationInfo(index);

        markerScript.SetMarkerText(locationName);

        return placedMarker;
    }

    public GameObject CreateModel(Location modelLocation)
    {
        Transform arLocationRoot;
        arLocationRoot = ARLocationManager.Instance.gameObject.transform;

        Camera arCamera;

         arCamera = ARLocationManager.Instance.MainCamera;
        /* var option = new PlaceAtLocation.PlaceAtOptions()
         {
             HideObjectUntilItIsPlaced = false,
             MaxNumberOfLocationUpdates = 2,
             MovementSmoothing = 0.1f,
             UseMovingAverage = false
         };

         placedModel = PlaceAtLocation.CreatePlacedInstance(modelPrefab, modelLocation, option);*/

        placedModel = Instantiate(modelPrefab, arLocationRoot.transform);
        /*Instantiate(modelPrefab
                   HotspotSettings.Prefab,
                   HotspotSettings.AlignToCamera ? transform : arLocationRoot);*/

        placedModel.name = locationName + " Model";


        /*var transform1 = arCamera.transform;
        var forward = transform1.forward;
        forward.y = 0;
        placedModel.transform.position = transform1.position + forward * 5;

        */

        var transform1 = arCamera.transform;
        var forward = transform1.forward;
        forward.y = 0;
        placedModel.transform.position = transform1.position + forward * 5;

        var groundHeight = placedModel.AddComponent<GroundHeight>();
        placedModel.transform.LookAt(arCamera.transform);



        return placedModel;

    }

    public void RemoveModel()
    {
        Destroy(placedModel);
    }

    public GameObject GetPlacedModel()
    {
        return placedModel;
    }

   

}


