using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class TimeReader : MonoBehaviour
{
    private TimeManager _manager;
    // public float[] seasonValues;
    // public V hour;

    private void Start()
    {
        _manager = TimeManager.Instance;;
        // if (seasonValues.Length != 4)
        // {
        //     Debug.LogError("No values were provided for season values for animal " + name + " at script TimeReader");
        //     seasonValues = new float[] { 0, 0, 0, 0 };
        // }
    }

    public float Hour()
    {
        return _manager.TimeOfTheDay / TimeManager.LenghtOfDay;
    }

    public float Temperature()
    {
        return _manager.Temperature;
    }
    
    // public float Season()
    // {
    //     return seasonValues[(int)_manager.currentSeason];
    // }
}
