using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeManaegr : MonoBehaviour
{
    public int DayOfTheYear { private set; get; }
    public int YearsPassed { private set; get; }
    public const int DaysPerSeason = 10;
    public float TimeOfTheDay { private set; get; }
    public float Temperature { private set; get; }
    /// <summary>
    /// Real time of each day. Half of it will be night and half day
    /// </summary>
    public const float LenghtOfDay = 4 * 60f; 
    public const float StartOfDaylight = LenghtOfDay*6/24;
    public const float EndOfDaylight = LenghtOfDay*22/24;
    public bool IsDay => TimeOfTheDay is >= StartOfDaylight and <= EndOfDaylight;

    private static readonly List<Vector2> k_SeasonTemperatures = new List<Vector2>() { new Vector2(-10f, 8f), new Vector2(8f, 20f),new Vector2(25f, 35f), new Vector2(8f, 20f) };
    private const float NightTemperatureOffset = 1.5f;
    private Light _light;
    public enum Season
    {
        Winter,
        Spring,
        Summer,
        Autumn,
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _light = FindObjectOfType<Light>();
        Time.timeScale = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 1000, 500));
        GUILayout.Label("Year: " + YearsPassed);
        GUILayout.Label("Season: " + DayOfTheYear / DaysPerSeason);
        GUILayout.Label("Day: " + DayOfTheYear);
        var time = TimeOfTheDay / LenghtOfDay * 24F;
        GUILayout.Label("Time: " + Mathf.FloorToInt(time) + ":" + Mathf.FloorToInt((time - Mathf.FloorToInt(time))*60));
        GUILayout.Label(IsDay ? "DAY" : "NIGHT");
        GUILayout.Label("Temperature: " + Temperature);
        GUILayout.EndArea();
    }

    private void UpdateTemperature()
    {
        var index = (DayOfTheYear) / DaysPerSeason;
        // Debug.Log(index);
        Temperature = Random.Range(k_SeasonTemperatures[index].x, k_SeasonTemperatures[index].y);
        if (!IsDay)
        {
            Temperature -= Random.Range(0, NightTemperatureOffset);
        }
    }
    
    
    
    
    private void UpdateTime()
    {
        TimeOfTheDay += Time.deltaTime;
        //While to ensure days work while in high game speeds (edge case that shouldn't happen)
        while (TimeOfTheDay >= LenghtOfDay)
        {
            TimeOfTheDay -= LenghtOfDay;
            DayOfTheYear++;
            UpdateTime();
        }
        //There are 4 months
        if (DayOfTheYear > DaysPerSeason * 4)
        {
            DayOfTheYear -= DaysPerSeason * 4;
            YearsPassed++;
            UpdateTemperature();
        }

        if (IsDay)
        {
            _light.color = new Color(0.93f, 0.91f, 0.7f);
        }
        else
        {
            _light.color = new Color(0.64f, 0.69f, 0.93f);
        }
        //TODO: Manage stuff related to change between day and night, and clocks/calendars
    }
}
