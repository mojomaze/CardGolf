using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel {
    public Sprite cardFace;
    public Sprite cardBack;
    public int cardIndex;
    public bool isFaceUp = false;

    public CardModel(int cardIndex, Sprite cardFace, Sprite cardBack)
    {
      this.cardIndex = cardIndex;
      this.cardFace = cardFace;
      this.cardBack = cardBack;
    }

}