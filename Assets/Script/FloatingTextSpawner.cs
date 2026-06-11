using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner instance;

    [Header("Floating Text Settings")]
    public Transform canvasTransform;
    public float moveUpDistance = 50f;
    public float duration = 1.2f;
    public int fontSize = 28;

    void Awake()
    {
        instance = this;
    }

    public void SpawnText(string message)
    {
        if (canvasTransform == null)
        {
            Debug.LogWarning("Canvas Transform belum diisi di FloatingTextSpawner.");
            return;
        }

        GameObject textObject = new GameObject("FloatingText");
        textObject.transform.SetParent(canvasTransform, false);

        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.text = message;
        text.fontSize = fontSize;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;

        RectTransform rect = text.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(300, 50);

        rect.anchoredPosition = new Vector2(
            Random.Range(-80f, 80f),
            Random.Range(20f, 120f)
        );

        StartCoroutine(AnimateText(textObject, rect, text));
    }

    IEnumerator AnimateText(GameObject textObject, RectTransform rect, TextMeshProUGUI text)
    {
        float timer = 0f;
        Vector2 startPos = rect.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, moveUpDistance);

        Color startColor = text.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / duration;

            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, progress);
            text.color = Color.Lerp(startColor, endColor, progress);

            yield return null;
        }

        Destroy(textObject);
    }
}