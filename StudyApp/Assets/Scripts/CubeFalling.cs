using System.Collections.Generic;
using UnityEngine;

public class CubeFalling : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 5f;
    public Vector3 startPosition = new Vector3(0, 20, 0);
    public Vector3 moveDirection;

    private readonly Queue<GameObject> _queueOfObjects = new Queue<GameObject>();
    private GameObject _lastCreatedCube;


    private void Start()
    {
        _queueOfObjects.Enqueue(CreateCube());
    }

    private void Update()
    {
        foreach (var cube in _queueOfObjects)
            cube.transform.Translate(moveDirection * speed * Time.deltaTime);

        if (_lastCreatedCube != null && Vector3.Distance(_lastCreatedCube.transform.position, startPosition) > distance)
            _queueOfObjects.Enqueue(CreateCube());
    }

    private GameObject CreateCube()
    {
        if (_lastCreatedCube != null)
            return Instantiate(_lastCreatedCube)
                .SetStartPosition(startPosition);

        var cube = GameObject
            .CreatePrimitive(PrimitiveType.Cube)
            .SetStartPosition(startPosition);

        cube.AddComponent<Rigidbody>().isKinematic = false;

        _lastCreatedCube = cube;

        return cube;
    }
}

public static class CubeExtention
{
    public static GameObject SetStartPosition(this GameObject cube, Vector3 startPosition)
    {
        cube.transform.position = startPosition;
        return cube;
    }
}
