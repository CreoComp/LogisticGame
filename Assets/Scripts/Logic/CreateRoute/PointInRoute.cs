using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointInRoute: MonoBehaviour
{
    private DrawRoute _drawRoute;
    private PointInfo _pointInfo;
    private StationInfo _stationInfo;

    private Sprite _circleSprite;
    public void Construct(DrawRoute drawRoute, PointInfo pointInfo, Sprite circleSprite, float size = 0.2f, bool isLastPoint = false)
    {
        _drawRoute = drawRoute;
        _pointInfo = pointInfo;
        _circleSprite = circleSprite;

        Image imageComponent = gameObject.AddComponent<Image>();
        imageComponent.sprite = _circleSprite;

        if (!isLastPoint)
            imageComponent.color = Color.cyan;
        else
            imageComponent.color = Color.magenta;


        try
        {
            // Добавляем компоненты RectTransform и устанавливаем размеры и положение изображения
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(_pointInfo.Weight * size, _pointInfo.Weight * size); // Размеры изображения
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Data);
        }

        AddEvents();
    }

    public void ConstructStation(DrawRoute drawRoute, StationInfo stationInfo, Sprite circleSprite, float size = 0.2f, bool isLastPoint = false)
    {
        _drawRoute = drawRoute;
        _stationInfo = stationInfo;
        _circleSprite = circleSprite;
        _pointInfo = stationInfo.PointInfo;

        Image imageComponent = gameObject.GetComponent<Image>();
        imageComponent.sprite = _circleSprite;

        switch (_stationInfo.StationType)
        {
            case StationType.Parking:
                imageComponent.color = Color.yellow;
                break;

            case StationType.Storage:
                imageComponent.color = Color.red;
                break;

            case StationType.Store:
                imageComponent.color = Color.green;
                break;

        }

        gameObject.GetComponent<Station>()._imageLogo.sprite = Resources.Load<Sprite>("Station/" + _stationInfo.LogoName);


        // Добавляем компоненты RectTransform и устанавливаем размеры и положение изображения
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(_stationInfo.PointInfo.Weight * size, stationInfo.PointInfo.Weight * size); // Размеры изображения

        AddEvents();
    }

    private void AddEvents()
    {
        // Добавляем компонент EventTrigger
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        // Добавляем слушатель события PointerDown
        EventTrigger.Entry entryPointerDown = new EventTrigger.Entry();
        entryPointerDown.eventID = EventTriggerType.PointerDown;
        entryPointerDown.callback.AddListener((eventData) => { OnPointerDown((PointerEventData)eventData); });
        eventTrigger.triggers.Add(entryPointerDown);

        // Добавляем слушатель события Drag
        EventTrigger.Entry entryDrag = new EventTrigger.Entry();
        entryDrag.eventID = EventTriggerType.Drag;
        entryDrag.callback.AddListener((eventData) => { OnDrag((PointerEventData)eventData); });
        eventTrigger.triggers.Add(entryDrag);

        // Добавляем слушатель события PointerEnter
        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        entryPointerEnter.eventID = EventTriggerType.PointerEnter;
        entryPointerEnter.callback.AddListener((eventData) => { PointerEnter((PointerEventData)eventData); });
        eventTrigger.triggers.Add(entryPointerEnter);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (_stationInfo == null)
            _drawRoute.AddPointToRoute(_pointInfo);
        else
            _drawRoute.AddStationToRoute(_stationInfo);
    }

    // Обработчик события Drag
    public void OnDrag(PointerEventData eventData)
    {
        /*Debug.Log("Dragging on Image!");*/
    }

    public void PointerEnter(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
            return;

        if(_stationInfo == null)
        _drawRoute.AddPointToRoute(_pointInfo);
        else
            _drawRoute.AddStationToRoute(_stationInfo);
    }
}
