using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
    private float _reducingSplitChance = 2;
    public float SplitChance { get; private set; } = 100f;

    public void InitializeSplitChance(float chanceDivide)
    {
        SplitChance = chanceDivide / _reducingSplitChance;
    }
}
