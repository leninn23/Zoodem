using System;
using System.Collections.Generic;
using System.Linq;
using BehaviourAPI.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Animals
{
    public class ABasicAnimal : MonoBehaviour
    {
        public TerrainGenerator terrainGenerator;
        public AnimalType animalType;
        [Space(15)]
        public float health;
        public float maxHealth;
        public float attackDamage;
        [Space(15)]
        public float moveSpeed;
        
        public float energy;
        public float maxEnergy;

        [Space(15)]
        public bool isFemale;
        public ABasicAnimal partner;
        public Nido den;
        [Space(8)]
        public float gestationTime; //in years
        public Vector2Int offspringNRange;
        private int _offspring;

        [FormerlySerializedAs("distanceWalk")] [Space(15)]
        public float wanderDistance;
        protected float currentDistanceWalk = 0;
        protected Vector3 walkObjective;
        protected Vector3 currentWalkDir;
        private float _walkDistance;
        [Space(15)]
        public List<TerrainGenerator.Biome> biomePreferences = new List<TerrainGenerator.Biome>
        {
            TerrainGenerator.Biome.Mountain,
            TerrainGenerator.Biome.Forest,
            TerrainGenerator.Biome.Lake
        };
        public bool IsInBiome()
        {
            return biomePreferences.Any(biome => terrainGenerator.IsBiomeOfPreference(transform.position, biome));
        }
        public Status WalkDir()
        {
            currentDistanceWalk += Time.deltaTime;
            // currentDistanceWalk =
            //     Mathf.Clamp(currentDistanceWalk, 0, Vector3.Distance(transform.position, walkObjective));
            var translation = currentWalkDir * moveSpeed * Time.deltaTime;
            var dist = Vector3.Distance(walkObjective, transform.position);
            if (translation.magnitude >= dist)
            {
                translation = translation.normalized * dist;
                currentDistanceWalk = wanderDistance + 1;
            }
            transform.Translate(translation);
            return currentDistanceWalk > wanderDistance ? Status.Success : Status.Running;
        }
        public Status WalkObjective()
        {
            var trans = transform;
            var dist = Vector3.Distance(trans.position, walkObjective);
            // if (dist <= 0.5f)
            // {
            //     trans.position = walkObjective;
            //     return Status.Success;
            // }
            var maxDist = Mathf.Min(moveSpeed * Time.deltaTime, dist);
            // Debug.Log($"Dist: {dist}, maxDist: {maxDist}, walkDir: {currentWalkDir}");
            trans.Translate(currentWalkDir * maxDist);
            return Mathf.Approximately(maxDist, dist) ? Status.Success : Status.Running;
        }
        public void StartTravelBiome()
        {
            walkObjective = Vector3.zero;
            var closestDistance = float.MaxValue;

            foreach (var biome in biomePreferences)
            {
                var biomePosition = terrainGenerator.LocateBiome(biome, transform.position);
                if (biomePosition != Vector3.zero)
                {
                    float distance = Vector3.Distance(transform.position, biomePosition);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        walkObjective = biomePosition;
                    }
                }
            }
        }

        public void StartWalkToBiome()
        {
            walkObjective = Vector3.zero;
            var closestDistance = float.MaxValue;

            var position = transform.position;
            foreach (var biome in biomePreferences)
            {
                var biomePosition = terrainGenerator.LocateBiome(biome, position);
                if (biomePosition != Vector3.zero)
                {
                    var distance = Vector3.Distance(position, biomePosition);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        walkObjective = biomePosition;
                        walkObjective.y = position.y;
                    }
                }
            }
            
            var fixedDir = walkObjective - position;
            Debug.Log("Walking to " + walkObjective);
            fixedDir.y = 0;
            currentWalkDir = fixedDir.normalized;
        }
        
        public void StartWalk()
        {
            currentDistanceWalk = 0;
            var position = transform.position;
            var dist = 0f;
            do
            {
                var dir = Random.insideUnitCircle * wanderDistance;
                var newPos = terrainGenerator.RealPosToMapPos(position + new Vector3(dir.x, 0, dir.y));
                newPos.x = Mathf.Clamp(newPos.x, 0, terrainGenerator.mapSize.x-1);
                newPos.y = Mathf.Clamp(newPos.y, 0, terrainGenerator.mapSize.y-1);
                walkObjective = terrainGenerator.MapPosToRealPos(newPos);
                walkObjective.y = position.y;
                dist = Vector3.Distance(position, walkObjective);
            } while (dist <= 2f);

            var fixedDir = walkObjective - position;
            fixedDir.y = 0;
            currentWalkDir = fixedDir.normalized;
            Debug.Log($"Going from {position} to {walkObjective}");
            // currentWalkDir = new Vector3(dir.x, 0, dir.y);
        }
        
        
        private void Awake()
        {
            health = maxHealth;
            energy = maxEnergy; 
        }

        public void GenerateOffspring(Nido nido)
        {
            _offspring = Random.Range(offspringNRange.x, offspringNRange.y);
            nido.offspringCount = _offspring;
            nido.timeLeftForSpawn = gestationTime;
            den = nido;
        }
    }
}