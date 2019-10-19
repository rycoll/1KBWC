using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : ByteManager
{
    private GameMaster GM;
    public ReadCallback skipToNext;

    public Interpreter (GameMaster gm) {
        GM = gm;
        skipToNext = executeNext;
    }

    public void executeNext () {
        Instruction next = this.next();
        try {
            switch(next) {

                // FUNCTIONS

                case Instruction.RANDOM_NUMBER: {
                    int upperBound = ReadIntLiteral(skipToNext);
                    push(LiteralFactory.CreateIntLiteral(UnityEngine.Random.Range(1, upperBound)));
                    break;
                }

                case Instruction.ADD: {
                    int a = ReadIntLiteral(skipToNext);
                    int b = ReadIntLiteral(skipToNext);
                    push(LiteralFactory.CreateIntLiteral(a + b));
                    break;
                }

                case Instruction.IF: {
                    Condition condition = ReadConditionLiteral(skipToNext);
                    if (!condition.Evaluate()) {
                        while (currentStackSize > 0 && peek() != (byte) Instruction.ENDIF) {
                            pop();
                        }
                    }
                    break;
                }

                case Instruction.LIST_LENGTH: {	
                    List<byte[]> list = ReadList(skipToNext);	
                    push(LiteralFactory.CreateIntLiteral(list.Count));	
                    break;	
                }

                case Instruction.CARD_HAS_TAG: {
                    Card card = GM.ReadCardFromStack();
                    string tagName = ReadStringLiteral(skipToNext);
                    push(LiteralFactory.CreateBoolLiteral(
                        card.HasTag(tagName)
                    ));
                    break;	
                }

                case Instruction.MULTIPLY: {
                    int a = ReadIntLiteral(skipToNext);
                    int b = ReadIntLiteral(skipToNext);
                    push(LiteralFactory.CreateIntLiteral(a * b));
                    break;
                }

                case Instruction.LOOP: {
                    int num = ReadIntLiteral(skipToNext);
                    List<byte> bytes = new List<byte>();
                    while (currentStackSize > 0) {
                        byte b = pop();
                        if (b == (byte) Instruction.ENDLOOP) {
                            break;
                        }
                        // add to start, to retain stack ordering
                        bytes.Insert(0, b);
                    }
                    byte[] byteArr = bytes.ToArray();
                    for (int n = 0; n < num; n++) {
                        push(byteArr);
                    }
                    break;
                }

                case Instruction.FOR_LOOP: {
                    int ID = ReadIntLiteral(skipToNext);
                    List<byte[]> items = ReadList(skipToNext);
                    items.Reverse();
                    
                    List<byte> bytestring = new List<byte>();
                    while (peek() != (byte) Instruction.ENDLOOP) {
                        // add to start, to retain stack ordering
                        bytestring.Insert(0, pop());
                    }
                    bytestring.Insert(0, (byte) Instruction.ENDLOOP);

                    List<byte> compiled = new List<byte>();
                    for (int i = 0; i < items.Count; i++) {
                        byte[] currentItem = items[i];
                        push(bytestring.ToArray());
                        byte currentByte = pop();
                        while (currentByte != (byte) Instruction.ENDLOOP) {
                            if (currentByte == (byte) Instruction.CHUNK) {
                                int chunkSize = ReadIntLiteral(skipToNext);
                                for (int n = 0; n < chunkSize; n++) {
                                    // add to start, to retain stack ordering
                                    compiled.Insert(n, pop());
                                }
                            } else if (currentByte == (byte) Instruction.PLACEHOLDER) {
                                int placeholderID = ReadIntLiteral(skipToNext);
                                  if (placeholderID == ID) {
                                    byte[] placeholder = (byte[]) items[i].Clone();
                                    Array.Reverse(placeholder);
                                    compiled.InsertRange(0, new List<byte>(placeholder));
                                } else {
                                    byte[] replaceID = LiteralFactory.CreateIntLiteral(placeholderID);
                                    Array.Reverse(replaceID);
                                    compiled.InsertRange(0, new List<byte>(replaceID));
                                    compiled.Insert(0, (byte) Instruction.PLACEHOLDER);
                                }
                            } else {
                                throw new UnexpectedByteException("Expected CHUNK or PLACEHOLDER, found " +  currentByte);
                            }
                            currentByte = pop();
                        }
                    }
                    
                    push(compiled.ToArray());
                    break;
                }
                
                // QUERIES

                case Instruction.GET_ACTIVE_PLAYER: {
                    push(LiteralFactory.CreatePlayerLiteral(GM.Players.GetActivePlayer()));
                    break;
                }

                case Instruction.GET_ALL_OPPONENTS: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<GamePlayer>(GM.Players.GetOpponents())
                    ));
                    break;
                }

                case Instruction.GET_ALL_PLAYERS: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<GamePlayer>(GM.Players.GetPlayers())
                    ));
                    break;
                }

                case Instruction.GET_CARDS_IN_DECK: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<Card>(GM.Cards.Deck.GetCards())
                    ));
                    break;
                }

                case Instruction.GET_CARDS_IN_DISCARD: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<Card>(GM.Cards.Discard.GetCards())
                    ));
                    break;
                }
                
                case Instruction.GET_CARDS_IN_HAND: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    push(LiteralFactory.CreateListLiteral(
                        new List<Card>(player.Hand.GetCards())
                    ));
                    break;
                }

                case Instruction.GET_PLAYER : {
                    int id = ReadIntLiteral(skipToNext);
                    push(LiteralFactory.CreatePlayerLiteral(id));
                    break;
                }

                case Instruction.GET_PLAYER_POINTS: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    int points = player.Points;
                    push(LiteralFactory.CreateIntLiteral(points));
                    break;
                }

                case Instruction.READ_COUNTER: {
                    string key = ReadStringLiteral(skipToNext);
                    int count = GM.Variables.GetCounter(key);
                    push(LiteralFactory.CreateIntLiteral(count));
                    break;
                }

                case Instruction.BOOL_COMPARISON: {
                    bool operandA = ReadBoolLiteral(skipToNext);
                    bool operandB = ReadBoolLiteral(skipToNext);
                    byte operatorEnum = ReadEnumLiteral();
                    push(LiteralFactory.CreateConditionLiteral(
                        new CompareBool(operandA, operandB, (ConditionOperator) operatorEnum)
                    ));
                    break;
                }

                case Instruction.NUM_COMPARISON: {
                    int operandA = ReadIntLiteral(skipToNext);
                    int operandB = ReadIntLiteral(skipToNext);
                    byte operatorEnum = ReadEnumLiteral();
                    push(LiteralFactory.CreateConditionLiteral(
                        new CompareNum(operandA, operandB, (ConditionOperator) operatorEnum)
                    ));
                    break;
                }

                case Instruction.TARGET_PLAYER: {
                    GM.UI.PresentChoiceOfPlayers(new List<GamePlayer>(GM.Players.GetPlayers()), this);
                    break;
                }

                case Instruction.TARGET_CARD: {
                    GM.UI.PresentChoiceOfCards(new List<Card>(GM.Cards.Table.GetCards()), this);
                    break;
                }

                // EFFECTS
 
                case Instruction.INCREMENT_PLAYER_POINTS: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    int pointsNum = ReadIntLiteral(skipToNext);
                    GM.SetPlayerPoints(player, player.Points + pointsNum);
                    break;
                }

                case Instruction.PLAYER_DRAW_CARD: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    int numCards = ReadIntLiteral(skipToNext);
                    for (int n = 0; n < numCards; n++) {
                        GM.PlayerDrawCard(player);
                    }
                    break;
                }

                case Instruction.SET_COUNTER: {
                    string key = ReadStringLiteral(skipToNext);
                    int count = ReadIntLiteral(skipToNext);
                    GM.Variables.SetCounter(key, count);
                    break;
                }

                case Instruction.SET_PLAYER_DRAW: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    int num = ReadIntLiteral(skipToNext);
                    player.SetDrawPerTurn(num);
                    break;
                }

                case Instruction.SET_PLAYER_MAX_HAND: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    int num = ReadIntLiteral(skipToNext);
                    player.Hand.SetMax(num);
                    break;
                }
                
                case Instruction.SET_PLAYER_POINTS: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    int pointsNum = ReadIntLiteral(skipToNext);
                    GM.SetPlayerPoints(player, pointsNum);
                    break;
                }

                case Instruction.MOVE_TO_DECK: {
                    Card card = GM.ReadCardFromStack();
                    DeckLocation posEnum = (DeckLocation) ReadEnumLiteral();
                    card.Zone.MoveCard(GM.Cards.Deck, card.GetID());
                    GM.Cards.Deck.MoveLastAddedCard(posEnum);
                    break;
                }

                case Instruction.MOVE_TO_DISCARD: {
                    Card card = GM.ReadCardFromStack();
                    card.Zone.MoveCard(GM.Cards.Discard, card.GetID());
                    break;
                }
            }
        } catch (UnexpectedByteException e) {
            Debug.LogError(e);
        } catch (StackFullException e) {
            Debug.LogError(e);
        } catch (StackEmptyException e) {
            Debug.LogError(e);
        }
    }

    public void PlayerChoiceCallback (GamePlayer chosenPlayer) {
        if (chosenPlayer != null) {
            byte[] player = LiteralFactory.CreatePlayerLiteral(chosenPlayer);
            GM.AddToStack(player);
        }
        next();
    }  

    public void CardChoiceCallback (Card chosenCard) {
        if (chosenCard != null) {
            byte[] card = LiteralFactory.CreateCardLiteral(chosenCard.GetID());
            GM.AddToStack(card);
        }
        next();
    }  
}
