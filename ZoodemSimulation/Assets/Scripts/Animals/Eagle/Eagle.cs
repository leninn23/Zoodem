using System.Collections.Generic;
using System.Linq;
using BehaviourAPI.Core;
using UnityEngine;

namespace Animals.Eagle
{
    public class Eagle : ABasicAnimal
    {
        public TerrainGenerator terrainGenerator;

        //public TerrainGenerator.Biome biomePreference = TerrainGenerator.Biome.Mountain;
        public List<TerrainGenerator.Biome> biomePreferences = new List<TerrainGenerator.Biome>
        {
            TerrainGenerator.Biome.Mountain,
            TerrainGenerator.Biome.Forest,
            TerrainGenerator.Biome.Desert
        };
        
        public float distanceWalk;

        private float _currentDistanceWalk = 0;
        private Vector3 _currentWalkDir;
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

        public bool IsInBiome()
        {
            return biomePreferences.Any(biome => terrainGenerator.IsBiomeOfPreference(transform.position, biome));
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

            if (Mathf.Approximately(closestDistance, float.MaxValue))
            {
                return Status.Running;
            }

            var direction = (closestBiomePosition - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * moveSpeed);

            return IsInBiome() ? Status.Success : Status.Running;
        }

        public void StartWalk()
        {
            _currentDistanceWalk = 0;
            var dir = Random.insideUnitCircle;
            _currentWalkDir = new Vector3(dir.x, 0, dir.y);
        }
    
        public Status Walk()
        {
            _currentDistanceWalk += Time.deltaTime;
            transform.Translate(_currentWalkDir * moveSpeed * Time.deltaTime);
            return _currentDistanceWalk > distanceWalk ? Status.Success : Status.Running;
        }

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

