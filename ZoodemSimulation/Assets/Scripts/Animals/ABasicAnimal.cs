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
        [Space(7)][Header("Basic settings")]
        public float maxHealth;
        public float maxEnergy;
        public float maxFood;
        public float attackDamage;
        public float moveSpeed;

        [Space(7)] [Header("Advanced settings")]
        public float walkEnergyDrain;
        public float huntEnergyDrain;
        public float foodDrain;
        [Header("Current state")] public float food;
        public float health;
        public float energy;
        // [Space(15)]
        

        [Space(7)][Header("Relationship attributes")]
        public bool isFemale;
        public ABasicAnimal partner;
        public RelationshipStatus relationshipState = RelationshipStatus.Single;
        public Nido den;
        public Nido nidoPrefab;
        private Collider[] _aBasicAnimals;
        [Space(7)][Header("Relationship settings")]
        public float minDistanceNest = 5f;
        public float gestationTime; //in years
        public Vector2Int offspringNRange;
        [Tooltip("How much food to give each child in the den")] public float foodPerChild;
        private int _offspring;

        //TODO: Look for a way to court a certain animal, and communicate with it to avoid other partners to court with them
        //maybe an enum would work (single, engaged, courting, beingCourted)
        
        [FormerlySerializedAs("distanceWalk")] [Space(15)]
        public float wanderDistance;
        protected float currentDistanceWalk = 0;
        protected Vector3 walkObjective;
        protected Vector3 currentWalkDir;
        private float _walkDistance;
        
        [Space(7)]
        public List<TerrainGenerator.Biome> biomePreferences = new List<TerrainGenerator.Biome>
        {
            TerrainGenerator.Biome.Mountain,
            TerrainGenerator.Biome.Forest,
            TerrainGenerator.Biome.Lake
        };
        
        public enum RelationshipStatus
        {
            Single,
            Enganged,
            Courting,
            BeingCourted,
        }
        private void Awake()
        {
            _aBasicAnimals = new Collider[10];
            
            food = maxFood;
            health = maxHealth;
            energy = maxEnergy; 
        }
        #region Movement

        // public Status WalkDir()
        // {
        //     currentDistanceWalk += Time.deltaTime;
        //     // currentDistanceWalk =
        //     //     Mathf.Clamp(currentDistanceWalk, 0, Vector3.Distance(transform.position, walkObjective));
        //     var translation = currentWalkDir * moveSpeed * Time.deltaTime;
        //     var dist = Vector3.Distance(walkObjective, transform.position);
        //     if (translation.magnitude >= dist)
        //     {
        //         translation = translation.normalized * dist;
        //         currentDistanceWalk = wanderDistance + 1;
        //     }
        //     transform.Translate(translation);
        //     return currentDistanceWalk > wanderDistance ? Status.Success : Status.Running;
        // }
        public Status WalkObjective()
        {
            var trans = transform;
            var dist = Vector3.Distance(trans.position, walkObjective);
            var maxDist = Mathf.Min(moveSpeed * Time.deltaTime, dist);
            trans.Translate(currentWalkDir * maxDist);
            energy -= walkEnergyDrain * Time.deltaTime;
            return Mathf.Approximately(maxDist, dist) ? Status.Success : Status.Running;
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
            fixedDir.y = 0;
            currentWalkDir = fixedDir.normalized;
        }
        
        public void StartWalkRandom()
        {
            currentDistanceWalk = 0;
            var position = transform.position;
            var dist = 0f;
            do
            {
                //Normalize the Random.insideUnitCircle if you want the animal to walk exactly wanderDistance units
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
            // currentWalkDir = new Vector3(dir.x, 0, dir.y);
        }

        public void StartWalkToNest()
        {
            walkObjective = den.transform.position;
            currentWalkDir = walkObjective - transform.position;
        }
        #endregion
        
        public bool IsInBiome()
        {
            return biomePreferences.Any(biome => terrainGenerator.IsBiomeOfPreference(transform.position, biome));
        }
        
        #region Relationship perceptions

        public bool HasChildren()
        {
            if (den)
            {
                return den.offspringCount > 0;
            }

            return false;
        }

        public bool HasPartner()
        {
            return relationshipState == RelationshipStatus.Enganged;
        }
        

        public bool NearNest() 
        {
            return terrainGenerator.GetClosestDen(transform.position, minDistanceNest);
        }

        public bool TieneNido()
        {
            return den;
        }
        #endregion

        #region Relationship functions
        public void CreateNest() 
        {
            terrainGenerator.SpawnNest(transform.position, nidoPrefab, this);
        } 
        public void GenerateOffspring(Nido nido)
        {
            _offspring = Random.Range(offspringNRange.x, offspringNRange.y);
            nido.offspringCount = _offspring;
            nido.timeLeftForSpawn = gestationTime;
            den = nido;
        }

        public void FeedOffspring()
        {
            den.food += _offspring * foodPerChild;
        }

        private bool TryFindPartner(float radius, out ABasicAnimal potentialPartner)
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, _aBasicAnimals,
                    LayerMask.GetMask("Animal")) == 0)
            {
                potentialPartner = null;
                return false;
            }
            
            var near = _aBasicAnimals.Select(collider1 => collider1.GetComponent<ABasicAnimal>()).Where(animal =>
            {
                if (animal)
                {
                    return animal.animalType == animalType && animal.relationshipState == RelationshipStatus.Single;
                }

                return false;
            }).ToList();
            if (near.Count == 0)
            {
                potentialPartner = null;
                return false;
            }

            potentialPartner = near[Random.Range(0, near.Count)];
            return true;
        }

        public bool FindPartner()
        {
            if (TryFindPartner(10f, out var potentialPartner))
            {
                partner = potentialPartner;
                partner.Court(this);
                relationshipState = RelationshipStatus.Courting;
            }

            return false;
        }

        private void Court(ABasicAnimal animal)
        {
            partner = animal;
            relationshipState = RelationshipStatus.BeingCourted;
        }
        
        #endregion

    }
}