using System;
using System.Collections.Generic;
using BehaviourAPI.Core.Actions;
using UnityEngine;
using UnityEngine.Serialization;
using Action = System.Action;
using Random = UnityEngine.Random;

namespace World
{
    [DefaultExecutionOrder(-200)]
    public class TimeManager : MonoBehaviour
    {
        public int DayOfTheYear { private set; get; }
        public int YearsPassed { private set; get; }
        public const int DaysPerSeason = 10;
        public float TimeOfTheDay { private set; get; }
        public float Temperature { private set; get; }
        /// <summary>
        /// Real time of each day. Half of it will be night and half day
        /// </summary>
        public const float LenghtOfDay = 30f; 
        public const float StartOfDaylight = LenghtOfDay*6/24;
        public const float EndOfDaylight = LenghtOfDay*22/24;
        
        public bool IsDay => TimeOfTheDay is >= StartOfDaylight and <= EndOfDaylight;
        private const float MorningDuration = LenghtOfDay*2/24f;
        public bool IsBetweenDayAndNigth => (TimeOfTheDay is >= StartOfDaylight - MorningDuration / 2f
            and <= StartOfDaylight + MorningDuration / 2f or >= EndOfDaylight - MorningDuration / 2f
            and <= EndOfDaylight + MorningDuration / 2f);
        private bool _temperatureUpdateFlag;

        private static readonly List<Vector2> k_SeasonTemperatures = new List<Vector2>() { new Vector2(-10f, 8f), new Vector2(8f, 20f),new Vector2(25f, 35f), new Vector2(8f, 20f) };
        private const float NightTemperatureOffset = 1.5f;
        private Light _light;
        public string[] seasonNames = new[] { "Winter", "Spring", "Summer", "Autumn" };
        public static TimeManager Instance;
        public Season currentSeason;
    
        //Public Events
        public Action<int> onDayChange;
        public Action<int> onYearChange;
        public Action<Season> onSeasonChange;
        public Action onTimeOfDayChange;


        public float GetNormalisedTime()
        {
            return TimeOfTheDay / LenghtOfDay * 24;
        }
        public enum Season
        {
            Winter = 0,
            Spring = 1,
            Summer = 2,
            Autumn = 3,
        }
    
        // Start is called before the first frame update
        void Start()
        {
            DayOfTheYear = 9;
            if (Instance) Destroy(gameObject);
            else Instance = this;
        
            _light = FindObjectOfType<Light>();
            //Time.timeScale = 10f;
            UpdateTemperature();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTime();
        }

        // private void OnGUI()
        // {
        //     GUILayout.BeginArea(new Rect(10, 10, 1000, 500));
        //     GUILayout.Label("Year: " + YearsPassed);
        //     GUILayout.Label("Season: " + seasonNames[DayOfTheYear / DaysPerSeason]);
        //     GUILayout.Label("Day: " + DayOfTheYear);
        //     var time = TimeOfTheDay / LenghtOfDay * 24F;
        //     GUILayout.Label("Time: " + Mathf.FloorToInt(time) + ":" + Mathf.FloorToInt((time - Mathf.FloorToInt(time))*60));
        //     GUILayout.Label(IsDay ? "DAY" : "NIGHT");
        //     GUILayout.Label("Temperature: " + Temperature);
        //     GUILayout.EndArea();
        // }

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

        // private IEnumerator TemperatureChanges()
        // {
        //     while (true)
        //     {
        //         yield return new WaitUntil(da);
        //     }
        // }
    
    
        private void UpdateTime()
        {
            TimeOfTheDay += Time.deltaTime;
            //While to ensure days work while in high game speeds (edge case that shouldn't happen)
            while (TimeOfTheDay >= LenghtOfDay)
            {
                TimeOfTheDay -= LenghtOfDay;
                var prevSeason = (Season)(DayOfTheYear / DaysPerSeason);
                DayOfTheYear++;
                onDayChange?.Invoke(DayOfTheYear);
                currentSeason = (Season)(DayOfTheYear / DaysPerSeason);
                if (prevSeason != currentSeason)
                {
                    onSeasonChange?.Invoke(currentSeason);
                }
                // UpdateTemperature();
            }
            //There are 4 months
            if (DayOfTheYear > DaysPerSeason * 4)
            {
                //In case in a high game speed it skips 2 days (??)
                DayOfTheYear -= DaysPerSeason * 4;
                YearsPassed++;
                onDayChange?.Invoke(DayOfTheYear);
                onYearChange.Invoke(YearsPassed);
            }

            if (IsDay)
            {
                if (!_temperatureUpdateFlag)
                {
                    _temperatureUpdateFlag = true;
                    UpdateTemperature();
                     _light.color = new Color(0.93f, 0.91f, 0.7f);
                     onTimeOfDayChange?.Invoke();
                }
            }
            else
            {   
                if (_temperatureUpdateFlag)
                {
                    _temperatureUpdateFlag = false;
                    UpdateTemperature();
                    _light.color = new Color(0.64f, 0.69f, 0.93f);
                    onTimeOfDayChange?.Invoke();
                }
            }
            //TODO: Manage stuff related to change between day and night, and clocks/calendars
        }
    }
}
