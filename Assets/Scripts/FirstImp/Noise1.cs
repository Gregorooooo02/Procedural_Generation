using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Noise1
{
    public abstract float GetNoiseMap(float x, float y, float scale = 1f);
}
