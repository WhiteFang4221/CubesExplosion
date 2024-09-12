using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CubeSearcher : MonoBehaviour
{
    private Camera _camera;
    private int _indexLeftMouseButton = 0;

    public event Action<ExplosiveCube> CubeFounded;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_indexLeftMouseButton))
        {
            IdentifyExplosiveCube();
        }
    }

    private void IdentifyExplosiveCube()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
           if (hit.collider.TryGetComponent(out ExplosiveCube explosiveCube))
           {
                CubeFounded?.Invoke(explosiveCube);
                Destroy(explosiveCube.gameObject);
           }
        }
    }
}
