using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Biome = TerrainGenerator.Biome;

public class TerrainUnit : MonoBehaviour
{
    public Vector2Int mapPosition;
    public Vector3Int realPosition;
    public Biome biome;
    public Nido nest;
    
    // public 
    [SerializeField] private Material[] biomeMaterials;
    

    private void OnValidate()
    {
        GetComponent<MeshRenderer>().SetMaterials(new List<Material> { biomeMaterials[TerrainGenerator.BiomeToNumber(biome)] });
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        var position1 = transform.position;
        realPosition = new Vector3Int(Mathf.FloorToInt(position1.x), Mathf.FloorToInt(position1.y), Mathf.FloorToInt(position1.z));
        GetComponentInParent<TerrainGenerator>().RegisterUnit(this);
    }

    // // Update is called once per frame
    // private void OnGUI()
    // {
    //     
    // }
}
