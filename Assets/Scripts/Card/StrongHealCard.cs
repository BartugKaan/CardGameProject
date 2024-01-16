using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongHealCard : Card
{
    [SerializeField]private int healAmount;
    

    public override void Play()
    {
        base.Play();
        Debug.Log("Playing a strong heal card");
        GetHeal(healAmount);
    }
    private void GetHeal(int hl)
    {
        Player.Instance.HealTaken(hl);
    }
}