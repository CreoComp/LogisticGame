using System;

public class GameTime
{
    public DateTime GetTime => nowTime;
    private DateTime nowTime;

    public Action EndDay;
    public Action<DateTime> NowTime;

    private bool isDayStarted;

    public float TimeMultiplier = 1f;

    public static GameTime Instance
    {
        get
        {
            if(instance == null)
                instance = new GameTime();
            return instance;
        }
    }
    private static GameTime instance;

    public void StartNewDay()
    {
        SaveLoadService saveLoadService = new SaveLoadService();
        nowTime = new DateTime(1,1,1,8,0,0);

        isDayStarted = true;
    }

    public void AddMinute()
    {
        if(isDayStarted)
        {
            nowTime = nowTime.AddMinutes(1);
            NowTime?.Invoke(nowTime);
        }
        CheckNewDay();
    }

    private void CheckNewDay()
    {
        if(nowTime.Hour == 20)
        {
            isDayStarted = false;
            EndDay?.Invoke();
        }
    }


}
