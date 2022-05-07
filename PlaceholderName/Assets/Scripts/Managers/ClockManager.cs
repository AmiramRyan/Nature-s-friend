using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockManager : MonoBehaviour
{
    public static Action onTimeChange; //time has changed
    public static Action onDayChange; //day has changed

    #region Static publics
    public static int minute { get; set; }
    public static int hour { get; set; }
    public static int day { get; set; }
    public static string dayText { get; set; }

    #endregion

    #region private vars

    private string[] daysStrings = { "Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"}; //names of the day of the week
    private float minuteToRealTime = 1f; //every sec is in game min
    private bool ticking;

    #endregion

    void Start()
    {
        SetTime(21, 50, 0); // (hour,min,day) dayMap = {0 -> Sunday ,1 -> Monday,2 -> Tuesday,3 -> Wednesday,4 -> Thursday,5 -> Friday,6 -> Saturday}
        ticking = true;
    }

    void Update()
    {
        if (ticking)
        {
            ticking = false;
            StartCoroutine(TickCo());
        }
        /*//for real time
        timer -= Time.deltaTime; 
        if(timer <= 0) //if 1 sec of real time passed 
        {
            minute++; //min will go up
            TimePass(0, 1);//send a signal (IF onMinChange != null)
            timer = minuteToRealTime; //reset the next timer loop
        }*/
    }

    public void TimePass(int addHours, int addMinutes) //move the time forward by the amounts given
    {
        minute += addMinutes;
        if(minute >=60) //minutes overflow to an hour
        {
            minute -= 60;
            hour++;
        }

        hour += addHours;
        if(hour >= 24) //hours overflow to a day
        {
            hour -= 24;
            day++;
            if(day > 6) //week passed
            {
                day = 0; //sunday relapse
            }
            dayText = daysStrings[day];
            onDayChange?.Invoke();
        }
        onTimeChange?.Invoke();
    }

    public void SetTime(int newHour, int newMinute, int newDay)
    {
        minute = newMinute;
        hour = newHour;
        day = newDay;
        dayText = daysStrings[day];
        onTimeChange?.Invoke();
        onDayChange?.Invoke();
    }

    public int GetHour()
    {
        return hour;
    }

    public int GetMin()
    {
        return minute;
    }

    public int GetDay()
    {
        return day;
    }

    public IEnumerator TickCo()
    {
        yield return new WaitForSecondsRealtime(1f);
        TimePass(0, 1);
        ticking = true;
    }
}
