using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlaceholderCard : Card
{
    public PlaceholderCard ()
    {
            cardName = "Placeholder Card";
            cardArt = null;
            overrideRuleText = "This is just a placeholder.\nIt does nothing.";
            Effects = new List<CardEffect>();
    }
}