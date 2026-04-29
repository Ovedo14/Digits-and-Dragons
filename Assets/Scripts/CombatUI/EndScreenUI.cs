using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeInDuration = 0.8f;
    [SerializeField] private string _mainMenuSceneName = "CharSelectScene";

    void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        _canvasGroup.alpha = 0f;
        float elapsed = 0f;

        while (elapsed < _fadeInDuration)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / _fadeInDuration);
            yield return null;
        }

        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
    }

    public void ReturnToMainMenu()
    {
        //destroy RunManager so the run resets
        if (RunManager.Instance != null)
            Destroy(RunManager.Instance.gameObject);

        AudioManager.Instance.PlayMenuMusic();
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}