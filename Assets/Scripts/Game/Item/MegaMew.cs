using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMew : Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterManager.charmanager.MegaMew();
        base.OnTriggerEnter2D(other);
    }
}
