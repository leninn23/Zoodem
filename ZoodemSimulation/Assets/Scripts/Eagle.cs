using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;

    //public TerrainGenerator.Biome biomePreference = TerrainGenerator.Biome.Mountain;
    public List<TerrainGenerator.Biome> biomePreferences = new List<TerrainGenerator.Biome>
    {
        TerrainGenerator.Biome.Mountain,
        TerrainGenerator.Biome.Forest,
        TerrainGenerator.Biome.Desert
    };

    public float speed;
    public Nido myNest;

    public float distanceWalk;

    private float currentDistanceWalk = 0;
    private Vector3 _currentWalkDir;
    public const float maxDistanceNest = 5f;

    public Nido nidoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsInBiome(){
        foreach (var biome in biomePreferences)
        {
            if (terrainGenerator.IsBiomeOfPreference(transform.position, biome))
            {
                return true;
            }
        }
        return false;
    }

    public Status TravelBiome() {
        Vector3 closestBiomePosition = Vector3.zero;
        float closestDistance = float.MaxValue;

        foreach (var biome in biomePreferences)
        {
            var biomePosition = terrainGenerator.LocateBiome(biome, transform.position);
            if (biomePosition != Vector3.zero)
            {
                float distance = Vector3.Distance(transform.position, biomePosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBiomePosition = biomePosition;
                }
            }
        }

        if (closestDistance == float.MaxValue)
        {
            return Status.Running;
        }

        var direction = (closestBiomePosition - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * speed);

        return IsInBiome() ? Status.Success : Status.Running;
    }


    public void StartWalk()
    {
        currentDistanceWalk = 0;
        var dir = Random.insideUnitCircle;
        _currentWalkDir = new Vector3(dir.x, 0, dir.y);
    }
    
    public Status Walk()
    {
        currentDistanceWalk += Time.deltaTime;
        transform.Translate(_currentWalkDir * speed * Time.deltaTime);
        return currentDistanceWalk > distanceWalk ? Status.Success : Status.Running;
    }

    public void CreateNest() 
    {
        terrainGenerator.SpawnNest(transform.position, nidoPrefab, this);
    }
    public bool NearNest() 
    {
        return terrainGenerator.GetClosestNest(transform.position, maxDistanceNest);
    }

    public bool TieneNido()
    {
        return myNest;
    }
    
    
}

