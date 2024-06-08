using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [field: SerializeField] public int Id { get; private set; }
    
    [SerializeField] private Image cardIcon;
    [SerializeField] private Button button;
    [SerializeField] private GameObject cardBack;
    [SerializeField] private bool isSelected;

    private Action<CardView> OnCardSelected;

    private IEnumerator onWrongSelectionCoroutine;
    private IEnumerator onMatchFoundCoroutine;
    
    public void SetCard(int id, Sprite icon, Action<CardView> cardViewSelected)
    {
        Id = id;
        cardIcon.sprite = icon;
        
        OnCardSelected = cardViewSelected;
        
        isSelected = false;
        UpdateCardUI();
    }
    
    public void OnClick()
    {
        if (!isSelected)
            SelectCard();
    }

    private void SelectCard()
    {
        isSelected = true;
        OnCardSelected?.Invoke(this);
        UpdateCardUI();
    }
    
    private void DeselectCard()
    {
        isSelected = false;
        UpdateCardUI();
    }

    public void HideOnWrongSelection()
    {
        if(!isSelected) return;
        if (onWrongSelectionCoroutine != null)
        {
            StopCoroutine(onWrongSelectionCoroutine);
        }

        onWrongSelectionCoroutine = WrongSelectionEffect();
        StartCoroutine(onWrongSelectionCoroutine);
    }
    
    private IEnumerator WrongSelectionEffect()
    {
        SetButtonInteractable(false);
        yield return new WaitForSeconds(1.5f);
        DeselectCard();
        SetButtonInteractable(true);
    }

    public void SetButtonInteractable(bool value)
    {
        button.interactable = value;
    }


    //it will Disable the Panel, Which hides the card
    private void UpdateCardUI() => cardBack.SetActive(!isSelected);
       
        
}
