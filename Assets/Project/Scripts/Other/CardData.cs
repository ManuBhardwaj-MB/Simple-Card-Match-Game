using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [field: SerializeField] public int CardID { get; private set; }
    [field: SerializeField] public Sprite CardSprite { get; private set; }

    public CardData(int id, Sprite sprite)
    {
        CardID = id;
        CardSprite = sprite;
    }
}

