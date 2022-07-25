using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController ruby = collision.gameObject.GetComponent<RubyController>();
        
        if (ruby != null)
        {
            if (ruby.CurrentHealth < 5)
            {
                ruby.ChangeHealth(1);
                Destroy(gameObject);
                Debug.Log("Current HP : " + ruby.CurrentHealth);
            }
            else Debug.Log("Health is full");
        }
    }


}
