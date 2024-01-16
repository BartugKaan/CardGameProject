using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAttackCard : Card
{
    [SerializeField] private int damage;
    public override void Play()
    {
        // Add logic for playing a strong attack card
        base.Play();
        Debug.Log("Playing a strong attack card");
        DealDamageToPriest(damage);
    }
    private void DealDamageToPriest(int dmg)
    {
        Priest.Instance.DamageTaken(dmg);
        
    }
}
