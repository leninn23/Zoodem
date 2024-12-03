using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World;

public class WeatherMenu : MonoBehaviour
{
    public List<Sprite> timeOfDaySprites;
    public List<string> timeOfDayNames;

    [Space(7)] 
    public TextMeshProUGUI dayTxt;
    public TextMeshProUGUI yearTxt;
    public TextMeshProUGUI timeOfDayTxt;
    public TextMeshProUGUI seasonTxt;
    public TextMeshProUGUI temperatureTxt;
    public TextMeshProUGUI timeSpeedTxt;

    [Space(7)] public Image timeOfDayImage;
    
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.onYearChange += SetYearText;
        TimeManager.Instance.onSeasonChange += SetSeasonText;
        TimeManager.Instance.onDayChange += SetDayText;
        
        SetYearText(TimeManager.Instance.YearsPassed);
        SetSeasonText(TimeManager.Instance.currentSeason);
        SetDayText(TimeManager.Instance.DayOfTheYear);

        StartCoroutine(ChangeTimeOfDay());
    }

    private void SetYearText(int year)
    {
        yearTxt.SetText("Year: " + year);
    }
    
    private void SetSeasonText(TimeManager.Season season)
    {
        seasonTxt.SetText("Season: " + TimeManager.Instance.seasonNames[(int)season]);
    }
    
    private void SetDayText(int day)
    {
        dayTxt.SetText("Day: " + day);
    }
    // Update is called once per frame
    void Update()
    {
        var time = TimeManager.Instance.GetNormalisedTime();
        timeOfDayTxt.SetText(Mathf.FloorToInt(time).ToString().PadLeft(2, '0') + ":" + Mathf.FloorToInt((time - Mathf.FloorToInt(time))*60).ToString().PadLeft(2, '0'));
        temperatureTxt.SetText(TimeManager.Instance.Temperature.ToString("0.00") + "º");
    }

    public void ChangeTimeSpeed(float timeSpeed)
    {
        timeSpeedTxt.SetText("Game speed: " + timeSpeed.ToString("0.0"));
        Time.timeScale = timeSpeed;
    }
    
    private IEnumerator ChangeTimeOfDay()
    {
        while (true)
        {
            yield return new WaitUntil((() => TimeManager.Instance.IsBetweenDayAndNigth));
            timeOfDayImage.sprite = timeOfDaySprites[1];
            yield return new WaitUntil((() => !TimeManager.Instance.IsBetweenDayAndNigth));
            timeOfDayImage.sprite = timeOfDaySprites[2];
            yield return new WaitUntil((() => TimeManager.Instance.IsBetweenDayAndNigth));
            timeOfDayImage.sprite = timeOfDaySprites[1];
            yield return new WaitUntil((() => !TimeManager.Instance.IsBetweenDayAndNigth));
            timeOfDayImage.sprite = timeOfDaySprites[0];
        }
    }
}
