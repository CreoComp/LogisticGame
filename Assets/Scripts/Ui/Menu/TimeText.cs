using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeText: MonoBehaviour
{
    private TextMeshProUGUI text;
    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        SetTime(GameTime.Instance.GetTime);
        GameTime.Instance.NowTime += SetTime;
    }

    private void OnDisable()
    {
        GameTime.Instance.NowTime -= SetTime;
    }

    private void SetTime(DateTime dateTime)
    {
        text.text = dateTime.ToShortTimeString();
    }
}
