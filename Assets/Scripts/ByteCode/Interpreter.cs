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

                case Instruction.UNLESS: {
                    Condition condition = ReadConditionLiteral(skipToNext);
                    if (condition.Evaluate()) {
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

                case Instruction.PLAYER_IS_WINNING: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    bool winning = true;
                    foreach (GamePlayer otherPlayer in GM.Players.GetPlayers()) {
                        if (otherPlayer.Points > player.Points) {
                            winning = false;
                            break;
                        }
                    }
                    push(LiteralFactory.CreateBoolLiteral(winning));
                    break;	
                }

                case Instruction.PLAYER_IS_LOSING: {
                    GamePlayer player = GM.ReadPlayerFromStack();
                    bool losing = true;
                    foreach (GamePlayer otherPlayer in GM.Players.GetPlayers()) {
                        if (otherPlayer.Points < player.Points) {
                            losing = false;
                            break;
                        }
                    }
                    push(LiteralFactory.CreateBoolLiteral(losing));
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
                    List<byte> bytestring = new List<byte>();
                    byte current = pop();
                    while (current != (byte) Instruction.ENDLOOP) {
                        // add to start, to retain stack ordering
                        bytestring.Insert(0, current);
                        current = pop();
                    }

                    byte[] idBytes = LiteralFactory.CreateIntLiteral(ID);
                    for (int i = 0; i < items.Count; i++) {
                        byte[] currentItem = items[i];
                        Array.Reverse(currentItem);
                        byte[] addToRegister = InstructionFactory.Make_AddToRegister(idBytes, currentItem);
                        push(bytestring.ToArray());
                        push(addToRegister);
                    }
                    break;
                }

                case Instruction.ADD_TO_REGISTER: {
                    int ID = ReadIntLiteral(skipToNext);
                    int size = ReadIntLiteral(skipToNext);
                    byte[] bytes = pop(size);
                    register[ID] = bytes;
                    break;
                }

                case Instruction.PLACEHOLDER: {
                    int ID = ReadIntLiteral(skipToNext);
                    byte[] fetch = register[ID];
                    Array.Reverse(fetch);
                    push(fetch);
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
