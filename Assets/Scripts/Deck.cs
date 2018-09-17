using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStackView))]
public class Deck : MonoBehaviour {

  public Sprite cardBack;
  public Sprite[] faces;

  private CardStack cardStack;

  void Awake() 
  {
    cardStack = GetComponent<CardStack>();
    cardStack.setCardModels(CreateDeck());
    cardStack.ShuffleCards();
  }

  void Start() 
  {
    
  }

  private List<CardModel> CreateDeck()
  {
    List<CardModel> cardModels = new List<CardModel>();
    for (int i = 0; i < 52; i++) {
			cardModels.Add(new CardModel(i, faces[i], cardBack));
		}
    return cardModels;
  }

}
