using System.Collections.Generic;
using System.Linq;
using BehaviourAPI.Core;
using UnityEngine;

namespace Animals.Eagle
{
    public class Eagle : ABasicAnimal
    {

        //public TerrainGenerator.Biome biomePreference = TerrainGenerator.Biome.Mountain;
        
        

        public const float MaxDistanceNest = 5f;

        public Nido nidoPrefab;
        // Start is called before the first frame update
        void Start()
        {
            animalType = AnimalType.Eagle;
        }

        // Update is called once per frame
        void Update()
        {
        
        }


    
        public Status TravelBiome()
        {
            var position = transform.position;
            var dist = Vector3.Distance(walkObjective, position);
            // if (Mathf.Approximately(dist, float.MaxValue))
            // {
            //     return Status.Running;
            // }

            var direction = (walkObjective - position).normalized;
            transform.Translate(direction * Time.deltaTime * moveSpeed);

            return IsInBiome() ? Status.Success : Status.Running;
        }

        
    
        // public Status Walk()
        // {
        //     currentDistanceWalk += Time.deltaTime;
        //     transform.Translate(currentWalkDir * moveSpeed * Time.deltaTime);
        //     return currentDistanceWalk > distanceWalk ? Status.Success : Status.Running;
        // }

        public void CreateNest() 
        {
            terrainGenerator.SpawnNest(transform.position, nidoPrefab, this);
        }
        public bool NearNest() 
        {
            return terrainGenerator.GetClosestDen(transform.position, MaxDistanceNest);
        }

        public bool TieneNido()
        {
            return den;
        }
    
    
    }
}

