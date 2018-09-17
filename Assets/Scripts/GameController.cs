using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  public CardStack deck;
  public CardStack discard;
  public CardStack player;
  public CardStack botLeft;
  public CardStack botTop;
  public CardStack botRight;
  public MarkerMover turnMarker;

  private CardStack currentPlayer;
  private CardModel stagedCard;
  private bool playersCardsHidden = false;

  private enum GameState 
  {
    None,
    Ready,
    Staged
  }

  private GameState playerClickState = GameState.None;

  private 

  #region Unity Messages

  void Awake()
  {
    CardClicker cardClicker = GetComponent<CardClicker>();
    cardClicker.CardClickedEvent += CardClicker_CardClicked;
  }

  void Start()
  {
    StartGame();
  }

  #endregion

  void CardClicker_CardClicked(object sender, CardEventArgs e) {
    switch (playerClickState) {
      case GameState.None:
        return;
        
      case GameState.Ready:
        if (deck.HasCard(e.cardModel)) {
          stagedCard = e.cardModel;
          deck.StageCard(e.cardModel, turnMarker.transform.position, true);
          playerClickState = GameState.Staged;
          // if (!playersCardsHidden) {
          //   playersCardsHidden = true;
          //   player.flipCards(new RangeInt(0, 2), false);
          // }
        }
        return;

      case GameState.Staged:
        if (stagedCard != null && e.cardModel != stagedCard) {

          Vector3 discardPosition = discard.GetComponent<CardStackView>().start;
          
          if (deck.HasCard(e.cardModel)) // discard
          {
            deck.MoveCardToStack(
              new CardMoveToStackEventArgs(e.cardModel, discard, discardPosition, false, -1, -1)
            );
            stagedCard = null;
            playerClickState = GameState.None;
          } 
          else if (player.HasCard(e.cardModel)) // card swap
          {
            int indexOfCardClicked = player.IndexOf(e.cardModel);
            CardStackView stackView = player.GetComponent<CardStackView>();
            Vector3 cardClickedPosition = stackView.CardPositionForIndex(indexOfCardClicked);

            CardMoveToStackEventArgs playerArgs  = new CardMoveToStackEventArgs(
              e.cardModel, discard, discardPosition, false, indexOfCardClicked, -1
            );

            CardMoveToStackEventArgs deckArgs  = new CardMoveToStackEventArgs(
              stagedCard, player, cardClickedPosition, false, -1, indexOfCardClicked
            );

            //player.MoveCardToStack(playerArgs); // discard clicked card and flip
            deck.MoveCardToStack(deckArgs); // add staged card to user stack
            
            stagedCard = null;
            playerClickState = GameState.None;
          }
          
        }
        return;
    }
  }

  private void StartGame()
  {
    Deal();
  }

  private void Deal() {
    for (int i = 0; i < 4; i++) {
      player.Push(deck.Pop());
      botLeft.Push(deck.Pop());
      botTop.Push(deck.Pop());
      botRight.Push(deck.Pop());
    }

    player.flipCards(new RangeInt(0, 4), true);
    turnMarker.moveToPlayer(0);
    currentPlayer = player;
    playerClickState = GameState.Ready;

  }

}
