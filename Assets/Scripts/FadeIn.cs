using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] public Image fadePanel;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeInOnStart());
    }

    private IEnumerator FadeInOnStart()
    {
        float elapsed = 0f;

        Color color = fadePanel.color;
        color.a = 1f;
        fadePanel.color = color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            fadePanel.color = color;
            yield return null;
        }
    }
}