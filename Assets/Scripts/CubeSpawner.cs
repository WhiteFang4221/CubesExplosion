using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    public Action CubeCLicked;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CheckClickedObject();
        }
    }

    private void CheckClickedObject()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ExplosiveCube explosiveCube = hit.collider.gameObject.GetComponent<ExplosiveCube>();

            if (explosiveCube != null)
            {
                Destroy(explosiveCube.gameObject);
            }
        }
    }
}
