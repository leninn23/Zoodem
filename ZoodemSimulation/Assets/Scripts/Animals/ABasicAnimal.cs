using System;
using System.Collections.Generic;
using System.Linq;
using BehaviourAPI.Core;
using UnityEngine;
using UnityEngine.Serialization;
using World;
using Random = UnityEngine.Random;

namespace Animals
{
    public class ABasicAnimal : MonoBehaviour
    {
        public StatusDisplay display;
        public Corpse corpse;
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
        public float sleepFoodDrain;
        public float sleepEnergyGain;
        public float sleepHealthGain;
        public List<IFood.FoodTypes> foodPreferences;
        [Header("Current state")]
        public float food;
        public float health;
        public float energy;
        public bool isSleeping;

        private float _timer;
        // [Space(15)]


        [Space(7)] [Header("Relationship attributes")]
        public GenderAnimal gender = GenderAnimal.unassigned;
        //public bool isFemale;
        public ABasicAnimal partner;
        public bool isDom;
        public RelationshipStatus relationshipState = RelationshipStatus.Single;
        public Nido den;
        public Nido nidoPrefab;
        private Collider[] _aBasicAnimals;
        public float courtTime;
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
        private Transform _prey;
        
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

        public enum GenderAnimal
        {
            male,
            female,
            unassigned
        }
        private void Awake()
        {
            terrainGenerator = FindObjectOfType<TerrainGenerator>();
            _aBasicAnimals = new Collider[10];
            
            food = maxFood;
            health = maxHealth;
            energy = maxEnergy;

            display = GetComponentInChildren<StatusDisplay>();
            // terrainGenerator = FindObjectOfType<TerrainGenerator>();
        }

        private void Update()
        {
            if (food <= 0.1f * food)
            {
                health -= 0.1f * Time.deltaTime;
            }
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
        private float _prevDistance;
        public Status WalkObjective()
        {
            var trans = transform;
            var dist = Vector3.Distance(trans.position, walkObjective);
            var maxDist = Mathf.Min(moveSpeed * Time.deltaTime, dist);
            trans.Translate(currentWalkDir * maxDist);
            energy = Mathf.Clamp(energy - walkEnergyDrain * Time.deltaTime, 0, maxEnergy);
            food = Mathf.Clamp(food - foodDrain * Time.deltaTime, 0, maxFood);
            if (_prevDistance < dist)
            {
                transform.position = walkObjective;
                return Status.Success;
            }
            else
            {
                _prevDistance = dist;
            }
            Debug.Log("At a distance of " + dist + " --- " + maxDist + ", " + name);
            return Mathf.Abs(maxDist - dist) <= 0.005f ? Status.Success : Status.Running;
        }
        public Status WalkPrey()
        {
            if (!_prey)
            {
                // _display.RemoveStatus(StatusDisplay.Statuses.Hunting);
                return Status.Failure;
            }
            
            var trans = transform;
            var preyPosition = _prey.position;
            var position = trans.position;
            var dist = Vector3.Distance(position, preyPosition);
            currentWalkDir = preyPosition - position;
            currentWalkDir.y = 0;
            var maxDist = Mathf.Min(moveSpeed * Time.deltaTime, dist);
            trans.Translate(currentWalkDir * maxDist);
            energy = Mathf.Clamp(energy - huntEnergyDrain * Time.deltaTime, 0, maxEnergy);
            food = Mathf.Clamp(food - foodDrain* Time.deltaTime, 0, maxFood);
            return Mathf.Abs(maxDist - dist) <= 1f ? Status.Success : Status.Running;
        }

        private void SetUpObjectiveAndDirection(Vector3 obj)
        {
            _prevDistance = float.MaxValue;
            walkObjective = obj;
            var position = transform.position;
            walkObjective.y = position.y;
            var fixedDir = walkObjective - position;
            fixedDir.y = 0;
            currentWalkDir = fixedDir.normalized;
        }
        public void StartWalkFood()
        {
            if (_prey)
            {
                SetUpObjectiveAndDirection(_prey.transform.position);
            }
            else
            {
                Debug.LogError("No food on sight m'sir");
                SetUpObjectiveAndDirection(transform.position);
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
            
            SetUpObjectiveAndDirection(walkObjective);
        }
        
        public void StartWalkRandom()
        {
            _prevDistance = float.MaxValue;
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

            SetUpObjectiveAndDirection(walkObjective);
            // currentWalkDir = new Vector3(dir.x, 0, dir.y);
        }

        public void StartWalkToNest()
        {
            _prevDistance = float.MaxValue;
            SetUpObjectiveAndDirection(den.transform.position);
            Debug.Log("Viajando a nido desde " + transform.position + " en " + walkObjective + " " + name);
        }
        #endregion
        
        public bool IsInBiome()
        {
            return biomePreferences.Where(biome => terrainGenerator.IsBiomeOfPreference(transform.position, biome)).ToArray().Length > 0;
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
            Debug.Log("Near mnesst?");
            return terrainGenerator.GetClosestDen(transform.position, minDistanceNest);
        }

        public bool TieneNido()
        {
            return den;
        }

        public bool IsFemale()
        {
            return gender == GenderAnimal.female;
        }
        #endregion

        #region Relationship functions
        public void CreateNest() 
        {
            if(terrainGenerator.SpawnNest(transform.position, nidoPrefab, this, out var nest)){
                Debug.Log(nest);
                den = nest;
                den.owner = this;
            }
        } 
        public void GenerateOffspring()
        {
            _offspring = Random.Range(offspringNRange.x, offspringNRange.y);
            den.offspringCount = _offspring;
            den.timeLeftForSpawn = gestationTime;
        }
        public void StartIncubate()
        {
            _timer = gestationTime;
        }
        public Status Incubate()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                GenerateOffspring();
                return Status.Success;
            }

            return Status.Running;
        }
        
        public void FeedOffspring()
        {
            den.food += _offspring * foodPerChild;
        }
        public void Rest()
        {
            energy = Mathf.Clamp(energy+ 10, 0, maxEnergy);
            health = Mathf.Clamp(health + 10, 0, maxHealth);
        }

        public float IsBeingCourted()
        {
            if (relationshipState == RelationshipStatus.BeingCourted)
            {
                return 10f;
            }
            else
            {
                return 0f;
            }
        }
        private bool TryFindPartner(float radius, out ABasicAnimal potentialPartner)
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, _aBasicAnimals,
                    LayerMask.GetMask("Animal")) == 0)
            {
                potentialPartner = null;
                return false;
            }

            // foreach (var aBasicAnimal in _aBasicAnimals)
            // {
            //     Debug.Log($"Animal: " +  aBasicAnimal.name);
            // }
            var near = _aBasicAnimals.Select(collider1 =>
            {
                if (collider1 && collider1.TryGetComponent<ABasicAnimal>(out var animal))
                {
                    return animal;
                }

                return null;
            }).Where(animal =>
            {
                if (animal)
                {
                    return animal.animalType == animalType && animal.relationshipState == RelationshipStatus.Single && animal != this;
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
            if (relationshipState != RelationshipStatus.Single) return true;
            
            if (TryFindPartner(10f, out var potentialPartner))
            {
                partner = potentialPartner;
                isDom = true;
                partner.Court(this);
                relationshipState = RelationshipStatus.Courting;
            }

            return false;
        }

        public void Court(ABasicAnimal animal)
        {
            partner = animal;
            isDom = false;
            relationshipState = RelationshipStatus.BeingCourted;
            _timer = courtTime;
            display.PushStatus(StatusDisplay.Statuses.Courting);
            partner.display.PushStatus(StatusDisplay.Statuses.Courting);
        }

        public Status Courting()
        {
            if (relationshipState == RelationshipStatus.BeingCourted) return Status.Running;
            
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                display.RemoveStatus(StatusDisplay.Statuses.Courting);
                partner.display.RemoveStatus(StatusDisplay.Statuses.Courting);
                partner.relationshipState = RelationshipStatus.Enganged;
                relationshipState = RelationshipStatus.Enganged;
                return Status.Success;
            }

            return Status.Running;
        }

        public void AssignGender()
        {
            var isFemale = Random.Range(0, 1) == 0;
            gender = isFemale ? GenderAnimal.female : GenderAnimal.male;
            partner.gender = isFemale ? GenderAnimal.male : GenderAnimal.female;
        }
        
        #endregion

        #region variable perceptions

        public float Energy()
        {
            return energy / maxEnergy;
        }
        public float Health()
        {
            return health / maxHealth;
        }
        public float Hunger()
        {
            return 1 - (food / maxFood);
        }

        public float HasNest()
        {
            return (den == null) ? 1 : 0;
        }

        public float IsAwake()
        {
            return isSleeping ? 0 : 1;
        }
        #endregion

        #region Hunt perceptions

        public bool PreyNear()
        {
            var foodNear = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Animal"));
            foreach (var c in foodNear)
            {
                if (c.TryGetComponent<IFood>(out var component))
                {
                    if (component.FoodState == IFood.FoodStates.Alive && foodPreferences.Contains(component.FoodType))
                    {
                        _prey = c.transform;
                        // display.PushStatus(StatusDisplay.Statuses.Hunting);
                        return true;
                        // walkObjective = c.transform.position;
                        // currentWalkDir = walkObjective - transform.position;
                        //     currentWalkDir.y = 0;
                    }
                }
            }
            return false;
        }
        public bool FoodNear()
        {
            var foodNear = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Animal"));
            foreach (var c in foodNear)
            {
                if (c.TryGetComponent<IFood>(out var component))
                {
                    if (component.FoodState != IFood.FoodStates.Alive && foodPreferences.Contains(component.FoodType))
                    {
                        walkObjective = c.transform.position;
                        currentWalkDir = walkObjective - transform.position;
                        currentWalkDir.y = 0;
                        _prey = c.transform;
                        return true;
                    }
                }
            }

            return false;
        }
        public bool LowHealth()
        {
            return health < maxHealth * 0.2;
        }
        public void Honeycomb()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Hunt functions

        public void StartAttack()
        {
            
            display.PushStatus(StatusDisplay.Statuses.Hunting);
            _timer = 0f;
        }
        
        public Status Attack()
        {
            if (!_prey)
            {
                return Status.Failure;
            }
            
            _timer -= Time.deltaTime;
            if (_timer > 0) return Status.Running;
            
            if (_prey.TryGetComponent<ABasicAnimal>(out var animal))
            {
                animal.GetAttacked(attackDamage);
                _timer = 1f;
                return Status.Running;
            }
            return Status.Failure;
        }

        public Action onDeath;
        
        private void GetAttacked(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                var a = Instantiate(corpse, transform.position, transform.rotation);
                onDeath?.Invoke();
                Destroy(gameObject);
            }
        }

        public void Eat()
        {
            if(_prey)
            {
                Destroy(_prey.gameObject);
                _prey = null;
                food += 20f;
            }
        }
        #endregion

        public void WakeUp()
        {
            Debug.Log("I woke up! " + name);
            display.RemoveStatus(StatusDisplay.Statuses.Sleeping);
            isSleeping = false;
        }
        public void StartSleep()
        {
            Debug.Log("I'm off to sleep! " + name);
            display.PushStatus(StatusDisplay.Statuses.Sleeping);
            isSleeping = true;
        }

        public void Sleep()
        {
            
            energy = Mathf.Clamp(energy + sleepEnergyGain, 0, maxEnergy);
            health = Mathf.Clamp(health + sleepHealthGain, 0, maxHealth);
            food = Mathf.Clamp(food - sleepFoodDrain, 0, maxFood);
        }
    }
}