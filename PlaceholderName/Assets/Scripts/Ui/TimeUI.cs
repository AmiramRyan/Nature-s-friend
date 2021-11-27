using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI dayText;

    private void OnEnable()
    {
        //subscribe to those events and invoke the UpdateTime function when needed
        ClockManager.onTimeChange += UpdateTime;
        ClockManager.onDayChange += UpdateDay;
    }

    private void OnDisable()
    {
        //unsubscribe
        ClockManager.onTimeChange -= UpdateTime;
        ClockManager.onDayChange -= UpdateDay;
    }

    private void UpdateTime()
    {
        clockText.text = $"{ClockManager.hour:00}:{ClockManager.minute:00}";
    }

    private void UpdateDay()
    {
        dayText.text = ClockManager.dayText;
    }
}
