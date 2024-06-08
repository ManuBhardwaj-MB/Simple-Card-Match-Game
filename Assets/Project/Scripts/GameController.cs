using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Level exampleLevel;
    
    [SerializeField] private GameView gameView;
    [SerializeField] private List<CardView> selectedCardviews;
    
    private int requiredCardPerMatchCount;

    public bool SelectCard(CardView cardView)
    {
        if (selectedCardviews.Equals(null))
            selectedCardviews = new List<CardView>();

        if (selectedCardviews.Count.Equals(0))
        {
            selectedCardviews.Add(cardView);
            return true;
        }
        if (!selectedCardviews.Contains(cardView) 
            && selectedCardviews[0].Id.Equals(cardView.Id))
        {
            selectedCardviews.Add(cardView);
            return true;
        }
        selectedCardviews.Add(cardView);
        RemoveAllCards();
        return false;
    }

    public void ValidateSelectedCards()
    {
        if(selectedCardviews.Count != requiredCardPerMatchCount) return;
        bool matchFound = true;
        for (int i = 0; i < selectedCardviews.Count; i++)
        {
            if (!selectedCardviews[0].Id.Equals(selectedCardviews[i].Id))
            {
                matchFound = false;
                break;
            }
        }
        
        if (matchFound)
        {
            var foundedCards = selectedCardviews.ToArray();
            string log ="selectedCardViews Count is "+ selectedCardviews.Count;
            selectedCardviews.Clear();
            log += "\nselectedCardViews Count is after"+ selectedCardviews.Count;
            log += "\n FoundedCards Count is after"+ foundedCards.Length;
            gameView.MatchFounded(foundedCards);
            GameManager.Instance.Log(log);
        }
    }

    public bool RemoveAllCards()
    {
        foreach (var cardView in selectedCardviews)
        {
            cardView.HideOnWrongSelection();
        }
        selectedCardviews.Clear();
        return true;
    }
    
    [ContextMenu("Load Test Level")]
    public void GenerateExampleLevel()
    {
        GenerateLevel(exampleLevel);
    }
    
    private void GenerateLevel(Level levelToLoad)
    {
        gameView.Setup(this);
        requiredCardPerMatchCount = levelToLoad.GetRequiredMatchesCount;
        
        var level = new Dictionary<int, Sprite>();
        var levelData = levelToLoad.levelImages;
        
        for (int i = 0; i < levelData.Length; i++)
        {
            level.Add(i, levelData[i]);
        }
        
        gameView.SpawnLevel(level);
    }
    
    
}
