//This script is used when player selects a target location to go

using TMPro;
using UnityEngine;


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


            compassScript.SetCompassTarget(currentTargetInfo);

            targetText.SetText("Current Destination: " + currentTargetInfo.locationName);

        }
        else
        {
            Debug.LogError("Location Data Not Found");
        }
    }

}
