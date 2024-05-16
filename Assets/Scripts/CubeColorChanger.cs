using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class CubeColorChanger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
