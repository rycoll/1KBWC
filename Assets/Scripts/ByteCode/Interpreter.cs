using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter
{

    private GameController game;

    private const int MAX_STACK_SIZE = 2058;
    private int currentStackSize = 0;
    byte[] stack = new byte[MAX_STACK_SIZE];

    public void push(byte b) {
        if (currentStackSize >= MAX_STACK_SIZE) {
            throw new StackFullException("Stack is too full! Can't push " + b.ToString());
        }
        stack[currentStackSize++] = b;
    }

    public void push(byte[] arr) {
        if (currentStackSize + arr.Length >= MAX_STACK_SIZE) {
            throw new StackFullException("Stack is too full! Can't push " + arr.Length + " bytes!");
        }
        foreach (byte b in arr) {
            push(b);
        }
    }

    public byte pop() {
        // needs better error handling
        if (currentStackSize <= 0) {
            throw new StackEmptyException("Stack is empty! " + currentStackSize);
        }
        return stack[--currentStackSize];
    }

    public byte[] pop(int n) {
        byte[] arr = new byte[n];
        for (int i = 0; i < n; i++) {
            arr[i] = pop();
        }
        return arr;
    }

    public byte peek() {
        if (currentStackSize <= 0) {
            throw new StackEmptyException("Stack is empty! " + currentStackSize);
        }
        return stack[currentStackSize - 1];
    }

    public bool CheckType (Instruction check) {
        byte top = peek();
        if (top == (byte) check) {
            pop();
            return true;
        } else throw new UnexpectedByteException("Expected " + check + ", Found " + top);
    }

    public static byte[] CreateIntLiteral(int n) {
        // getbytes uses same endian format as the system
        byte[] intRepresentation = BitConverter.GetBytes(n);
        byte[] literal = new byte[intRepresentation.Length + 1];
        literal[0] = (byte) Instruction.INT;
        intRepresentation.CopyTo(literal, 1);
        return literal;
    }
    public int ReadIntLiteral() {
        try {
            CheckType(Instruction.INT);
            byte[] intRepresentation = pop(4);
            return BitConverter.ToInt32(intRepresentation, 0);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public static byte[] CreateStringLiteral(string str) {
        byte[] arr = System.Text.Encoding.UTF8.GetBytes(str);
        byte[] strBytes = new byte[255];
        if (arr.Length <= 255) {
            strBytes = arr;
        } else {   
            Array.Copy(arr, strBytes, 255);
        }
        byte[] literal = new byte[strBytes.Length + 2];
        literal[0] = (byte) Instruction.STRING;
        literal[1] = (byte) strBytes.Length;
        strBytes.CopyTo(literal, 2);
        return literal;
    }
    public string ReadStringLiteral() {
        try {
            CheckType(Instruction.STRING);
            byte arrSize = pop();
            byte[] strArray = pop(arrSize);
            char[] chars = System.Text.Encoding.UTF8.GetChars(strArray);
            return new string(chars);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public static byte[] CreatePlayerLiteral(int n) {
        byte[] intRepresentation = CreateIntLiteral(n);
        byte[] literal = new byte[intRepresentation.Length + 1];
        literal[0] = (byte) Instruction.PLAYER;
        intRepresentation.CopyTo(literal, 1);
        return literal;
    }
    public GamePlayer ReadPlayerLiteral() {
        try {
            CheckType(Instruction.PLAYER);
            int index = ReadIntLiteral();
            return game.GetPlayer(index);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public static byte[] CreateCardLiteral(int n) {
        byte[] intRepresentation = CreateIntLiteral(n);
        byte[] literal = new byte[intRepresentation.Length + 1];
        literal[0] = (byte) Instruction.PLAYER;
        intRepresentation.CopyTo(literal, 1);
        return literal;
    }
    public Card ReadCardLiteral() {
        try {
            CheckType(Instruction.CARD);
            int id = ReadIntLiteral();
            return game.FindCardById(id);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public static byte[] CreateBoolLiteral(bool b) {
        byte[] boolArr = new byte[2];
        boolArr[0] = (byte) Instruction.BOOL;
        boolArr[1] = b ? (byte) 1 : (byte) 0;
        return boolArr;
    }
    public bool ReadBoolLiteral() {
        try {
            CheckType(Instruction.BOOL);
            return pop() != 0;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public static byte[] CreateConditionLiteral(CompareNum condition) {
        byte[] operandA = CreateIntLiteral(condition.OperandA);
        byte[] operandB = CreateIntLiteral(condition.OperandB);
        byte[] condition = new byte[2 + operandA.Length + operandB.Length];
        condition[0] = (byte) ConditionType.NUM;
        condition[1] = (byte) condition.Operator;
        operandA.CopyTo(condition, 2);
        operandB.CopyTo(condition, 2 + operandA.Length);
        return condition;
    }
    public Condition ReadConditionLiteral() {
        try {
            CheckType(Instruction.CONDITION);
            switch ((ConditionType) pop()) {
                case ConditionType.NUM:
                    int a = ReadIntLiteral();
                    int b = ReadIntLiteral();
                    ConditionOperator op = (ConditionOperator) pop();
                    return new Condition(a, b, op);
                    break;
            }
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public enum ListType {
        PLAYER, CARD
    }
    public static byte[] CreateListLiteral(byte[] objects, ListType type, int length) {
        byte[] listSize = CreateIntLiteral(length);
        byte[] listLiteral = new byte[2 + listSize.Length + objects.Length];
        listLiteral[0] = Instruction.LIST;
        listLiteral[1] = (byte) type;
        objects.CopyTo(listLiteral, 2);
        return listLiteral;
    }
    public static byte[] CreateListLiteral(List<GamePlayer> players) {
        List<byte> bytes = new List<byte>();
        foreach (GamePlayer player in players) {
            bytes.AddRange(
                new List<byte>(CreatePlayerLiteral(player.id))
            );
        }
        return CreateListLiteral(bytes, ListType.PLAYER, players.Count);
    }
    public static byte[] CreateListLiteral(List<Card> cards) {
        List<byte> bytes = new List<byte>();
        foreach (Card card in cards) {
            bytes.AddRange(
                new List<byte>(CreateCardLiteral(card.id))
            );
        }
        return CreateListLiteral(bytes, ListType.CARD, cards.Count);
    }

    public List<byte[]> UnpackListLiteral() {

    }
    public List<Player> ReadPlayerList() {

    }

    public void next () {
        if (currentStackSize <= 0) {
            throw new StackEmptyException($"Stack is empty: ({currentStackSize}, can't do next!");
        }
        Instruction next = (Instruction) pop();

        try {
            switch(next) {

                // FUNCTIONS

                case Instruction.ADD: {
                    int a = ReadIntLiteral();
                    int b = ReadIntLiteral();
                    push(CreateIntLiteral(a + b));
                    break;
                }

                case INSTRUCTION.IF: {
                    Condition condition = ReadConditionLiteral();
                    if (!condition.Evaluate()) {
                        while (currentStackSize > 0 && peek() != Instruction.ENDIF) {
                            pop();
                        }
                    }
                    break;
                }

                case Instruction.MULTIPLY: {
                    int a = ReadIntLiteral();
                    int b = ReadIntLiteral();
                    push(CreateIntLiteral(a * b));
                    break;
                }

                case Instruction.LOOP: {
                    int num = ReadIntLiteral();
                    List<byte> bytes = new List<byte>();
                    while (currentStackSize > 0) {
                        byte b = pop();
                        if (b == (byte) Instruction.ENDLOOP) {
                            break;
                        }
                        bytes.Add(b);
                    }
                    byte[] byteArr = bytes.ToArray();
                    for (int n = 0; n < num; n++) {
                        stack.push(byteArr);
                    }
                    break;
                }
                
                // QUERIES

                case Instruction.GET_ACTIVE_PLAYER: {
                    int playerIndex = game.GetIndexOfPlayer(game.GetActivePlayer());
                    push(CreatePlayerLiteral(playerIndex));
                    break;
                }

                case Instruction.READ_COUNTER: {
                    string key = ReadStringLiteral();
                    int count = game.Variables.GetCounter(key);
                    push(CreateIntLiteral(count));
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
                    GamePlayer player = ReadPlayerLiteral();
                    int pointsNum = ReadIntLiteral();
                    game.SetPlayerPoints(player, player.Points + pointsNum);
                    break;
                }

                case Instruction.PLAYER_DRAW_CARD: {
                    GamePlayer player = ReadPlayerLiteral();
                    int numCards = ReadIntLiteral();
                    for (int n = 0; n < numCards;) {
                        game.PlayerDrawCard(player);
                    }
                    break;
                }

                case Instruction.SET_COUNTER: {
                    string key = ReadStringLiteral();
                    int count = ReadIntLiteral();
                    game.SetCounter(key, count);
                    break;
                }

                case Instruction.SET_PLAYER_DRAW: {
                    GamePlayer player = ReadPlayerLiteral();
                    int num = ReadIntLiteral();
                    player.SetDrawPerTurn(num);
                    break;
                }

                case Instruction.SET_PLAYER_MAX_HAND: {
                    GamePlayer player = ReadPlayerLiteral();
                    int num = ReadIntLiteral();
                    player.Hand.MaxHandSize = num;
                    break;
                }
                
                case Instruction.SET_PLAYER_POINTS: {
                    GamePlayer player = ReadPlayerLiteral();
                    int pointsNum = ReadIntLiteral();
                    game.SetPlayerPoints(player, pointsNum);
                    break;
                }

                case Instruction.MOVE_TO_DECK: {
                    Card card = ReadCardLiteral();
                    DeckLocation posEnum = (DeckLocation) ReadIntLiteral();
                    card.Zone.MoveCard(game.Deck, card.id);
                    game.Deck.MoveLastAddedCard(posEnum);
                    break;
                }

                case Instruction.MOVE_TO_DISCARD: {
                    Card card = ReadCardLiteral();
                    card.Zone.MoveCard(game.Discard, card.id);
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
            byte[] player = Interpreter.CreatePlayerLiteral(game.GetIndexOfPlayer(chosenPlayer));
            game.AddToStack(player);
        }
        next();
    }  

    public void CardChoiceCallback (Card chosenCard) {
        if (chosenCard != null) {
            byte[] card = Interpreter.CreateCardLiteral(chosenCard.id);
            game.AddToStack(card);
        }
        next();
    }  
}
