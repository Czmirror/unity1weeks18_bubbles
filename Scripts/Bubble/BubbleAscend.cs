using System.Collections;
using System.Collections.Generic;
using Bubbles.Scripts.Interface;
using UnityEngine;

public class BubbleAscend : MonoBehaviour, IFloatable
{
    [SerializeField] private float speed;
    void Update()
    {
        Floating();
    }

    public void Floating()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
