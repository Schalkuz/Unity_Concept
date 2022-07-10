using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplode : MonoBehaviour
{
    [SerializeField]
    private int cubesPerAxis = 8;
    [SerializeField]
    private float delay = 1f;
    [SerializeField]
    private float force = 300f;
    [SerializeField]
    private float radius = 2f;

    private void Start()
    {
        Invoke("Main", delay);
    }

    void Main()
    {
        for(int x = 0; x < cubesPerAxis; x++)
        {
            for(int y = 0; y < cubesPerAxis; y++)
            {
                for (int z = 0; z < cubesPerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }

        Destroy(gameObject);

    }

    void CreateCube(Vector2 coordinates)
    {
        // Firstly create the new cube

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Set the newly created cube material to be the same as the parent cube

        Renderer rd = cube.GetComponent<Renderer>();
        rd.material = GetComponent<Renderer>().material;

        // Set the new cube scale to be a fraction of the parent cube depending
        // on how many cubes are created

        cube.transform.localScale = transform.localScale / cubesPerAxis;

        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

        // Now for the explosion
        var cubeRb = cube.AddComponent<Rigidbody>();
        cubeRb.AddExplosionForce(force, transform.position, radius);
    }
}
