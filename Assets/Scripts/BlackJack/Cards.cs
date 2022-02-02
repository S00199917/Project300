using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public string Face { get; set; }
    public string Suit { get; set; }

    public Cards()
    {

    }

    public Cards(string face, string suit)
    {
        Face = face;
        Suit = suit;
    }
    public override string ToString()
    {
        return Face + " of " + Suit;
    }
}
