using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{

    public float referenceDistance = 20f;  
    public float minScale = 0.5f;
    public float maxScale = 1f;
    public bool useDynamicScale = true;

    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }


    void FixedUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);

        if (useDynamicScale)
        {
            Vector3 camPos = Camera.main.transform.position;
            Vector3 objPos = transform.position;

            Vector2 camXY = new Vector2(camPos.x, camPos.y);
            Vector2 objXY = new Vector2(objPos.x, objPos.y);
            float horizontalDistance = Vector2.Distance(objXY, camXY);

            float scale = horizontalDistance / referenceDistance;
            scale = Mathf.Clamp(scale, minScale, maxScale);

            transform.localScale = Vector3.one * scale;

        }    
    }
}
