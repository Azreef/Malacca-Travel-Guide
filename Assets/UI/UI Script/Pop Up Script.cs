using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class NotificationPop : MonoBehaviour
{
    public float popInDuration = 0.2f;
    public float stayDuration = 2f;
    public float popOutDuration = 0.2f;

    public TextMeshProUGUI editableTopText;
    public TextMeshProUGUI editableBottomText;
    void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public void Show(string topMessage, string bottomMessage)
    {
        transform.localScale = Vector3.zero;
        editableTopText.text = topMessage;
        editableBottomText.text = bottomMessage;

        StartCoroutine(PopRoutine());
    }

    IEnumerator PopRoutine()
    {
        // Pop in
        yield return ScaleTo(Vector3.one, popInDuration);

        // Wait
        yield return new WaitForSecondsRealtime(stayDuration);

        // Pop out
        yield return ScaleTo(Vector3.zero, popOutDuration);

        Destroy(gameObject);
    }

    IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 start = transform.localScale;
        float t = 0f;

        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(start, targetScale, t / duration);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}