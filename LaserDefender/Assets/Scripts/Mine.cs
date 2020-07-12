using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Rigidbody2D rb;
    Player target;
    Vector2 moveDirection;
    public float projectileSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindObjectOfType<Player>())
        {
            target = GameObject.FindObjectOfType<Player>();
            MoveToPlayerLocation();
        }
    }

    private void MoveToPlayerLocation()
    {
        moveDirection = (target.transform.position - transform.position).normalized * projectileSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }
}
