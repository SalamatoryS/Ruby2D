using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public float timeInvincible = 2;
    public float speed = 3;
    public GameObject projectilePrefab;
    
    int currentHealth;
    float moveHorizontal;
    float moveVertical;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rubyRigid;
    Animator anim;
    Vector2 lookDirection = new Vector2(1, 0);

    public int CurrentHealth
    {
        get { return currentHealth; }
    }
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rubyRigid = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;       
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveHorizontal, moveVertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rubyRigid.position;

        position.x = position.x + speed * moveHorizontal * Time.deltaTime;
        position.y = position.y + speed * moveVertical * Time.deltaTime;

        rubyRigid.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            anim.SetTrigger("Hit");
            if (isInvincible) return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rubyRigid.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        anim.SetTrigger("Launch");
    }
}
