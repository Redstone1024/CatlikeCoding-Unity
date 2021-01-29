using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    const float hoursToDegrees = -30.0f;
    const float minutesToDegrees = -6.0f;
    const float secondsToDegrees = -6.0f;

    [SerializeField]
    Transform hoursPivot = default;
    
    [SerializeField]
    Transform minutesPivot = default;

    [SerializeField]
    Transform secondsPivot = default;

    private void Update()
    {
        var time = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0.0f, 0.0f, hoursToDegrees * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0.0f, 0.0f, minutesToDegrees * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0.0f, 0.0f, secondsToDegrees * (float)time.TotalSeconds);
    }
}
