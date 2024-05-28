using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Station : MonoBehaviour
{
    public Image _imageLogo;
    private StationInfo _stationInfo;
    private Sprite _circleSprite;

    public void DrawStation(StationInfo stationInfo, Sprite circleSprite, float size = 0.2f)
    {
        _stationInfo = stationInfo;
        _circleSprite = circleSprite;

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

        Debug.Log("logo name" + _imageLogo.name);
        _imageLogo.sprite = Resources.Load<Sprite>("Station/" + _stationInfo.LogoName);

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(_stationInfo.PointInfo.Weight * size, stationInfo.PointInfo.Weight * size); // Размеры изображения
    }
}
