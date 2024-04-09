using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{   
    public enum DrawMode 
    {
        NoiseMap,
        ColorMap,
        Mesh
    };

    public DrawMode drawMode;
    public int width;
    public int height;
    public float noiseScale;

    public int octaves;
    
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    
    public int seed;
    public Vector2 offset;
    
    public bool autoUpdate;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise2.GenerateNoiseMap(width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) 
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * width + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromColorMap(colorMap, width, height));
        }
    }

    private void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        
        if (height < 1)
        {
            height = 1;
        }
        
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType 
{
    public string name;
    public float height;
    public Color color;
}