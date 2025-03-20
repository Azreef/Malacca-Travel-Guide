using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttractionButtonScript : MonoBehaviour
{
    public TextMeshProUGUI locationNameText;
    public TextMeshProUGUI rotationDescriptionText;
    public RawImage locationImageFrame;
    private int locationIndex;
    public SetTargetLocation setTargetManager;

    private void Start()
    {
        setTargetManager = GameObject.FindWithTag("Manager").GetComponent<SetTargetLocation>();
    }
    public void setLocationName(string name)
    {
        locationNameText.text = name;
    }

    public void setLocationDescription(string description)
    {
        rotationDescriptionText.text = description;
    }

    public void setLocationImage(Texture2D locationImage)
    {
        locationImageFrame.texture = locationImage;
    }
    public void setLocationIndex(int index)
    {
        locationIndex = index;

    }

    public void setTargetButtonClicked()
    {
        setTargetManager.setTarget(locationIndex);
    }
}
