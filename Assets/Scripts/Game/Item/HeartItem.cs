using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterManager.charmanager.HealthGain();
        base.OnTriggerEnter2D(other);
    }
}
