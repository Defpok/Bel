using UnityEngine;
using UnityEngine.U2D; // <--- ¬от эту строчку нужно добавить

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    private PixelPerfectCamera pixelPerfectCamera;

    void Start()
    {
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ∆елаема€ позици€ с учетом смещени€
        Vector3 targetPosition = target.position + offset;

        // ѕлавное движение к игроку
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // ѕрив€зываем позицию к пиксельной сетке, чтобы убрать дерганье
        if (pixelPerfectCamera != null)
        {
            smoothedPosition = pixelPerfectCamera.RoundToPixel(smoothedPosition);
        }

        transform.position = smoothedPosition;
    }
}