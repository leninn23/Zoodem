using System.Collections.Generic;
using System.Linq;
using BehaviourAPI.Core;
using UnityEngine;

namespace Animals.Eagle
{
    public class Eagle : ABasicAnimal
    {

        //public TerrainGenerator.Biome biomePreference = TerrainGenerator.Biome.Mountain;
        
        


        // Start is called before the first frame update
        void Start()
        {
            animalType = AnimalType.Eagle;
        }
        
        
    
        // public Status Walk()
        // {
        //     currentDistanceWalk += Time.deltaTime;
        //     transform.Translate(currentWalkDir * moveSpeed * Time.deltaTime);
        //     return currentDistanceWalk > distanceWalk ? Status.Success : Status.Running;
        // }


    
    
    }
}

