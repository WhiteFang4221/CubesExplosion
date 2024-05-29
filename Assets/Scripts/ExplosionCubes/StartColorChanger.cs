using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class StartColorChanger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
