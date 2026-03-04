using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed;
    public float arrawDistance;
    private Rigidbody2D rg2D;
    private Vector3 startPos;
    void Start()
    {
        rg2D= GetComponent<Rigidbody2D>();
        rg2D.velocity = transform.right * speed;
        startPos= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > arrawDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Trap")|| collision.gameObject.CompareTag("BigTrap"))
        {
            
            Destroy(gameObject);
        }
    }
}
