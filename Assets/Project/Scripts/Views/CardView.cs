using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [field: SerializeField] public int Id { get; private set; }
    
    [SerializeField] private Image cardIcon;
    [SerializeField] private Button button;
    
    private bool isShowing;
    
    public void SetCard(int id, Sprite icon)
    {
        Id = id;
        cardIcon.sprite = icon;
    }

    public void OnClick()
    {
        
    }
    
    public void FlipCard()
    {
        
    }
    
}
