//This script is used when player selects a target location to go

using ARLocation;
using ARLocation.MapboxRoutes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class SetTargetLocation : MonoBehaviour
{
    public LocationManagerScript locationManager;
    
    public CompassScript compassScript;
    private LocationInformation currentTargetInfo;

    public UIManager UIManager;
    public TextMeshProUGUI targetText;

    public void setTarget(int locationIndex)
    {
        bool locationFound = false;

        if (locationManager.locationList[locationIndex] != null) 
        {
            locationFound = true;
            currentTargetInfo = (locationManager.locationList[locationIndex]);
        }

        if (locationFound)
        {
            UIManager.HideAllCanvas();
            UIManager.ShowARCanvas();
            UIManager.ToggleNavigationPanel(true);


            compassScript.SetCompassTarget(currentTargetInfo.LocationCoodinates);

            targetText.SetText(currentTargetInfo.locationName);

        }
        else
        {
            Debug.LogError("Location Data Not Found");
        }


        

    }


    //---------------------------------------- OLD MAPBOX 

    /* [SerializeField] public MapboxRoute targetAFamosa;
    [SerializeField] public MapboxRoute targetIstana;
    [SerializeField] public MapboxRoute targetSaintPaulChurch;
    [SerializeField] public TextMeshProUGUI targetText;
    [SerializeField] public UIManager UIManager;

    //[SerializeField] public GameObject currentRouteObject;
    [SerializeField] public MapboxRoute currentRoute;


   *//* public void ResetTargetLocation()
    {

       *//* targetAFamosa.clearRoute();
        targetIstana.clearRoute();
        targetSaintPaulChurch.clearRoute();*//*
        // currentRoute.ReloadRoute();

        *//* targetAFamosa.SetActive(false);
        targetIstana.SetActive(false);
        targetSaintPaulChurch.SetActive(false);
        targetText.SetText("");*//*

    }

    public void setTarget(LocationEnum target)
    {

        *//*ARLocationManager.Instance.ResetARSession((() =>
        {
            Debug.Log("AR+GPS and AR Session were restarted!");
        }));*//*

        currentRoute.clearRoute();
        currentRoute.StopAllCoroutines();


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Navigation");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);

        //Destroy(currentRoute.transform);

        UIManager.ShowARCanvas();
        //ResetTargetLocation();
        //currentRoute.clearRoute();

        if (target.selectedTarget == LocationEnum.targetList.AFamosa)
        {


            currentRoute = Instantiate(targetAFamosa, new Vector3(0, 0, 0), Quaternion.identity);
            StartCoroutine(currentRoute.LoadRoute());
            //Instantiate(targetAFamosa, new Vector3(0, 0, 0), Quaternion.identity);


            //targetAFamosa.LoadRoute();
            targetText.SetText("A'Famosa Fort");
            //currentRoute = targetAFamosa;


            //StartCoroutine(currentRoute.LoadRoute());
            //StartCoroutine(targetAFamosa.LoadRoute());

            Debug.Log("LOADED");
        }
        else if (target.selectedTarget == LocationEnum.targetList.istana)
        {

            //StartCoroutine(Instantiate(targetIstana, new Vector3(0, 0, 0), Quaternion.identity).LoadRoute());

            currentRoute = Instantiate(targetIstana, new Vector3(0, 0, 0), Quaternion.identity);
            StartCoroutine(currentRoute.LoadRoute());


            //targetIstana.LoadRoute();

            //currentRoute = targetIstana;

            //StartCoroutine(currentRoute.LoadRoute());
            targetText.SetText("Melaka Sultanate Palace Museum");
            Debug.Log("LOADED");
        }
        else if (target.selectedTarget == LocationEnum.targetList.saintPaulChurch)
        {


            currentRoute = Instantiate(targetSaintPaulChurch, new Vector3(0, 0, 0), Quaternion.identity);
            StartCoroutine(currentRoute.LoadRoute());
            //Instantiate(targetSaintPaulChurch, new Vector3(0, 0, 0), Quaternion.identity);
            targetText.SetText("Church of Saint Paul");
            //currentRoute = targetSaintPaulChurch;

            //StartCoroutine(currentRoute.LoadRoute());


            //Debug.Log("LOADED");
            //targetSaintPaulChurch.LoadRoute();


        }
        //StartCoroutine(currentRoute.ReloadRoute());
        //currentRoute.ReloadRoute();
    }

    private void Start()
    {
        //
    }*/

}
