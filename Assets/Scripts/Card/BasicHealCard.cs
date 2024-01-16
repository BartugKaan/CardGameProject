using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealCard : Card
{
    [SerializeField]private int healAmount;
    [SerializeField] private Player player;
    

    public override void Play()
    {
        // Add logic for playing a basic heal card
        base.Play();
        Debug.Log("Playing a basic heal card");
        GetHeal(healAmount);
    }
    private void GetHeal(int hl)
    {
        Player.Instance.HealTaken(hl);
    }
}
