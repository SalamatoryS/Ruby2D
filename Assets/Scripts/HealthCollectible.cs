using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    public int maxHealt = 5;
    public int addHealthAtOnce = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController ruby = collision.gameObject.GetComponent<RubyController>();
        
        if (ruby != null)
        {
            if (ruby.CurrentHealth < maxHealt)
            {
                ruby.ChangeHealth(addHealthAtOnce);
                Destroy(gameObject);
                ruby.PlaySound(collectedClip);
            }
            else Debug.Log("Health is full");
        }
    }


}
