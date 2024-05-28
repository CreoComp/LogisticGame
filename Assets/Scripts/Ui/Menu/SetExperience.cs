using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetExperience : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    private void OnEnable()
    {
        SetInfo();
        Experience.Instance.OnChangeExperienceAmount += SetInfo;
    }

    private void OnDisable()
    {
        Experience.Instance.OnChangeExperienceAmount -= SetInfo;
    }

    private void SetInfo()
    {
        SaveLoadService saveLoadService = new SaveLoadService();
        float experienceToUpLevel = 100 + saveLoadService.playerData.Level * 100;
        image.fillAmount = (float)saveLoadService.playerData.Experience / experienceToUpLevel;
        text.text = saveLoadService.playerData.Level + " уровень";
    }
}