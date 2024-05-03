using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Station : MonoBehaviour
{
    public Image _imageLogo;

    private Image _circleImage;

    private StationInfo _info;

    private void Start()
    {
        _circleImage = GetComponent<Image>();
    }
}
