using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CubeSearcher : MonoBehaviour
{
    public event Action<ExplosiveCube> CubeFounded;
    
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                Debug.Log(explosiveCube.SplitChance);
                Destroy(explosiveCube.gameObject);
            }
        }
    }
}
