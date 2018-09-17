using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour {

  private CardStack cardStack;
  private Dictionary<int, Card> cards;
  private int lastCount = 0;

	public Vector3 start;
  public int cardRotation;
	public float cardOffset;
  public Card cardPrefab;
  public bool reverseLayerOrder;

  void Awake()
  {
    cards = new Dictionary<int, Card>();
    cardStack = GetComponent<CardStack>();

    cardStack.CardAddedEvent += CardStack_CardAdded;
    cardStack.CardRemovedEvent += CardStack_CardRemoved;
    cardStack.CardFlipEvent += CardStack_FlipCard;
    cardStack.CardStageEvent += CardStack_StageCard;
    cardStack.CardMoveToStackEvent += CardStack_MoveCardToStack;
  }

  void Start() {
    ShowCards();
  }

  void Update()
  {
    if (lastCount != cardStack.CardCount) {
      lastCount = cardStack.CardCount;
      ShowCards();
    }
  }

  void CardStack_CardAdded(object sender, CardEventArgs e)
  {
    lastCount = cardStack.CardCount;
    float co = cardOffset * cardStack.CardCount;
    AddCard(co, e.cardModel, cardStack.CardCount);
  }

  void CardStack_CardRemoved(object sender, CardEventArgs e)
  {
    lastCount = cardStack.CardCount;
    int index = e.cardModel.cardIndex;
    if (cards.ContainsKey (index)) {
			Destroy(cards[index].gameObject);
			cards.Remove (index);
      ShowCards();
		}
  }

  void CardStack_FlipCard(object sender, CardEventArgs e)
  {
    if (cards.ContainsKey(e.cardModel.cardIndex)) {
      CardFlipper cardFlipper = cards[e.cardModel.cardIndex].GetComponent<CardFlipper>();
      cardFlipper.FlipCard(e.cardModel);
    }
  }

  void CardStack_StageCard(object sender, CardStageEventArgs e)
  {
    if (cards.ContainsKey(e.cardModel.cardIndex)) {
      CardMover cardMover = cards[e.cardModel.cardIndex].GetComponent<CardMover>();
      cardMover.MoveCard(e.position);

      if (e.flip) {
        CardFlipper cardFlipper = cards[e.cardModel.cardIndex].GetComponent<CardFlipper>();
        cardFlipper.FlipCard(e.cardModel);
      }
    }
  }

  void CardStack_MoveCardToStack(object sender, CardMoveToStackEventArgs e)
  {
    if (cards.ContainsKey(e.cardModel.cardIndex)) {
      CardMover cardMover = cards[e.cardModel.cardIndex].GetComponent<CardMover>();
      cardMover.CardMoveToStackEvent += CardMover_MovedCardToStack;
      cardMover.MoveCardToStack(e);

      if (e.flip) {
        CardFlipper cardFlipper = cards[e.cardModel.cardIndex].GetComponent<CardFlipper>();
        cardFlipper.FlipCard(e.cardModel);
      }
    }
  }

  void CardMover_MovedCardToStack(object sender, CardMoveToStackEventArgs e) {
    cardStack.finishedMovingToStack(e);
    CardMover cardMover = sender as CardMover;
    if (cardMover != null) {
      cardMover.CardMoveToStackEvent -= CardMover_MovedCardToStack;
    }
  }

  public Vector3 CardPositionForIndex(int index) {
     float co = cardOffset * (index + 1);
     return CardPosition(co);
  }

  void ShowCards() {
		if (cardStack.CardCount == 0) {
			return;
		}

		int cardCount = 0;
		foreach (CardModel cardModel in cardStack.GetCards()) {
      if (cards.ContainsKey (cardModel.cardIndex)) {
			  continue;
		  }

			float co = cardOffset * cardCount;
      AddCard (co, cardModel, cardCount);
			cardCount++;
		}		
	}

	private void AddCard(float cardOffset, CardModel cardModel, int positionalIndex) {	
		Card cardCopy = (Card)Instantiate (cardPrefab);
		cardCopy.transform.position = CardPosition(cardOffset);
    cardCopy.transform.rotation = Quaternion.Euler(90.0f, 0.0f, cardRotation);

		cardCopy.cardModel = cardModel;

    int sortingOrder = reverseLayerOrder ? cardStack.CardCount - positionalIndex : positionalIndex;

		cardCopy.ToggleFace(cardModel.isFaceUp, sortingOrder);

		cards.Add(cardModel.cardIndex, cardCopy);
	}

  private Vector3 CardPosition(float cardOffset)
  {
    Vector3 position;
    switch(cardRotation) {
      case 90:
        position = start + new Vector3 (0f, 0f, -cardOffset);
        break;

      case 180:
        position = start + new Vector3 (-cardOffset, 0f);
        break;

      case 270:
        position = start + new Vector3 (0f, 0f, cardOffset);
        break;

      default:
        position = start + new Vector3(cardOffset, 0f);
        break;
    }
    return position;
  }

}