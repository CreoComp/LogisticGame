using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementMap : MonoBehaviour
{
    // ���������� ��� �������� ������� ������� � ���������� �������
    private Vector2 touchPosition;
    private Vector2 previousTouchPosition;


    private Vector3 mousePosition;
    private Vector3 previousMousePosition;

    // ���������� ��� �����������, ���� �� ������ ����� ������ ����
    private bool isMouseButtonDown = false;

    private float moveSpeed;
    // �������� ����������� � ��������� ������
    public float zoomSpeed = 2f;
    // ������������ � ����������� ���
    public float maxZoom = 5f;
    public float minZoom = 1f;

    private float nowZoom;

    // ������� �����
    public float mapHeight;
    public float mapWidth;
    private Camera cam;

    private bool isFixedCamera = false;

    [SerializeField] private TextMeshProUGUI isFixedCameraText;
    private void Start()
    {
        cam = Camera.main;
        isFixedCameraText.text = "������ ��������";
    }

    public void FixUnFixCamera()
    {
        if(isFixedCamera)
        {
            isFixedCamera = false;
            isFixedCameraText.text = "������ ��������";
        }
        else
        {
            isFixedCamera = true;
            isFixedCameraText.text = "������ �������������";
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
        // ��������� ������� �� ������
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

                // �������� � ��������� ������� ������
                ClampCameraPosition();

                previousTouchPosition = touchPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float zoomAmount = touch.deltaPosition.y * zoomSpeed * Time.deltaTime;

                // ����������� �������� ������
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
            // ��������, ���� �� �������� ����� ������ ����
            else if (Input.GetMouseButtonUp(0))
            {
                isMouseButtonDown = false;
            }

            // ���� ����� ������ ���� ������, ������������ ����������� ������
            if (isMouseButtonDown)
            {
                mousePosition = Input.mousePosition;

                Vector3 mouseDelta = mousePosition - previousMousePosition;

                transform.Translate(-mouseDelta.x * moveSpeed * Time.deltaTime, -mouseDelta.y * moveSpeed * Time.deltaTime, 0);

                // �������� � ��������� ������� ������
                ClampCameraPosition();

                previousMousePosition = mousePosition;
            }

            // �������� � ��������� ������� ������
            ClampCameraPosition();

            // ���������� ������������ � ����������
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            nowZoom = Mathf.Clamp(Camera.main.orthographicSize - scrollInput * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
            cam.orthographicSize = nowZoom;
        }
    }

    // �������� � ��������� ������� ������
    void ClampCameraPosition()
    {
        float zoomLevel = nowZoom;
        // �������� ������� ������� ������
        var newPosition = transform.position;

        Vector3 worldBotoomLeftPoint = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 worldTopRightPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float XValueWillBeChanged = (worldTopRightPoint.x - worldBotoomLeftPoint.x) / 2;
        float YValueWillBeChanged = (worldTopRightPoint.y - worldBotoomLeftPoint.y) / 2;

        // ������������ ������� ������ �� ����������� � ������ ���������������
        newPosition.x = Mathf.Clamp(transform.position.x, -(mapWidth - XValueWillBeChanged), +(mapWidth - XValueWillBeChanged));
        // ������������ ������� ������ �� ��������� � ������ ���������������
        newPosition.y = Mathf.Clamp(transform.position.y, -(mapHeight - YValueWillBeChanged), +(mapHeight - YValueWillBeChanged));

        // ������������� ����� ������� ������
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

    }
}
