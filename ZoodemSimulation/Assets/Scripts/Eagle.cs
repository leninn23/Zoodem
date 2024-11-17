using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;

    public TerrainGenerator.Biome biomePreference = TerrainGenerator.Biome.Mountain;

    public float speed;

    public float distanceWalk;

    private float currentDistanceWalk = 0;
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
        return terrainGenerator.IsBiomeOfPreference(transform.position, biomePreference);
    }
    
    public Status TravelBiome() {
        var direction = (terrainGenerator.LocateBiome(biomePreference, transform.position) - transform.position).normalized;
        transform.Translate(direction*Time.deltaTime*speed);
        
        return IsInBiome() ? Status.Success : Status.Running;
    }

    public Status Walk()
    {
        currentDistanceWalk += Time.deltaTime;
        return currentDistanceWalk > distanceWalk ? Status.Success : Status.Running;
    }

    public void CreateNest() 
    {
        terrainGenerator.SpawnNest(transform.position, nidoPrefab, gameObject);
    }
    public bool NearNest() 
    {
        return terrainGenerator.GetClosestNest(transform.position, maxDistanceNest);
    }

    
}

