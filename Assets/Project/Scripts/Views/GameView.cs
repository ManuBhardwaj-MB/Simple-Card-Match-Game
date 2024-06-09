using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private Transform contentHolder;
    [SerializeField] private CardView cardPrefab; 
    [SerializeField] private AutoGridSizer autoGridSizer;
    [SerializeField] private List<CardView> loadedCards;
    private List<IEnumerator> matchFoundedEffectCoroutine;
    
    public void ResetGameplayPanel()
    {
        if (loadedCards != null && loadedCards.Count > 0)
        {
            foreach (var cardView in loadedCards)
            {
                if (cardView != null)
                    Destroy(cardView.gameObject);
            }
        }
        loadedCards = new List<CardView>();
    }
    
    public void SpawnLevel(Dictionary<int, Sprite> levelData)
    {
        var listOfCards = new List<CardData>();
        for (int i = 0; i < levelData.Count; i++)
        {
            for (int x = 0; x < GameController.Instance.RequiredCardPerMatchCount; x++)
            {
                listOfCards.Add(new CardData(i,levelData[i]));
            }
        }
        
        for (int i = 0; i < listOfCards.Count; i++)
        {
            CardView card1 = Instantiate(cardPrefab, contentHolder);
            card1.SetCard(listOfCards[i].CardID, listOfCards[i].CardSprite, SelectCard);
            loadedCards.Add(card1);
        }

        autoGridSizer.ResetAndUpdateGridLayout();
    }
    
    private void SelectCard(CardView selectedCard)
    {
       var isSuccessfullyAdded = GameController.Instance.SelectCard(selectedCard);
       if (isSuccessfullyAdded)
       {
           GameController.Instance.ValidateSelectedCards();
       }
    }

    public void MatchFounded(CardView[] foundedCards)
    {
        if (matchFoundedEffectCoroutine == null)
            matchFoundedEffectCoroutine = new List<IEnumerator>();
        
        bool emptyCoroutineFounded = false;
        for (int i = 0; i < matchFoundedEffectCoroutine.Count; i++)
        {
            if (matchFoundedEffectCoroutine[i] == null)
            {
                emptyCoroutineFounded = true;
                matchFoundedEffectCoroutine[i] = MatchFoundedEffect(foundedCards);
                StartCoroutine(MatchFoundedEffect(foundedCards));
                break;
            }
        }

        if (!emptyCoroutineFounded)
        {
            IEnumerator newMatchFoundedEffectCoroutine = MatchFoundedEffect(foundedCards);
            matchFoundedEffectCoroutine.Add(newMatchFoundedEffectCoroutine);
            StartCoroutine(MatchFoundedEffect(foundedCards));
        }
    }
    
    private IEnumerator MatchFoundedEffect(CardView[] foundedCards)
    {
        foreach (var cardView in foundedCards)
        {
            cardView.SetButtonInteractable(false);
        }
        yield return new WaitForSeconds(1.5f);
        foreach (var cardView in foundedCards)
        {
            Destroy(cardView.gameObject, 0.5f);
        }
    }

    public void BackToMainMenu()
    {
        if (matchFoundedEffectCoroutine != null)
        {
            for (int i = 0; i < matchFoundedEffectCoroutine.Count; i++)
            {
                if (matchFoundedEffectCoroutine[i] != null)
                {
                    StopCoroutine(matchFoundedEffectCoroutine[i]);
                }
            }

            matchFoundedEffectCoroutine.Clear();
        }

        ResetGameplayPanel();
        GameManager.Instance.SwitchPanel(GameState.MainMenu);
    }
}
