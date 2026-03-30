using UnityEngine;

public class ParallaxMenu : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private float textureSize;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        textureSize = 19.2f;

    }

    void LateUpdate()
    {
        // Mover fondo
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // Checar si se reinicia el bucle de movimiento
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureSize)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureSize;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}