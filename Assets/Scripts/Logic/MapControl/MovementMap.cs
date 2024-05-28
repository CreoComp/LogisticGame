using TMPro;
using UnityEngine;

public class MovementMap : MonoBehaviour
{
    private Vector2 touchPosition;
    private Vector2 previousTouchPosition;

    private Vector3 mousePosition;
    private Vector3 previousMousePosition;

    private bool isMouseButtonDown = false;
    private bool isTouching = false;

    private float moveSpeed;
    public float zoomSpeed = 2f;
    public float maxZoom = 5f;
    public float minZoom = 1f;
    private float nowZoom;

    public float mapHeight;
    public float mapWidth;
    private Camera cam;

    private bool isFixedCamera = false;

    [SerializeField] private TextMeshProUGUI isFixedCameraText;

    private void Start()
    {
        cam = Camera.main;
        isFixedCameraText.text = "Камера свободна";

        maxZoom = mapWidth / ((float)Screen.width / Screen.height);
    }

    public void FixUnFixCamera()
    {
        isFixedCamera = !isFixedCamera;
        isFixedCameraText.text = isFixedCamera ? "Камера зафиксирована" : "Камера свободна";
    }

    void Update()
    {
        if (!isFixedCamera)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                MobileCameraControl();
            else
                PC_CameraControl();
        }
    }

    private void PC_CameraControl()
    {
        moveSpeed = 0.27f * nowZoom;

        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonDown = true;
            previousMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
        }

        if (isMouseButtonDown)
        {
            mousePosition = Input.mousePosition;

            Vector3 mouseDelta = mousePosition - previousMousePosition;

            transform.Translate(-mouseDelta.x * moveSpeed * Time.deltaTime, -mouseDelta.y * moveSpeed * Time.deltaTime, 0);

            ClampCameraPosition();

            previousMousePosition = mousePosition;
        }

        ClampCameraPosition();

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        nowZoom = Mathf.Clamp(cam.orthographicSize - scrollInput * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        cam.orthographicSize = nowZoom;
    }

    private void MobileCameraControl()
    {
        moveSpeed = 0.1f * nowZoom;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
                previousTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }

            if (isTouching)
            {
                touchPosition = touch.position;

                Vector2 touchDelta = touchPosition - previousTouchPosition;

                transform.Translate(-touchDelta.x * moveSpeed * Time.deltaTime, -touchDelta.y * moveSpeed * Time.deltaTime, 0);

                ClampCameraPosition();

                previousTouchPosition = touchPosition;
            }
        }

        ClampCameraPosition();

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            nowZoom = Mathf.Clamp(Camera.main.orthographicSize - difference * zoomSpeed * 0.002f * Time.deltaTime, minZoom, maxZoom - 0.5f);
            cam.orthographicSize = nowZoom;
        }
    }

    void ClampCameraPosition()
    {
        float zoomLevel = nowZoom;
        var newPosition = transform.position;

        Vector3 worldBottomLeftPoint = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 worldTopRightPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float XValueWillBeChanged = (worldTopRightPoint.x - worldBottomLeftPoint.x) / 2;
        float YValueWillBeChanged = (worldTopRightPoint.y - worldBottomLeftPoint.y) / 2;

        newPosition.x = Mathf.Clamp(transform.position.x, -(mapWidth - XValueWillBeChanged), +(mapWidth - XValueWillBeChanged));
        newPosition.y = Mathf.Clamp(transform.position.y, -(mapHeight - YValueWillBeChanged), +(mapHeight - YValueWillBeChanged));

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
