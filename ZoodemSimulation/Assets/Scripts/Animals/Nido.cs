using System;
using System.Collections;
using System.Collections.Generic;
using Animals;
using UnityEngine;

public class Nido : MonoBehaviour
{
    public int offspringCount;
    public float timeLeftForSpawn;
    public float food;
    public float foodDrain = 0.01f;
    public int freeSpace;

    public ABasicAnimal owner;

    public ABasicAnimal animalPrefab;
    private bool _incubated;

    protected Action _onBorn;
    protected Action _onIncubated;
    // public IAnimal owner;

    private void Update()
    {
        if(offspringCount <= 0) return;
        if (!_incubated)
        {
            _onIncubated?.Invoke();
        }
        _incubated = true;
        
        timeLeftForSpawn -= Time.deltaTime;
        food -= foodDrain * Time.deltaTime;
        if (food <= 0)
        {
            offspringCount = 0;
        }

        if (timeLeftForSpawn <= 0)
        {
            for (int i = 0; i < offspringCount; i++)
            {
                Instantiate(animalPrefab, transform.position, transform.rotation);
            }

            _incubated = false;
            _onBorn?.Invoke();
            offspringCount = 0;
        }
        
    }
}
