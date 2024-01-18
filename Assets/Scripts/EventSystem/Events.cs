using UnityEngine;

public static class Events
{
    public static readonly EventActions OnPlayerTurn = new EventActions();
    public static readonly EventActions OnPriestTurn = new EventActions();
    public static readonly EventActions OnDisplayPriestHealth = new EventActions();
    public static readonly EventActions OnDisplayPlayerHealth = new EventActions();
    public static readonly EventActions OnDisplayPlayerDefence = new EventActions();
    public static readonly EventActions OnDestroyCards = new EventActions();
    public static readonly EventActions OnInstantiateCards = new EventActions();
    public static readonly EventActions OnPlayerAnimations = new EventActions();
    public static readonly EventActions OnPriestAnimations = new EventActions();
    public static readonly EventActions OnGameEnd = new EventActions();
}