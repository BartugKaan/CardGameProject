using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public static Priest Instance;
    [SerializeField] private int damage = 10;
    [SerializeField] private int health = 100;
    [Range(100,200)]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    private bool _isTakingDamage;

    private void OnEnable()
    {
        Events.OnPriestTurn.AddListener(GiveDamageToPlayer);
    }

    private void OnDestroy()
    {
        Events.OnPriestTurn.RemoveListener(GiveDamageToPlayer);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Health = maxHealth;
        Events.OnDisplayPriestHealth?.Invoke();
    }

    public int Health
    {
        get => health;
        private set
        {
            health = value;
            health = Mathf.Clamp(health, 0, MaxHealth);
            Events.OnDisplayPriestHealth?.Invoke();
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public void DamageTaken(int damage)
    {
        
        StartCoroutine(HandleTakeDamage(damage));
        
    }
    private void GiveDamageToPlayer()
    {
        if (_isTakingDamage)
        {
            return;
        }
        else
        {
            StartCoroutine(HandleGiveDamageToPlayer());
        }
    }
    private IEnumerator HandleTakeDamage(int damage)
    {
        _isTakingDamage = true;
        animator.SetBool("isGettingHit", true);
        Health = health - damage;
        Events.OnDisplayPriestHealth?.Invoke();
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            yield return new WaitForSeconds(5f);
            GameManager.Instance.ChangeState(GameState.End);
        }
        yield return new WaitForSeconds(0.95f);
        
        animator.SetBool("isGettingHit", false);
        _isTakingDamage = false;
        
        StartCoroutine(HandleGiveDamageToPlayer());
        

    }
    private IEnumerator HandleGiveDamageToPlayer()
    {
        
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.93f);
        player.DamageTaken(damage);
        animator.SetBool("isHit", false);
        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }
}
