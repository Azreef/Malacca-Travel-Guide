//This script is used to store the location information

using ARLocation;
using Lean.Touch;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Location Info", menuName = "Add New Location")]
public class LocationInformation : ScriptableObject
{

    [Header("Location Data")]
    public string locationName;
    [Tooltip("This text will be displayed is attraction list")]
    public string locationShortDescription;
    public ARLocation.Location LocationCoodinates;
    public AttractionType attractionType;
    [Tooltip("How close the user need to be in order to trigger the location hotspot")]
    public float hotspotActivationRange = 7f;

    [Tooltip("Disable other marker when user enter this location")]
    public bool disableOtherMarkerOnEnter = false;
    public bool disableThisMarkerOnEnter = true;

    [Header("Location Learning Materials")]
    [Tooltip("This text will be displayed when user is in designated location")]
    public string locationInformationDescription;
    public List<VideoClip> videoClipList;

    [Tooltip("Recommended size is 360x360")]
    public Texture2D locationImage;
    public AudioClip locationInformationAudio;

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
          
        };

        placedMarker = PlaceAtLocation.CreatePlacedInstance(markerPrefab, LocationCoodinates, option);
        MarkerScript markerScript = placedMarker.GetComponent<MarkerScript>();
      
        placedMarker.name = locationName + " Marker";

        markerScript.SetLocationInfo(index);

        markerScript.SetMarkerText(locationName);

        return placedMarker;
    }

    public GameObject CreateModel(Location modelLocation)
    {
        Transform arLocationRoot;
        arLocationRoot = ARLocationManager.Instance.gameObject.transform;

        Camera arCamera;

        arCamera = ARLocationManager.Instance.MainCamera;
        
        placedModel = Instantiate(modelPrefab, arLocationRoot.transform);
       
        placedModel.name = locationName + " Model";

        var transform1 = arCamera.transform;
        var forward = transform1.forward;
        forward.y = 0;
        placedModel.transform.position = transform1.position + forward * 5;

        var groundHeight = placedModel.AddComponent<GroundHeight>();
        placedModel.transform.LookAt(arCamera.transform);

        placedModel.AddComponent<LeanDragTranslate>();
        placedModel.AddComponent<LeanPinchScale>();
        placedModel.AddComponent<LeanTwistRotateAxis>();

        return placedModel;

    }

    public void RemoveModel()
    {
        if (placedModel != null)
        {
            Destroy(placedModel);
        }  
    }

    public GameObject GetPlacedModel()
    {
        return placedModel;
    }

    public GameObject GetPlacedMarker()
    { 
        return placedMarker; 
    }
   
}


