using UnityEngine;
[RequireComponent (typeof(Renderer))]

public class CreatedCube : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Random.ColorHSV();
    }
}
