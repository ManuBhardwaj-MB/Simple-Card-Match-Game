using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    public Action<int> OnMatchFoundValueChanged;
    public Action<int> OnLocalScoreValueChanged;
    public Action<int> OnTurnLeftValueChanged;
    
    public int RequiredCardPerMatchCount { get; private set; }
    private int matchFoundedCount;
    private int localScore;
    private int turnLeft;
    private int totalMatchesRequired;
    
    [SerializeField] private GameView gameView;
    [SerializeField] private List<CardView> selectedCardviews;
    
    public int MatchFoundedCount
    {
        get => matchFoundedCount;
        private set
        {
            OnMatchFoundValueChanged?.Invoke(value);
            matchFoundedCount = value;
        }
    }

    public int LocalScore
    {
        get => localScore;
        private set
        {
            OnLocalScoreValueChanged?.Invoke(value);
            localScore = value;
        }
    }

    public int TurnLeft
    {
        get => turnLeft;
        private set
        {
            OnTurnLeftValueChanged?.Invoke(value);
            turnLeft = value;
        }
    }
    
    private void Awake() => Instance ??= this;
    
    public void LoadNewLevel(Level level)
    {
        GenerateLevel(level);
    }
    
    private void GenerateLevel(Level levelToLoad)
    {
        SetupLevelSettings(levelToLoad);
        gameView.ResetGameplayPanel();
        
        var level = new Dictionary<int, Sprite>();
        var levelData = levelToLoad.levelImages;
        
        for (int i = 0; i < levelData.Length; i++)
        {
            level.Add(i, levelData[i]);
        }
        
        gameView.SpawnLevel(level);
    }

    private void SetupLevelSettings(Level levelToLoad)
    {
        selectedCardviews.Clear();
        totalMatchesRequired = levelToLoad.levelImages.Length;
        TurnLeft = levelToLoad.MaxTurnsCount;
        LocalScore = 0;
        MatchFoundedCount = 0;
        RequiredCardPerMatchCount = levelToLoad.GetRequiredMatchesCount;
        
        OnMatchFoundValueChanged?.Invoke(MatchFoundedCount);
        OnLocalScoreValueChanged?.Invoke(LocalScore);
        OnTurnLeftValueChanged?.Invoke(TurnLeft);
    }
    
    public bool SelectCard(CardView cardView)
    {
        if (selectedCardviews.Equals(null))
            selectedCardviews = new List<CardView>();

        if (selectedCardviews.Count.Equals(0))
        {
            SoundManager.Instance.PlayFlipSound();
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
        SoundManager.Instance.PlayMissMatchSound();
        RemoveAllCards();
        return false;
    }

    public void ValidateSelectedCards()
    {
        if (selectedCardviews.Count != RequiredCardPerMatchCount)
        {
            SoundManager.Instance.PlayFlipSound();
            return;
        }
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
            SoundManager.Instance.PlayMatchSound();
            MatchFoundedCount++;
            var foundedCards = selectedCardviews.ToArray();
            TurnUsedAndClearCards();
            gameView.MatchFounded(foundedCards);

            if (IsWinning())
            {
                SoundManager.Instance.PlayGameWonSound();
                GameManager.Instance.SwitchPanel(GameState.GameWon);
            }
        }
    }

    private bool IsWinning()
    {
        var isWinning = MatchFoundedCount >= totalMatchesRequired;
        return isWinning;
    }

    private void TurnUsedAndClearCards()
    {
        if(turnLeft>0)
            TurnLeft--;
        
        selectedCardviews.Clear();

        if (turnLeft <= 0 && !IsWinning())
        {
            SoundManager.Instance.PlayGameOverSound();
            GameManager.Instance.SwitchPanel(GameState.GameOver);
        }
    }
    
    private void RemoveAllCards()
    {
        RemoveAllCardsFromSelection();
        TurnUsedAndClearCards();
    }
    
    private void RemoveAllCardsFromSelection()
    {
        foreach (var cardView in selectedCardviews)
        {
            if(cardView != null)
                cardView.HideOnWrongSelection();
        }
    }
}
