using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackCard : Card
{
    [SerializeField] private int damage;
    public override void Play()
    {
        base.Play();
        Debug.Log("Playing a basic attack card");
        DealDamageToPriest(damage);
    }
    private void DealDamageToPriest(int dmg)
    {
        Priest.Instance.DamageTaken(dmg);
        
    }
}
