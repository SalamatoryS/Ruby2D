using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public ParticleSystem projectileParticle;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("DestroyObj", 1);
    }

    public void Launch(Vector2 direction, float forse)
    {
        rigidbody2d.AddForce(direction * forse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController e = collision.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }
        Instantiate(projectileParticle, gameObject.transform.position, projectileParticle.transform.rotation);
        Destroy(gameObject);
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
