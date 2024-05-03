using System;

public class GameTime
{
    public DateTime NowTime => nowTime;
    private DateTime nowTime;

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

    public void AddMinute()
    {
        nowTime.AddMinutes(1);
    }
}
