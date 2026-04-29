using UnityEngine;

public class WebOpener : MonoBehaviour
{
    public void OpenPage()
    {
        Application.OpenURL("https://digits-and-dragons.s3.us-east-1.amazonaws.com/index.html");
    }
}