using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public GameObject locationNotificationPrefab;  
    public Transform canvasParent;        
    private AudioManager audioManager;

    public void SpawnLocationNotification(string topMessage, string bottomMessage, bool isEnterLocation)
    {
        GameObject popup = Instantiate(locationNotificationPrefab, canvasParent);
        popup.transform.localScale = Vector3.one; // Ensure correct scale

        NotificationPop notifier = popup.GetComponent<NotificationPop>();
        if (notifier != null)
        {
            notifier.Show(topMessage, bottomMessage);
            if (isEnterLocation)
            {
                audioManager.PlayArrivingSound();
            }
            else
            {
                audioManager.PlayExitSound();
            }        
        }
    }

    // Example call
    void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }
}