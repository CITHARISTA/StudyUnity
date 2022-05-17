using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CubeFalling : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 5f;
    public Vector3 startPosition = new Vector3(0, 20, 0);
    public Vector3 moveDirection;
    public float timeDeleySec = 0;

    private readonly Queue<GameObject> _queueOfObjects = new Queue<GameObject>();
    private GameObject _lastCreatedCube;


    private void Start()
    {
        StartCoroutine(Waiter());
    }

    private void Update()
    {
        foreach (var cube in _queueOfObjects)
            cube.transform.Translate(moveDirection * speed * Time.deltaTime);

        var heading = startPosition - _lastCreatedCube.transform.position;
        heading.y = 0;
        if (heading.magnitude > distance)
            _queueOfObjects.Enqueue(CreateCube());

        if (_queueOfObjects.Peek().transform.position.y < -1)
            Destroy(_queueOfObjects.Dequeue());
    }

    private GameObject CreateCube()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = startPosition;
        cube.AddComponent<Rigidbody>().isKinematic = false;

        _lastCreatedCube = cube;

        return cube;
    }

    private IEnumerator<WaitForSeconds> Waiter()
    {
        yield return new WaitForSeconds(timeDeleySec);
        _queueOfObjects.Enqueue(CreateCube());
    }
}

