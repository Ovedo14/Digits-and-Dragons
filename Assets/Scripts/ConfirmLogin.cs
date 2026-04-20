using UnityEngine;

public class ConfirmLogin : MonoBehaviour
{
    [SerializeField] public GameObject MainCanvas;
    [SerializeField] public GameObject LoginScreen;

    public void SendLogin()
    {
        LoginScreen.SetActive(false);
        MainCanvas.SetActive(true);
    }

}