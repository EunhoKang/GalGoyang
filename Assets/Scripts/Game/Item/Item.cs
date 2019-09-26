using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemScore;
    public AudioClip eatSound;
    public int audioChannel=2;
    public int item_ID;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(item_ID>=1 && item_ID < 4)
            {
                CharacterManager.charmanager.itemCounts[item_ID-1]();
            }
            SoundManager.soundmanager.SFXSet(eatSound, audioChannel);
            CharacterManager.charmanager.ScoreUIUpdate(itemScore);
            gameObject.SetActive(false);
        }
    }
}
