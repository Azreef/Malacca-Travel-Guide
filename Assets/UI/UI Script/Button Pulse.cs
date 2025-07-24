using UnityEngine;

public class ButtonPulseLoop : MonoBehaviour
{
    public float pulseScale = 1.1f;        
    public float pulseSpeed = 1f;          

    private Vector3 originalScale;
    private float timer;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime * pulseSpeed;

        float scale = Mathf.Lerp(1f, pulseScale, (Mathf.Sin(timer * Mathf.PI * 2f) + 1f) / 2f);
        transform.localScale = originalScale * scale;
    }
}