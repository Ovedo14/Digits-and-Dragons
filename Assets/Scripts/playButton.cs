using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class playButton : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _fader;

    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        var root = _uiDocument.rootVisualElement;

        // Buscamos el Fader y el Botón por su "Name" del UI Builder
        _fader = root.Q<VisualElement>("Fader");
        Button btn = root.Q<Button>("play");

        if (btn != null)
        {
            btn.clicked += () => StartCoroutine(FadeAndLoad());
        }
        else
        {
            Debug.LogError("No encontré el botón 'Play'. Revisa el Name en UI Builder.");
        }
    }

    IEnumerator FadeAndLoad()
    {
        if (_fader != null)
        {
            // Hacemos que el fader bloquee clics para evitar doble Play
            _fader.pickingMode = PickingMode.Position;

            float opacity = 0;
            while (opacity < 1.0f)
            {
                opacity += Time.deltaTime * 1.2f; // Velocidad del fundido
                _fader.style.opacity = opacity;
                yield return null;
            }
        }

        // Cargamos la escena cuando la pantalla ya esté negra
        SceneManager.LoadScene("MainScene");
    }
}