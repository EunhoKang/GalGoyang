using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatleafParty : Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterManager.charmanager.CatLeafParty();
        base.OnTriggerEnter2D(other);
    }
}
