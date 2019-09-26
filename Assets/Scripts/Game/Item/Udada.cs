using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Udada : Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterManager.charmanager.Udada();
        base.OnTriggerEnter2D(other);
    }
}
