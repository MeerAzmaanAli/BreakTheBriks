using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombBrick : MonoBehaviour
{
    public float explosionRadius = 2.5f;  // Adjust explosion range
    public LayerMask brickLayer;  // Assign "Brick" layer in Inspector
    BrickSpawner spawner;

    public Transform circle;

    public bool isExploding = false;
    AudioSource Audio;
    public AudioClip exp;
    public Brick b;
    private void Start()
    {
        spawner = FindAnyObjectByType<BrickSpawner>();
        Audio=FindAnyObjectByType<GameManager>().GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isExploding)
        {
            Explosion();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))  // Check if hit by ball
        {
            isExploding = true;
        }
    }

    public void Explode()
    {
        Collider2D[] nearbyBricks = Physics2D.OverlapCircleAll(transform.position, explosionRadius, brickLayer);

        foreach (Collider2D brick in nearbyBricks)
        {
            if (brick.gameObject != gameObject)  // Ensure it doesn’t destroy itself twice
            {
                if (brick.gameObject.GetType() == typeof(BombBrick))
                {
                    brick.GetComponent<BombBrick>().isExploding=true;
                }
                else 
                {
                    
                    brick.TryGetComponent<Brick>(out b);
                    if (b != null)
                    {
                        b.Break();
                    }
                }
                
            }
            
        }
        Audio.PlayOneShot(exp);
        spawner.spawnedBricks.Remove(gameObject);// Update the brick list
        spawner.brickCount--;
        Destroy(gameObject);  // Destroy the explosive brick itself

          
    }
    void Explosion()
    {
        circle.transform.localScale+= new Vector3(circle.transform.localScale.x, circle.transform.localScale.y, circle.transform.localScale.z)*0.5f*Time.deltaTime;
        Invoke("Explode", 2f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
