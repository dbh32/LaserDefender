using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float spinningSpeed;

    void Update()
    {
        transform.Rotate(0, 0, spinningSpeed * Time.deltaTime);
    }
}
