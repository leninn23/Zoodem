using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class Bush : MonoBehaviour, IFood
{

    public int currentBerries;

    public float berryValue;
    public float spawnTime;
    private float _timer = 0;
    
    private SphereCollider _collider;

    private List<GameObject> _berries;

    public List<TimeManager.Season> spawnSeasons;
    // Start is called before the first frame update
    void Start()
    {
        FoodState = IFood.FoodStates.Fresh;
        FoodType = IFood.FoodTypes.Berry;
        
        _collider = GetComponent<SphereCollider>();
        _berries = new List<GameObject>();
        foreach (Transform berry in transform)
        {
            _berries.Add(berry.gameObject);
            berry.gameObject.SetActive(false);
        }

        _timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnSeasons.Contains(TimeManager.Instance.currentSeason) && currentBerries<_berries.Count)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                SpawnBerry();
                _timer = spawnTime;
            }
        }
    }

    private void SpawnBerry()
    {
        if (currentBerries < _berries.Count)
        {
            _berries[currentBerries].SetActive(true);
            currentBerries++;
        }
    }
    
    // private void GenerateBerries()
    // {
    //     foreach (var berry in _berries)
    //     {
    //         berry.SetActive(true);
    //         currentBerries++;
    //     }
    // }
    
    public float GetFoodValue()
    {
        return berryValue;
    }

    public void GetEaten()
    {
        currentBerries--;
        if (currentBerries <= 0)
        {
            _collider.enabled = false;
        }
        if(currentBerries >= 0)
        {
            _berries[currentBerries].SetActive(false);
        }
    }

    public IFood.FoodTypes FoodType { get; set; }
    public IFood.FoodStates FoodState { get; set; }
}
