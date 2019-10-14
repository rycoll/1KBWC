using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : ByteManager
{
    private static GameController game;
    private GameMaster gameMaster;
    public ReadCallback skipToNext;

    public Interpreter (GameController gc, GameMaster gm) {
        game = gc;
        gameMaster = gm;
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
                                    compiled.Insert(0, pop());
                                }
                            } else if (currentByte == (byte) Instruction.PLACEHOLDER) {
                                int placeholderID = ReadIntLiteral(skipToNext);
                                  if (placeholderID == ID) {
                                    compiled.AddRange(new List<byte>(items[i]));
                                } else {
                                    compiled.Add((byte) Instruction.PLACEHOLDER);
                                    compiled.AddRange(new List<byte>(LiteralFactory.CreateIntLiteral(placeholderID)));
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
                    push(LiteralFactory.CreatePlayerLiteral(game.GetActivePlayer()));
                    break;
                }

                case Instruction.GET_ALL_OPPONENTS: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<GamePlayer>(game.GetOpponents())
                    ));
                    break;
                }

                case Instruction.GET_ALL_PLAYERS: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<GamePlayer>(game.GetPlayers())
                    ));
                    break;
                }

                case Instruction.GET_CARDS_IN_DECK: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<Card>(game.Deck.GetCards())
                    ));
                    break;
                }

                case Instruction.GET_CARDS_IN_DISCARD: {
                    push(LiteralFactory.CreateListLiteral(
                        new List<Card>(game.Discard.GetCards())
                    ));
                    break;
                }
                
                case Instruction.GET_CARDS_IN_HAND: {
                    GamePlayer player = gameMaster.ReadPlayerFromStack();
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
                    GamePlayer player = gameMaster.ReadPlayerFromStack();
                    int points = player.Points;
                    push(LiteralFactory.CreateIntLiteral(points));
                    break;
                }

                case Instruction.READ_COUNTER: {
                    string key = ReadStringLiteral(skipToNext);
                    int count = game.Variables.GetCounter(key);
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
                    game.PresentChoiceOfPlayers(new List<GamePlayer>(game.GetPlayers()));
                    break;
                }

                case Instruction.TARGET_CARD: {
                    game.PresentChoiceOfCards(new List<Card>(game.Table.GetCards()));
                    break;
                }

                // EFFECTS

                case Instruction.INCREMENT_PLAYER_POINTS: {
                    GamePlayer player = gameMaster.ReadPlayerFromStack();
                    int pointsNum = ReadIntLiteral(skipToNext);
                    game.SetPlayerPoints(player, player.Points + pointsNum);
                    break;
                }

                case Instruction.PLAYER_DRAW_CARD: {
                    GamePlayer player = gameMaster.ReadPlayerFromStack();
                    int numCards = ReadIntLiteral(skipToNext);
                    for (int n = 0; n < numCards;) {
                        game.PlayerDrawCard(player);
                    }
                    break;
                }

                case Instruction.SET_COUNTER: {
                    string key = ReadStringLiteral(skipToNext);
                    int count = ReadIntLiteral(skipToNext);
                    game.SetCounter(key, count);
                    break;
                }

                case Instruction.SET_PLAYER_DRAW: {
                    GamePlayer player = gameMaster.ReadPlayerFromStack();
                    int num = ReadIntLiteral(skipToNext);
                    player.SetDrawPerTurn(num);
                    break;
                }

                case Instruction.SET_PLAYER_MAX_HAND: {
                    GamePlayer player = gameMaster.ReadPlayerFromStack();
                    int num = ReadIntLiteral(skipToNext);
                    //player.Hand.MaxHandSize = num;
                    break;
                }
                
                case Instruction.SET_PLAYER_POINTS: {
                    GamePlayer player = gameMaster.ReadPlayerFromStack();
                    int pointsNum = ReadIntLiteral(skipToNext);
                    game.SetPlayerPoints(player, pointsNum);
                    break;
                }

                case Instruction.MOVE_TO_DECK: {
                    Card card = gameMaster.ReadCardFromStack();
                    DeckLocation posEnum = (DeckLocation) ReadEnumLiteral();
                    card.Zone.MoveCard(game.Deck, card.GetID());
                    game.Deck.MoveLastAddedCard(posEnum);
                    break;
                }

                case Instruction.MOVE_TO_DISCARD: {
                    Card card = gameMaster.ReadCardFromStack();
                    card.Zone.MoveCard(game.Discard, card.GetID());
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
            game.AddToStack(player);
        }
        next();
    }  

    public void CardChoiceCallback (Card chosenCard) {
        if (chosenCard != null) {
            byte[] card = LiteralFactory.CreateCardLiteral(chosenCard.GetID());
            game.AddToStack(card);
        }
        next();
    }  
}
