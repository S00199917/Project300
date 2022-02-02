using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to assign the values of each of the cards
/// </summary>
[System.Serializable]
public class Values
{
    public enum Faces
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public enum Suits
    {
        Heart,
        Diamond,
        Spade,
        Club
    }

    public UnityEngine.GameObject card;
    public Faces Face;
    public Suits Suit;
}
