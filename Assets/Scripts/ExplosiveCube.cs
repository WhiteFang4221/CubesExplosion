using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
    public float SplitChance { get; private set; } = 100f;

    private float _reducingSplitChance = 2;

    public void InitializeSplitChance(float chanceDivide)
    {

        SplitChance = chanceDivide / _reducingSplitChance;
    }
}
