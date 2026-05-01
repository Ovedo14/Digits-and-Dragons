using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance { get; private set; }

    [System.Serializable]
    private class CheckUnlockRequest
    {
        public int id_jugador;
        public int id_personaje;
    }

    [System.Serializable]
    private class UnlockRequest
    {
        public int id_jugador;
        public int id_personaje;
    }

    [System.Serializable]
    private class GetRunRequest
    {
        public int id_jugador;
    }

    [System.Serializable]
    private class UpdateRunRequest
    {
        public int id_jugador;
        public int id_personaje;
        public int hp_actual;
        public int oro_actual;
        public int piso_actual;
    }

    [System.Serializable]
    private class EndRunRequest
    {
        public int id_run;
        public int id_jugador;
        public string resultado_final;
        public int oro_recolectado;
    }

    [System.Serializable]
    private class AddRelicRequest
    {
        public int id_run;
        public int id_objeto;
        public int cantidad;
    }
    
    [System.Serializable]
    private class RunData
    {
        public int id_run = -1;
        public int id_jugador = 4;
        public int id_personaje;
        public int hp_actual;
        public int oro_actual;
        public int piso_actual;
    }

    [SerializeField] private int _playerId = 4;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _playerId = PlayerPrefs.GetInt("player_id", 4);
    }

    // --------------- RUN MANAGEMENT ------------

    public void StartNewRun(int characterId, int startingHP, System.Action<int> onSuccess)
    {
        StartCoroutine(StartNewRunCoroutine(characterId, startingHP, onSuccess));
    }

    private IEnumerator StartNewRunCoroutine(int characterId, int startingHP, System.Action<int> onSuccess)
    {
        // Step 1: Create the run
        yield return UpdateRunActivaCoroutine(characterId, startingHP, 0, 0, null);

        // Step 2: Get the id_run that was created
        yield return GetRunActivaCoroutine((runData) => {

            if (runData == null)
            {
                Debug.LogWarning("runData is null. Using fallback runId.");
                onSuccess?.Invoke(-1); // fallback
                return;
            }

            onSuccess?.Invoke(runData.id_run);
        });
    }

    private IEnumerator GetRunActivaCoroutine(System.Action<RunData> callback)
    {
        string url = "https://6ypjc3h6mx2jjttnlmvkjmrxiq0ntkky.lambda-url.us-east-1.on.aws/";

        GetRunRequest data = new GetRunRequest { id_jugador = _playerId };
        string json = JsonUtility.ToJson(data);
        Debug.Log("Sending JSON GetRunID: " + json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            RunData runData = JsonUtility.FromJson<RunData>(request.downloadHandler.text);
            callback?.Invoke(runData);
        }
        else
        {
            Debug.LogError("Failed to get run: " + request.error);
            callback?.Invoke(null);
        }
    }

    public void UpdateRunProgress(int hp, int gold, int floor)
    {
        StartCoroutine(UpdateRunActivaCoroutine(RunManager.Instance.CharacterId, hp, gold, floor, null));
    }

    public void EndRun(string result, int goldCollected, System.Action onSuccess = null)
    {
        StartCoroutine(EndRunCoroutine(result, goldCollected, onSuccess));
    }

    private IEnumerator UpdateRunActivaCoroutine(int characterId, int hp, int gold, int floor, System.Action<int> onSuccess)
    {
        string url = "https://rhy5eoyttsdputsxkczxxc7ibi0tdobv.lambda-url.us-east-1.on.aws/";

        UpdateRunRequest data = new UpdateRunRequest
        {
            id_jugador = _playerId,
            id_personaje = characterId,
            hp_actual = hp,
            oro_actual = gold,
            piso_actual = floor
        };
        string json = JsonUtility.ToJson(data);
        
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Run updated successfully");
            
            // First run creation might return id_run - parse if needed
            if (onSuccess != null)
            {
                // If your endpoint returns id_run, parse it here
                // For now just call success callback
                onSuccess?.Invoke(1); // placeholder id_run
            }
        }
        else
        {
            Debug.LogError("Failed to update run: " + request.error);
        }
    }

    private IEnumerator EndRunCoroutine(string result, int goldCollected, System.Action onSuccess)
    {
        string url = "https://5yjfvwntfcz23gndw7rfaeksoi0phzpq.lambda-url.us-east-1.on.aws/";

        EndRunRequest data = new EndRunRequest
        {
            id_run = RunManager.Instance.RunId,
            id_jugador = _playerId,
            resultado_final = result,
            oro_recolectado = goldCollected
        };
        string json = JsonUtility.ToJson(data);


        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Run ended successfully");
            onSuccess?.Invoke();
        }
        else
        {
            Debug.LogError("Failed to end run: " + request.error);
        }
    }

    // ============ INVENTORY (RELICS) ============

    public void AddRelicToInventory(int relicId)
    {
        StartCoroutine(AddRelicCoroutine(relicId));
    }

    private IEnumerator AddRelicCoroutine(int relicId)
    {
        string url = "https://hxmqvcbaq2xydwm7eqkes3gqdi0blqrp.lambda-url.us-east-1.on.aws/";

        AddRelicRequest data = new AddRelicRequest
        {
            id_run = RunManager.Instance.RunId,
            id_objeto = relicId,
            cantidad = 1
        };
        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Relic added to inventory");
        }
        else
        {
            Debug.LogError("Failed to add relic: " + request.error);
        }
    }

    // ============ CHARACTER UNLOCKS ============

    public void CheckCharacterUnlock(int characterId, System.Action<bool> callback)
    {
        bool localUnlock = PlayerPrefs.GetInt($"char{characterId}_unlocked", 0) == 1;
        
        StartCoroutine(CheckUnlockCoroutine(characterId, (dbUnlock) => {
            callback?.Invoke(dbUnlock || localUnlock);
        }));
    }

        public void UnlockCharacter(int characterId, System.Action onSuccess = null)
    {
        PlayerPrefs.SetInt($"char{characterId}_unlocked", 1);
        PlayerPrefs.Save();

        StartCoroutine(UnlockCharacterCoroutine(characterId, onSuccess));
    }

    private IEnumerator CheckUnlockCoroutine(int characterId, System.Action<bool> callback)
    {
        string url = "https://2eflymvlf6ahgo3chcjkqxs6aa0pvvwz.lambda-url.us-east-1.on.aws/";

        CheckUnlockRequest data = new CheckUnlockRequest
        {
            id_jugador = _playerId,
            id_personaje = characterId
        };
        string json = JsonUtility.ToJson(data);
        Debug.Log("Sending JSON CheckUnlock: " + json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var response = JsonUtility.FromJson<SuccessResponse>(request.downloadHandler.text);
            callback?.Invoke(response.success);
        }
        else
        {
            Debug.LogError("Failed to check unlock: " + request.error);
            callback?.Invoke(true);
        }
    }

    private IEnumerator UnlockCharacterCoroutine(int characterId, System.Action onSuccess)
    {
        string url = "https://xgaly7dahsrcyx3vvxb3s7azsa0ripxm.lambda-url.us-east-1.on.aws/";

        UnlockRequest data = new UnlockRequest
        {
            id_jugador = _playerId,
            id_personaje = characterId
        };
        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Character unlocked!");
            onSuccess?.Invoke();
        }
        else
        {
            Debug.LogError("Failed to unlock character: " + request.error);
        }
    }

    // ============ RESPONSE CLASSES ============

    [System.Serializable]
    private class SuccessResponse
    {
        public bool success;
    }
}