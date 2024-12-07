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
    // public IAnimal owner;

    private void Update()
    {
        if(offspringCount <= 0) return;
        
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
                Instantiate(owner);
            }

            offspringCount = 0;
        }
    }
}
