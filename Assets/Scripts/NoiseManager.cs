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

    private void Awake()
    {
        SetNoiseTexture(new float[width, height]);
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
