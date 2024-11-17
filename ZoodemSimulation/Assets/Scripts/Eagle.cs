using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;

    public TerrainGenerator.Biome biomePreference = TerrainGenerator.Biome.Mountain;

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
        return terrainGenerator.IsBiomeOfPreference(transform.position, biomePreference);
    }
    
    public Status TravelBiome() {
        var direction = (terrainGenerator.LocateBiome(biomePreference, transform.position) - transform.position).normalized;
        transform.Translate(direction*Time.deltaTime*speed);
        
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

