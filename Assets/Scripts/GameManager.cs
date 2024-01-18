using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState _state;

    private void Awake()
    {
        Instance = this;
    }

    
    
    public GameState CurrentState => _state;

    

    private void Start()
    {
        
        ChangeState(GameState.PlayerTurn);
    }
    
    
    public void ChangeState(GameState newState)
    {
        _state = newState;
        Debug.Log($"Game state changed to {newState}");
        switch (newState)
        {
            case GameState.PlayerTurn:
                Events.OnInstantiateCards.Invoke();
                Events.OnPlayerTurn.Invoke();
                break;
            case GameState.PriestTurn:
                Events.OnDestroyCards.Invoke();
                Events.OnPriestTurn.Invoke();
                break;
            case GameState.End:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    PlayerTurn,
    PriestTurn,
    End
}