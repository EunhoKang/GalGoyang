using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabet : Item
{
    public int index;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterManager.charmanager.AlphabetOn(index);
        base.OnTriggerEnter2D(other);
    }
}
