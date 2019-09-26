using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLine : MonoBehaviour
{
    public ParkourCircle parkour;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer==17)
        {
            parkour.PushButton();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 17)
        {
            parkour.ReleaseButton();
        }
    }
}
