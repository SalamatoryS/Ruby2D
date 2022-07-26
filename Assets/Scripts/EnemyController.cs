using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public int damage = 1;
    public ParticleSystem smokeEffect;
    public AudioClip enemyHit;

    AudioSource enemyAudio;
    Rigidbody2D rb;
    Animator animator;
    RubyController ruby;
    float timer;
    int direction = 1;
    bool broken = true;

    private void Start()
    {
        ruby = GameObject.Find("Ruby").GetComponent<RubyController>();
        enemyAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rb.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(- damage);
       }
    }

    public void Fix()
    {
        smokeEffect.Stop();
        broken = false;
        rb.simulated = false;
        animator.SetTrigger("Fixed");
        ruby.PlaySound(enemyHit);
        enemyAudio.Pause();
    }
}
