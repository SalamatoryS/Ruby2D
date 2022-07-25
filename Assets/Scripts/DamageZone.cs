using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        RubyController ruby = collision.gameObject.GetComponent<RubyController>();
        
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
            Debug.Log("Current HP : " + ruby.CurrentHealth);
        }
    }
}
