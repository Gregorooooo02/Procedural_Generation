using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseManager : MonoBehaviour
{
    public RawImage noiseImage;
    
    public int width = 256;
    public int height = 256;
    
    [Header("Noise Settings")]
    [SerializeField] float scale = 0.1f;
    private Noise _noise;
    
    private void FixedUpdate()
    {
        RecomputeNoise();
    }
    
    private void Awake()
    {
        _noise = new PerlinNoise();
    }

    private void RecomputeNoise()
    {
        float[,] noise = new float[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noise[x, y] = _noise.GetNoiseMap(x, y, scale);
            }
        }
        SetNoiseTexture(noise);
    }

    private void SetNoiseTexture(float[,] noise)
    {
        Color[] pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                pixels[x + width * y] = Color.Lerp(Color.black, Color.white, noise[x, y]);
            }
        }
        
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();
        
        noiseImage.texture = texture;
    }
}
