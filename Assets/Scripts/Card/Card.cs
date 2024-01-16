using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public virtual void Play()
    {
        // Add logic for playing a generic card
        Debug.Log("Playing a card");
    }
}
