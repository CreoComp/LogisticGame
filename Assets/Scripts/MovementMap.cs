using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementMap : MonoBehaviour
{
    // Переменные для хранения позиции касания и предыдущей позиции
    private Vector2 touchPosition;
    private Vector2 previousTouchPosition;


    private Vector3 mousePosition;
    private Vector3 previousMousePosition;

    // Переменная для определения, была ли нажата левая кнопка мыши
    private bool isMouseButtonDown = false;

    private float moveSpeed;
    // Скорость приближения и отдаления камеры
    public float zoomSpeed = 2f;
    // Максимальный и минимальный зум
    public float maxZoom = 5f;
    public float minZoom = 1f;

    private float nowZoom;

    // Границы карты
    public float mapHeight;
    public float mapWidth;
    private Camera cam;

    private bool isFixedCamera = false;

    [SerializeField] private TextMeshProUGUI isFixedCameraText;
    private void Start()
    {
        cam = Camera.main;
        isFixedCameraText.text = "Камера свободна";
    }

    public void FixUnFixCamera()
    {
        if(isFixedCamera)
        {
            isFixedCamera = false;
            isFixedCameraText.text = "Камера свободна";
        }
        else
        {
            isFixedCamera = true;
            isFixedCameraText.text = "Камера зафиксирована";
        }
    }

    void Update()
    {
        if (!isFixedCamera)
            CameraControl();
    }

    private void CameraControl()
    {
        moveSpeed = 0.27f * nowZoom;
        // Обработка касаний на экране
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
                previousTouchPosition = touchPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchPosition = touch.position;

                Vector2 touchDelta = touchPosition - previousTouchPosition;

                transform.Translate(-touchDelta.x * moveSpeed * Time.deltaTime, 0, -touchDelta.y * moveSpeed * Time.deltaTime);

                // Проверка и коррекция позиции камеры
                ClampCameraPosition();

                previousTouchPosition = touchPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float zoomAmount = touch.deltaPosition.y * zoomSpeed * Time.deltaTime;

                // Ограничение масштаба камеры
                nowZoom = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount, minZoom, maxZoom);
                cam.orthographicSize = nowZoom;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMouseButtonDown = true;
                previousMousePosition = Input.mousePosition;
            }
            // Проверка, была ли отпущена левая кнопка мыши
            else if (Input.GetMouseButtonUp(0))
            {
                isMouseButtonDown = false;
            }

            // Если левая кнопка мыши зажата, обрабатываем перемещение камеры
            if (isMouseButtonDown)
            {
                mousePosition = Input.mousePosition;

                Vector3 mouseDelta = mousePosition - previousMousePosition;

                transform.Translate(-mouseDelta.x * moveSpeed * Time.deltaTime, -mouseDelta.y * moveSpeed * Time.deltaTime, 0);

                // Проверка и коррекция позиции камеры
                ClampCameraPosition();

                previousMousePosition = mousePosition;
            }

            // Проверка и коррекция позиции камеры
            ClampCameraPosition();

            // Управление приближением и отдалением
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            nowZoom = Mathf.Clamp(Camera.main.orthographicSize - scrollInput * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
            cam.orthographicSize = nowZoom;
        }
    }

    // Проверка и коррекция позиции камеры
    void ClampCameraPosition()
    {
        float zoomLevel = nowZoom;
        // Получаем текущую позицию камеры
        var newPosition = transform.position;

        Vector3 worldBotoomLeftPoint = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 worldTopRightPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float XValueWillBeChanged = (worldTopRightPoint.x - worldBotoomLeftPoint.x) / 2;
        float YValueWillBeChanged = (worldTopRightPoint.y - worldBotoomLeftPoint.y) / 2;

        // Ограничиваем позицию камеры по горизонтали с учетом масштабирования
        newPosition.x = Mathf.Clamp(transform.position.x, -(mapWidth - XValueWillBeChanged), +(mapWidth - XValueWillBeChanged));
        // Ограничиваем позицию камеры по вертикали с учетом масштабирования
        newPosition.y = Mathf.Clamp(transform.position.y, -(mapHeight - YValueWillBeChanged), +(mapHeight - YValueWillBeChanged));

        // Устанавливаем новую позицию камеры
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

    }
}
