using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isPlayerTurn = false;
    public static Player Instance;
    [SerializeField] private int health = 100;
    [Range(100,200)]
    [SerializeField] private int maxHealth = 100;
    
    [SerializeField] private int defenceAmount = 0;
    [Range(100,200)]
    [SerializeField] private int maxDefenceAmount = 100;
    
    [SerializeField]private Camera _mainCamera;
    private bool _isDragging = false;
    private Vector3 _offset;
    private GameObject _card;

    private void OnEnable()
    {
        Events.OnPlayerTurn.AddListener(PlayerTurn);
    }
    private void OnDisable()
    {
        Events.OnPlayerTurn.RemoveListener(PlayerTurn);
    }
    private void PlayerTurn()
    {
        
        _isPlayerTurn = true;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Health = maxHealth;
        DefenceAmount = 0;
        Events.OnDisplayPlayerHealth?.Invoke();
        Events.OnDisplayPlayerDefence?.Invoke();
    }

    public int Health
    {
        get => health;
        private set
        {
            health = value;
            health = Mathf.Clamp(health, 0, MaxHealth);
            Events.OnDisplayPlayerHealth?.Invoke();
        }
    }
    public int DefenceAmount
    {
        get => defenceAmount;
        private set
        {
            defenceAmount = value;
            defenceAmount = Mathf.Clamp(defenceAmount, 0, MaxDefenceAmount);
            Events.OnDisplayPlayerDefence?.Invoke();
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public int MaxDefenceAmount
    {
        get => maxDefenceAmount;
        set => maxDefenceAmount = value;
    }

    public void DefenceTaken(int defence)
    { 
        DefenceAmount = defenceAmount + defence;
        Events.OnDisplayPlayerDefence?.Invoke();
    }
    public void HealTaken(int heal)
    { 
        Health = health + heal;
        Events.OnDisplayPlayerHealth?.Invoke();
    }
    public void DamageTaken(int damage)
    { 
        if(DefenceAmount >= damage)
        {
            DefenceAmount = defenceAmount - damage;
            Events.OnDisplayPlayerDefence?.Invoke();
            if (DefenceAmount > 0) return;
        }
        else
        {
            damage = damage - defenceAmount;
            DefenceAmount = 0;
            Health = health - damage;
            Events.OnDisplayPlayerHealth?.Invoke();
            Events.OnDisplayPlayerDefence?.Invoke();
            if (health > 0) return;
            GameManager.Instance.ChangeState(GameState.End);
        }
        
    }

    void Update()
    {
        if(_isPlayerTurn == false) return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("Card"))
                {
                    _isDragging = true;
                    _card = hitInfo.collider.gameObject;
                    _offset = _card.transform.position - hitInfo.point;
                }
            }
        }

        if (_isDragging)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                _card.transform.position = hitInfo.point + _offset;
            }
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            _isDragging = false;
            Card cardComponent = _card.GetComponent<Card>();
            if (cardComponent != null)
            {
                cardComponent.Play();
                _isPlayerTurn = false;
                GameManager.Instance.ChangeState(GameState.PriestTurn);
            }
            
        }
    }
    
    void OnDrawGizmos()
    {
        // Visualize the ray in the scene view
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 10f);
    }
}
