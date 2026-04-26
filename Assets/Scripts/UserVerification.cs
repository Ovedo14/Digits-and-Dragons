using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
using TMPro.Examples;
using UnityEditor.PackageManager;
using TMPro;

public class UserVerification : MonoBehaviour
{
    [SerializeField] public TMP_InputField usernameInput;
    [SerializeField] public TMP_InputField passwordInput;
    [SerializeField] public GameObject errorMessage;
    [SerializeField] private SceneLoader sceneLoader;

    private string url = "https://xuofr6tvbbaa5fjag25qnx4kka0sxjgr.lambda-url.us-east-1.on.aws/";

    [System.Serializable]
    public class LoginData
    {
        public string username;
        public string password;
    }

    [System.Serializable]
    public class Usuario
    {
        public int id;
        public string nombre;
        public string rol;
    }

    [System.Serializable]
    public class ResponseData
    {
        public bool success;
        public string token;
        public Usuario usuario;
    }

    public void SendLogin()
    {
        StartCoroutine(LoginCoroutine());
    }

    IEnumerator LoginCoroutine()
    {
        LoginData loginData = new LoginData
        {
            username = usernameInput.text,
            password = passwordInput.text
        };

        string json = JsonUtility.ToJson(loginData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        string responseText = request.downloadHandler.text;
        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log("Raw Response: " + responseText);

        ResponseData response = null;

        try
        {
            response = JsonUtility.FromJson<ResponseData>(responseText);
        }
        catch
        {
            Debug.LogError("JSON error");
        }

        if (response != null && response.success)
        {
            Debug.Log("Login successful!");
            sceneLoader.FadeAndLoadScene("Menu");
        }
        else
        {
            Debug.LogWarning("Login failed.");
            errorMessage.SetActive(true);

            if (response != null)
            {
                Debug.LogWarning("Backend says: " + responseText);
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
            }
        }
    }
}