using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardHolder : MonoBehaviour
{
    [SerializeField] private Card basicAttackCard;
    [SerializeField] private Card basicHealCard;
    [SerializeField] private Card basicDefenceCard;
    [SerializeField] private Card strongAttackCard;
    [SerializeField] private Card strongHealCard;
    [SerializeField] private Card strongDefenceCard;
    [SerializeField] private Transform cardHolder0;
    [SerializeField] private Transform cardHolder1;
    [SerializeField] private Transform cardHolder2;
    private List<Card> _cards;
    private List<Transform> _cardHolders;

    private void OnEnable()
    {
        Events.OnInstantiateCards.AddListener(InstantiateCardRandomly);
        Events.OnDestroyCards.AddListener(DestroyCards);
    }

    private void OnDestroy()
    {
        Events.OnInstantiateCards.RemoveListener(InstantiateCardRandomly);
        Events.OnDestroyCards.RemoveListener(DestroyCards);
    }

    private void Awake()
    {
        // Create a list of cards where basic cards are repeated more times than strong cards
        _cards = new List<Card>()
        {
            basicAttackCard, basicAttackCard, basicAttackCard, basicAttackCard, basicAttackCard,
            basicHealCard, basicHealCard, basicHealCard, basicHealCard, basicHealCard,
            basicDefenceCard, basicDefenceCard, basicDefenceCard, basicDefenceCard, basicDefenceCard,
            strongAttackCard, // less chance to get strong cards
            strongHealCard, // less chance to get strong cards
            strongDefenceCard // less chance to get strong cards
        };
        _cardHolders = new List<Transform>()
        {
            cardHolder0,
            cardHolder1,
            cardHolder2,
        };
    }
    
    private void InstantiateCardRandomly()
    {
        Debug.Log("Instantiating cards randomly");
        // Generate six random indices
        var randomIndices = new int[3];
        for (var i = 0; i < 3; i++)
        {
            randomIndices[i] = Random.Range(0, _cards.Count);
        }
        // Shuffle the list of card holders
        for (var i = 0; i < _cardHolders.Count; i++)
        {
            var temp = _cardHolders[i];
            var randomIndex = Random.Range(i, _cardHolders.Count);
            _cardHolders[i] = _cardHolders[randomIndex];
            _cardHolders[randomIndex] = temp;
        }

        // Select six cards using the random indices and instantiate them
        for (var i = 0; i < 3; i++)
        {
            var selectedCard = _cards[randomIndices[i]];

            // Instantiate the selected card in the shuffled card holder
            Instantiate(selectedCard, _cardHolders[i]);
        }
    }
    private void DestroyCards()
    {
        foreach (var cardHolder in _cardHolders)
        {
            foreach (Transform child in cardHolder)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
