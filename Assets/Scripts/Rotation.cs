using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;
    

    private void Update()
    {
        transform.Rotate(0, 0, -rotationSpeed * 360 * Time.deltaTime);
    }
}
