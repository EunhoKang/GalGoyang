using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMew : Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterManager.charmanager.MegaMew();
        }
        
    }
}
