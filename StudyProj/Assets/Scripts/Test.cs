using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    private readonly Queue<GameObject> objs = new Queue<GameObject>();
    public int speed;

    public void Start()
    {
        var firstCube = CreateCube();
        objs.Enqueue(firstCube);
    }

    private void Update()
    {
        foreach (var obj in objs)
        {
            obj.GetComponent<Transform>().Translate(new Vector3(-1, 0) * speed * Time.deltaTime);
        }

        if (objs.Peek().transform.position.x < -18f)
            Destroy(objs.Dequeue());
        if (objs.Last().transform.position.x < -5)
            objs.Enqueue(CreateCube());

    }

    private void OnDisable()
    {
        foreach (var obj in objs)
            Destroy(obj);
    }

    private GameObject CreateCube()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(3, 10, -9);

        cube.AddComponent<Rigidbody>().isKinematic = false;

        return cube;
    }
}
