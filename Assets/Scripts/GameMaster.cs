using UnityEngine;
using System.Collections.Generic;

public class GameMaster {

    public ByteManager Bytes;
    public CardManager Cards;
    public GameFunctions Functions;
    public PlayerManager Players;
    public GameVariables Variables;
    
    public GameMaster () {
        Bytes = new ByteManager();
        Cards = new CardManager();
        Functions = new GameFunctions();
        Players = new PlayerManager(4);
        Variables = new GameVariables();
    }

    #region Card stuff

    public Card FindCardById (int id) {
        Card card = Cards.FindCardById(id);
        if (card == null) {
            card = Players.FindCardById(id);
        }
        return card;
    }

    #endregion

    #region Stack stuff

    public GamePlayer ReadPlayerFromStack () {
        int index = Bytes.ReadPlayerLiteral();
        return Players.GetPlayer(index);
    }

    public Card ReadCardFromStack () {
        int id = Bytes.ReadCardLiteral();
        return FindCardById(id);
    }

    #endregion

    #region Byte execution

    public void ExecuteByte (Instruction instruction) {
        try {
            switch(instruction) {

                #region FUNCTIONS

                case Instruction.RANDOM_NUMBER: {
                    int upperBound = Bytes.ReadIntLiteral();
                    Bytes.push(LiteralFactory.CreateIntLiteral(UnityEngine.Random.Range(1, upperBound)));
                    break;
                }

                case Instruction.ADD: {
                    int a = Bytes.ReadIntLiteral();
                    int b = Bytes.ReadIntLiteral();
                    Bytes.push(LiteralFactory.CreateIntLiteral(a + b));
                    break;
                }

                case Instruction.IF: {
                    Condition condition = Bytes.ReadConditionLiteral();
                    if (!condition.Evaluate()) {
                        while (Bytes.hasBytes() && Bytes.peek() != (byte) Instruction.ENDIF) {
                            Bytes.pop();
                        }
                    }
                    break;
                }

                case Instruction.MULTIPLY: {
                    int a = Bytes.ReadIntLiteral();
                    int b = Bytes.ReadIntLiteral();
                    Bytes.push(LiteralFactory.CreateIntLiteral(a * b));
                    break;
                }

                case Instruction.LOOP: {
                    int num = Bytes.ReadIntLiteral();
                    List<byte> bytes = new List<byte>();
                    while (Bytes.hasBytes()) {
                        byte b = Bytes.pop();
                        if (b == (byte) Instruction.ENDLOOP) {
                            break;
                        }
                        // add to start, to retain stack ordering
                        bytes.Insert(0, b);
                    }
                    byte[] byteArr = bytes.ToArray();
                    for (int n = 0; n < num; n++) {
                        Bytes.push(byteArr);
                    }
                    break;
                }

                case Instruction.FOR_LOOP: {
                    int ID = Bytes.ReadIntLiteral();
                    List<byte[]> items = Bytes.ReadList();
                    
                    List<byte> bytestring = new List<byte>();
                    while (Bytes.peek() != (byte) Instruction.ENDLOOP) {
                        // add to start, to retain stack ordering
                        bytestring.Insert(0, Bytes.pop());
                    }
                    bytestring.Insert(0, (byte) Instruction.ENDLOOP);

                    List<byte> compiled = new List<byte>();
                    for (int i = 0; i < items.Count; i++) {
                        byte[] currentItem = items[i];
                        Bytes.push(bytestring.ToArray());
                        byte currentByte = Bytes.pop();
                        while (currentByte != (byte) Instruction.ENDLOOP) {
                            if (currentByte == (byte) Instruction.CHUNK) {
                                int chunkSize = Bytes.ReadIntLiteral();
                                for (int n = 0; n < chunkSize; n++) {
                                    // add to start, to retain stack ordering
                                    compiled.Insert(0, Bytes.pop());
                                }
                            } else if (currentByte == (byte) Instruction.PLACEHOLDER) {
                                int placeholderID = Bytes.ReadIntLiteral();
                                  if (placeholderID == ID) {
                                    compiled.AddRange(new List<byte>(items[i]));
                                } else {
                                    compiled.Add((byte) Instruction.PLACEHOLDER);
                                    compiled.AddRange(new List<byte>(LiteralFactory.CreateIntLiteral(placeholderID)));
                                }
                            } else {
                                throw new UnexpectedByteException("Expected CHUNK or PLACEHOLDER, found " +  currentByte);
                            }
                            currentByte = Bytes.pop();
                        }
                    }
                    Bytes.push(compiled.ToArray());
                    break;
                }

                #endregion

                #region QUERIES

                case Instruction.GET_ACTIVE_PLAYER: {
                    Bytes.push(
                        LiteralFactory.CreatePlayerLiteral(
                            Players.GetActivePlayer()
                        )
                    );
                    break;
                }

                case Instruction.GET_PLAYER : {
                    int id = Bytes.ReadIntLiteral();
                    Bytes.push(
                        LiteralFactory.CreatePlayerLiteral(id)
                    );
                    break;
                }

                case Instruction.GET_PLAYER_POINTS: {
                    int id = Bytes.ReadIntLiteral();
                    int points = Players.GetPlayer(id).Points;
                    Bytes.push(LiteralFactory.CreateIntLiteral(points));
                    break;
                }

                case Instruction.READ_COUNTER: {
                    string key = Bytes.ReadStringLiteral();
                    int count = Variables.GetCounter(key);
                    Bytes.push(LiteralFactory.CreateIntLiteral(count));
                    break;
                }

                case Instruction.TARGET_PLAYER: {
                    //game.PresentChoiceOfPlayers(new List<GamePlayer>(game.GetPlayers()));
                    break;
                }

                case Instruction.TARGET_CARD: {
                    //game.PresentChoiceOfCards(new List<Card>(game.Table.GetCards()));
                    break;
                }

                #endregion

                #region EFFECTS

                case Instruction.INCREMENT_PLAYER_POINTS: {
                    GamePlayer player = ReadPlayerFromStack();
                    int pointsNum = Bytes.ReadIntLiteral();
                    Functions.SetPlayerPoints(player, player.Points + pointsNum);
                    break;
                }

                case Instruction.PLAYER_DRAW_CARD: {
                    GamePlayer player = ReadPlayerFromStack();
                    int numCards = Bytes.ReadIntLiteral();
                    for (int n = 0; n < numCards;) {
                        Functions.PlayerDrawCard(player, Cards.Deck);
                    }
                    break;
                }

                case Instruction.SET_COUNTER: {
                    string key = Bytes.ReadStringLiteral();
                    int count = Bytes.ReadIntLiteral();
                    Variables.SetCounter(key, count);
                    break;
                }

                case Instruction.SET_PLAYER_DRAW: {
                    GamePlayer player = ReadPlayerFromStack();
                    int num = Bytes.ReadIntLiteral();
                    player.SetDrawPerTurn(num);
                    break;
                }

                case Instruction.SET_PLAYER_MAX_HAND: {
                    GamePlayer player = ReadPlayerFromStack();
                    int num = Bytes.ReadIntLiteral();
                    player.Hand.MaxHandSize = num;
                    break;
                }
                
                case Instruction.SET_PLAYER_POINTS: {
                    GamePlayer player = ReadPlayerFromStack();
                    int pointsNum = Bytes.ReadIntLiteral();
                    Functions.SetPlayerPoints(player, pointsNum);
                    break;
                }

                case Instruction.MOVE_TO_DECK: {
                    Card card = ReadCardFromStack();
                    DeckLocation posEnum = (DeckLocation) Bytes.ReadIntLiteral();
                    card.Zone.MoveCard(Cards.Deck, card.id);
                    Cards.Deck.MoveLastAddedCard(posEnum);
                    break;
                }

                case Instruction.MOVE_TO_DISCARD: {
                    Card card = ReadCardFromStack();
                    card.Zone.MoveCard(Cards.Discard, card.id);
                    break;
                }

                #endregion
            }
        } catch (UnexpectedByteException e) {
            Debug.LogError(e);
        } catch (StackFullException e) {
            Debug.LogError(e);
        } catch (StackEmptyException e) {
            Debug.LogError(e);
        }
    }

    public void ExecuteNext () {
        try {
            ExecuteByte(Bytes.next());
        } catch (StackEmptyException e) {
            Debug.Log(e);
        }
    }

    #endregion
}

public enum ListType {
    PLAYER, CARD
}