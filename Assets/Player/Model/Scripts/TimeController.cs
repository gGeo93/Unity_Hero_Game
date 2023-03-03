using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;
    [SerializeField]
    private float afternoonHour;
    [SerializeField]
    private float eveningHour;
    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;
    [SerializeField]
    private Material[] skyboxesMats;
    private DateTime currentTime;

    private TimeSpan sunriseTime;
    private TimeSpan eveningTime;
    private TimeSpan afternoonTime;

    private TimeSpan sunsetTime;

    void OnEnable() => Events.onSunsetArrival += delegate {return IsNight;};

    void OnDisable() => Events.onSunsetArrival -= delegate {return IsNight;};    
    
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        afternoonTime = TimeSpan.FromHours(afternoonHour);
        eveningTime = TimeSpan.FromHours(eveningHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        ChangeDaySky();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
    private bool IsNight => !(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime);
    private void ChangeDaySky()
    {
        if(currentTime.TimeOfDay >= sunriseTime && currentTime.TimeOfDay < afternoonTime)
        {
            RenderSettings.skybox = skyboxesMats[0];
            Events.onSkyboxHeightChanged.Invoke(0);
        }
        else if(currentTime.TimeOfDay >= afternoonTime && currentTime.TimeOfDay < eveningTime)
        {    
            RenderSettings.skybox = skyboxesMats[1];
            Events.onSkyboxHeightChanged.Invoke(0);
        }
        else if(currentTime.TimeOfDay >= eveningTime && currentTime.TimeOfDay < sunsetTime)
        {
            RenderSettings.skybox = skyboxesMats[2];
            Events.onSkyboxHeightChanged.Invoke(-500);
        }
        else
        {
            RenderSettings.skybox = skyboxesMats[3];
            Events.onSkyboxHeightChanged.Invoke(0);
        }
    }
}