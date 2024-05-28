using UnityEngine;

public class DayTimer: MonoBehaviour
{
    private float time;

    public float TimeMultiplier = 2f;

    private void Update()
    {
        GameTime.Instance.TimeMultiplier = TimeMultiplier;
        if(time <= 1)
        {
            time += Time.deltaTime * TimeMultiplier;
        }
        else
        {
            time = 0;
            GameTime.Instance.AddMinute();
        }
    }
}
