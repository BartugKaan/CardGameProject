using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDefenceCard : Card
{
    [SerializeField] private int defence;
    

    public override void Play()
    {
        
        base.Play();
        Debug.Log("Playing a basic defense card");
        GetDefence(defence);
    }

    private void GetDefence(int dfnc)
    {
        Player.Instance.DefenceTaken(dfnc);
    }
}