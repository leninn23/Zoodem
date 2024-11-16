using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize;
    
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

    [SerializeField] private bool generateMap;
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
    private Vector2Int _minCorner;

    private void OnValidate()
    {
        if(!generateMap)    return;
        UnityEditor.EditorApplication.delayCall += () =>
        {
            DestroyMap();
            GenerateMap();
        };
        generateMap = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        CreateDictionary();
        _heatMap = new Perlin();
        _heatMap.SetSeed(Random.Range(0, 10000000));
        _humidityMap = new Perlin();
        _humidityMap.SetSeed(Random.Range(0, 10000000));
        //
        // DestroyMap();
        // GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**/
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
        _minCorner = mapSize / 2;
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
        _terrainDictionary.Add(new Vector2Int(tr.mapPosition.x, tr.mapPosition.y), tr);
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

    public TerrainUnit GetTerrainAt(Vector3 pos)
    {
        var posInt = new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.z));
        posInt -= _minCorner;
        return _terrainDictionary[posInt];
    }
    
    public bool IsBiomeOfPreference(Vector3 pos, Biome biome)
    {
        return GetTerrainAt(pos).biome == biome;
    }
    
    public Vector3 LocateBiome(Biome biome, Vector3 origin)
    {
        var list = _biomes[biome];
        list.Sort((unit, terrainUnit) => Mathf.RoundToInt(Vector3.Distance(unit.realPosition, origin) -
                                                          Vector3.Distance(terrainUnit.realPosition, origin)));
        return list[0].realPosition;
    }
}
