using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catleaf : MonoBehaviour
{
    public int catLeafScore=1000;
    public AudioClip Leaf;

    public void Clicked()
    {
        SoundManager.soundmanager.SFXSet(Leaf, 2);
        CharacterManager.charmanager.ScoreUIUpdate(catLeafScore);
        CharacterManager.charmanager.catLeafCount++;
        gameObject.SetActive(false);
    }
}
