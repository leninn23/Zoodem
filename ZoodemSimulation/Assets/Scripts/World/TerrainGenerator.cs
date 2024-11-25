using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Animals;
using Animals.Eagle;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] public Vector2Int mapSize;
    
        [Space]
    [SerializeField] private float minHumidity;
    [SerializeField] private float maxHumidity;
    
        [Space]
    [SerializeField] private float minHeat;
    [SerializeField] private float maxHeat;
    
        [Space]
    [SerializeField] private GameObject[] terrainUnits;

    private Perlin _heatMap;
    private Perlin _humidityMap;

    // [SerializeField] private bool generateMap;
    public enum Biome
    {
        Glacier,
        Desert,
        Forest,
        Mountain,
        Lake,
    }
    
    
    private Dictionary<Biome, List<TerrainUnit>> _biomes;
    private Dictionary<Vector2Int, TerrainUnit> _terrainDictionary;
    public Vector2Int _minCorner;

    // private void OnValidate()
    // {
    //     if(!generateMap)    return;
    //     UnityEditor.EditorApplication.delayCall += () =>
    //     {
    //         DestroyMap();
    //         GenerateMap();
    //     };
    //     generateMap = false;
    // }

    // Start is called before the first frame update
    void Awake()
    {
        CreateDictionary();
        _heatMap = new Perlin();
        _heatMap.SetSeed(Random.Range(0, 10000000));
        _humidityMap = new Perlin();
        _humidityMap.SetSeed(Random.Range(0, 10000000));
        _minCorner = mapSize / 2;

        // DestroyMap();
        // GenerateMap();
    }


    /**
    private int selected = -1;
    private Vector3 ssadas;
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 1000, 500));
        var biomes = new Biome[] { Biome.Desert, Biome.Glacier, Biome.Forest, Biome.Lake, Biome.Mountain };
        var biomes2 = new string[] { "Desert", "Glacier", "Forest", "Lake", "Mountain" };
        
        selected = GUILayout.SelectionGrid(selected, biomes2, 5);
        var a = GUILayout.TextArea(ssadas.x.ToString(CultureInfo.InvariantCulture));
        var aa = GUILayout.TextArea(ssadas.z.ToString(CultureInfo.InvariantCulture));
        ssadas.x = float.Parse(a);
        ssadas.z = float.Parse(aa);
        if (GUILayout.Button("Get Random Distance") && selected != -1)
        {
            Debug.Log("The to closest " + biomes2[selected] + " from " + ssadas + " (" + GetTerrainAt(ssadas).biome + ") is " + LocateBiome(biomes[selected], ssadas));
        }
        GUILayout.EndArea();
    }//*/
    // public Nido nest;
    // private Vector3 aaaa;
    // private void OnGUI()
    // {
    //     GUILayout.BeginArea(new Rect(10, 10, 1000, 500));
    //     
    //     var a = GUILayout.TextArea(aaaa.x.ToString(CultureInfo.InvariantCulture));
    //     var aa = GUILayout.TextArea(aaaa.z.ToString(CultureInfo.InvariantCulture));
    //     if(float.TryParse(a, out var asa))
    //     {
    //         aaaa.x = asa;
    //     }
    //     if(float.TryParse(aa, out var asa2))
    //     {
    //         aaaa.z = asa2;
    //     }
    //     if (GUILayout.Button("Spawn nest at " + aaaa))
    //     {
    //         // aaaa.x = float.Parse(a);
    //         // aaaa.y = float.Parse(aa);
    //         SpawnNest(aaaa, nest, nest.gameObject);
    //     }
    //     GUILayout.EndArea();
    // }

    #region Map Generation

    
    private void DestroyMap()
    {
        for (var i = transform.childCount; i > 0; i--)
        {
            if(Application.isEditor)
                DestroyImmediate(transform.GetChild(i-1).gameObject);
            else
                 Destroy(transform.GetChild(i-1).gameObject);
        }
    }
    
    private void GenerateMap()
    {
        // _heatMap.SetSeed(Random.Range(0, 10000000));
        // _humidityMap.SetSeed(Random.Range(0, 10000000));
        for (var x = 0; x < mapSize.x; x++)
        {
            for (var y = 0; y < mapSize.y; y++)
            {
                var pos = new Vector3(_minCorner.x + x, 0, _minCorner.y + y);
                var t = Instantiate(terrainUnits[0], pos, Quaternion.identity, transform).GetComponent<TerrainUnit>();
                t.mapPosition = new Vector2Int(x, y);
            }
        }
    }
    private void GenerateRandomMap()
    {
        // _heatMap.SetSeed(Random.Range(0, 10000000));
        // _humidityMap.SetSeed(Random.Range(0, 10000000));
        var corner = mapSize / 2;
        for (var x = 0; x < mapSize.x; x++)
        {
            for (var y = 0; y < mapSize.y; y++)
            {
                var pos = new Vector3(corner.x + x, 0, corner.y + y);//corner + new Vector2Int(x, y);
                // var heat = _heatMap.Noise(pos.x/ mapSize.x, pos.z/ mapSize.y) + 0.25f;
                // heat = Mathf.Sqrt(heat);
                // var humidity = _humidityMap.Noise(pos.x/ mapSize.x, pos.z/ mapSize.y) + 0.15f;
                //
                // humidity = Mathf.Pow(heat, 0.25f);
                var heat = Mathf.PerlinNoise(pos.x / mapSize.x, pos.z/ mapSize.y);
                var humidity = Mathf.PerlinNoise(pos.z/ mapSize.y, pos.x/ mapSize.x);
                Debug.Log("Heat: " + heat + ", Humidity: " + humidity);
                if (heat > maxHeat)
                {
                    Instantiate(terrainUnits[0], pos, Quaternion.identity, transform);
                }else if (heat < minHeat)
                {
                    Instantiate(terrainUnits[1], pos, Quaternion.identity, transform);
                }
                else
                {
                    if (humidity > maxHumidity)
                    {
                        Instantiate(terrainUnits[2], pos, Quaternion.identity, transform);
                    }else if (humidity < minHumidity)
                    {
                        Instantiate(terrainUnits[4], pos, Quaternion.identity, transform);
                    }
                    else
                    {
                        Instantiate(terrainUnits[3], pos, Quaternion.identity, transform);
                    }
                }
            }
        }
    }

    #endregion

    public void RegisterUnit(TerrainUnit tr)
    {
        if (_biomes.TryGetValue(tr.biome, out var list))
        {
            _biomes[tr.biome].Add(tr);
        }
        _terrainDictionary.Add(tr.mapPosition, tr);
    }
    
    private void CreateDictionary()
    {
        _biomes = new Dictionary<Biome, List<TerrainUnit>>
        {
            { Biome.Glacier, new List<TerrainUnit>() },
            { Biome.Desert, new List<TerrainUnit>() },
            { Biome.Forest, new List<TerrainUnit>() },
            { Biome.Mountain, new List<TerrainUnit>() },
            { Biome.Lake, new List<TerrainUnit>() }
        };
        _terrainDictionary = new Dictionary<Vector2Int, TerrainUnit>();
    }

    public static int BiomeToNumber(Biome biome)
    {
        return biome switch
        {
            Biome.Glacier => 0,
            Biome.Desert => 1,
            Biome.Forest => 2,
            Biome.Mountain => 3,
            Biome.Lake => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(biome), biome, null)
        };
    }

    public Vector2Int RealPosToMapPos(Vector3 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)) - _minCorner;
    }
    
    /// <summary>
    /// Transforms 2d map tile coordinates into world 3d coordinates
    /// </summary>
    /// <param name="pos">The x and y position of the tile</param>
    /// <returns>The world position of the center of the tile</returns>
    public Vector3 MapPosToRealPos(Vector2Int pos)
    {
        return new Vector3(pos.x + _minCorner.x, 0, pos.y + _minCorner.y);
    }
    
    /// <summary>
    /// Tries to create a <see cref="Nido"/> in position <paramref name="pos"/>, and assigns the <paramref name="owner"/> as owner of the nest.
    /// <see cref="ABasicAnimal"/> <paramref name="owner"/> den variable gets assigned to this new den.
    /// </summary>
    /// <param name="pos">Position to spawn the <see cref="Nido"/></param>
    /// <param name="den">Type of <see cref="Nido"/> to spawn</param>
    /// <param name="owner">Animal that spawns the den</param>
    /// <returns></returns>
    public bool SpawnNest(Vector3 pos, Nido den, ABasicAnimal owner)
    {
        var mapPos = RealPosToMapPos(pos);
        // Debug.Log("Map pos: " + mapPos);
        if (_terrainDictionary.TryGetValue(mapPos, out var tu))
        {
            if(tu.den) return false;

            var tuDen = Instantiate(den, tu.realPosition, Quaternion.identity);
            tu.den = tuDen;
            owner.den = tuDen;
            // TODO: SET OWNER OF NEST
            return true;
        }

        return false;
    }
    #region  Perceptions
    public TerrainUnit GetTerrainAt(Vector3 pos)
    {
        var posInt = RealPosToMapPos(pos);
        if (_terrainDictionary.TryGetValue(posInt, out var tu))
        {
            return tu;
        }

        return null;
    }
    
    public bool IsBiomeOfPreference(Vector3 pos, Biome biome)
    {
        return GetTerrainAt(pos)?.biome == biome;
    }
    
    public Vector3 LocateBiome(Biome biome, Vector3 origin)
    {
        var list = _biomes[biome];
        list.Sort((unit, terrainUnit) => Mathf.RoundToInt(Vector3.Distance(unit.realPosition, origin) -
                                                          Vector3.Distance(terrainUnit.realPosition, origin)));
        return list[0].realPosition;
    }

    
    public Nido GetClosestDen(Vector3 origin, float maxSearchDistance)
    {
        var dens = _terrainDictionary.Values.Where(tu => tu.den && Vector3.Distance(origin, tu.realPosition) <= maxSearchDistance).ToList();
        dens.Sort((unit, terrainUnit) => Mathf.RoundToInt(Vector3.Distance(unit.realPosition, origin) -
                                                           Vector3.Distance(terrainUnit.realPosition, origin)));
        return dens.Count == 0 ? null : dens.First().den;
    }
    
    public Nido GetClosestDen(Vector3 origin, float maxSearchDistance, AnimalType animal)
    {
        var dens = _terrainDictionary.Values.Where(tu => tu.den && Vector3.Distance(origin, tu.realPosition) <= maxSearchDistance && tu.den.owner.animalType == animal).ToList();
        dens.Sort((unit, terrainUnit) => Mathf.RoundToInt(Vector3.Distance(unit.realPosition, origin) -
                                                           Vector3.Distance(terrainUnit.realPosition, origin)));
        return dens.First().den;
    }
    
    #endregion
}
