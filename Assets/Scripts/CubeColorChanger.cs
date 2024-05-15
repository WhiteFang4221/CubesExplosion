using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class CubeColorChanger : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
