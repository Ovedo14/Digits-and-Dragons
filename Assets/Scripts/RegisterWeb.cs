using UnityEngine;

public class WebOpener : MonoBehaviour
{
    public void OpenPage()
    {
        Application.OpenURL("https://www.google.com");
    }
}