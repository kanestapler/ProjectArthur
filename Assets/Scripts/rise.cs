﻿using UnityEngine;
using System.Collections;

public class rise : MonoBehaviour
{

    public float max;
    public float speed;
    public Vector3 direction;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= max)
        {
            speed = 0.0f;
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
