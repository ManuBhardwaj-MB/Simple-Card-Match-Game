using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private Transform contentHolder;
    [SerializeField] private CardView cardPrefab; 
    [SerializeField] private AutoGridSizer autoGridSizer;
    [SerializeField] private List<CardView> loadedCards;
    private GameController gameController;
    
    public void Setup(GameController controller)
    {
        gameController ??= controller;
        loadedCards = new List<CardView>();
    }
    
    public void SpawnLevel(Dictionary<int, Sprite> levelData)
    {
        for (int i = 0; i < levelData.Count; i++)
        {
            CardView card1 = Instantiate(cardPrefab, contentHolder);
            card1.SetCard(i, levelData[i], SelectCard);
            loadedCards.Add(card1);
            
            CardView card2 = Instantiate(cardPrefab, contentHolder);
            card2.SetCard(i, levelData[i], SelectCard);
            loadedCards.Add(card2);
        }
        autoGridSizer.UpdateLayout();
    }
    
    private void SelectCard(CardView selectedCard)
    {
       var isSuccessfullyAdded = gameController.SelectCard(selectedCard);
       if (isSuccessfullyAdded)
       {
           gameController.ValidateSelectedCards();
       }
    }

    public void MatchFounded(CardView[] foundedCards)
    {
        StartCoroutine(MatchFoundedEffect(foundedCards));
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
}
