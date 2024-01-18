using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Priest enemy;
    
    public TextMeshProUGUI healthText;
    public Image healthBar;
    
    public TextMeshProUGUI priestHealthText;
    public Image priestHealthBar;
    
    public TextMeshProUGUI defenceText;
    public Image defenceBar;
    
    private void OnEnable()
    {
        Events.OnDisplayPriestHealth.AddListener(DisplayPriestHealth);
        Events.OnDisplayPlayerHealth.AddListener(DisplayPlayerHealth);
        Events.OnDisplayPlayerDefence.AddListener(DisplayPlayerDefence);
        
    }

    private void OnDisable()
    {
        Events.OnDisplayPriestHealth.RemoveListener(DisplayPriestHealth);
        Events.OnDisplayPlayerHealth.RemoveListener(DisplayPlayerHealth);
        Events.OnDisplayPlayerDefence.RemoveListener(DisplayPlayerDefence);
    }


    private void DisplayPlayerDefence()
    {
        defenceText.text = player.DefenceAmount.ToString();
        defenceBar.DOFillAmount((float)player.DefenceAmount / player.MaxDefenceAmount, 0.3f);
    }

    private void DisplayPlayerHealth()
    {
        healthText.text = player.Health.ToString();
        healthBar.DOFillAmount((float)player.Health / player.MaxHealth, 0.3f);
    }

    private void DisplayPriestHealth()
    {
        priestHealthText.text = enemy.Health.ToString();
        priestHealthBar.DOFillAmount((float)enemy.Health / enemy.MaxHealth, 0.3f);
    }
}
