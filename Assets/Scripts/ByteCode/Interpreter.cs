using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter
{
    private static GameController game;

    private const int MAX_STACK_SIZE = 2058;
    private int currentStackSize = 0;
    byte[] stack = new byte[MAX_STACK_SIZE];

    public Interpreter (GameController gc) {
        game = gc;
    }

    public int GetCurrentStackSize () {
        return currentStackSize;
    }

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
    public bool CheckType (ListType check) {
        byte top = peek();
        if (top == (byte) check) {
            pop();
            return true;
        } else throw new UnexpectedByteException("Expected " + check + ", Found " + top);
    }
    public bool NextInstructionIsAccessor () {
        byte top = peek();
        return top < 0x60 && top >= 0x30;
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
        if (NextInstructionIsAccessor()) {
            next();
        }
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
        if (NextInstructionIsAccessor()) {
            next();
        }
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

    public static byte[] CreatePlayerLiteral(GamePlayer player) {
        return CreatePlayerLiteral(game.GetIndexOfPlayer(player));
    }
    public static byte[] CreatePlayerLiteral(int n) {
        byte[] intRepresentation = CreateIntLiteral(n);
        byte[] literal = new byte[intRepresentation.Length + 1];
        literal[0] = (byte) Instruction.PLAYER;
        intRepresentation.CopyTo(literal, 1);
        return literal;
    }
    public GamePlayer ReadPlayerLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
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
        if (NextInstructionIsAccessor()) {
            next();
        }
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
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.BOOL);
            return pop() != 0;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public static byte[] CreateConditionLiteral(byte[] operandA, byte[] operandB, ConditionOperator op) {
        // use this to create a reusable condition that can run queries
        byte[] conditionArr = new byte[2 + operandA.Length + operandB.Length];
        conditionArr[0] = (byte) ConditionType.NUM;
        conditionArr[1] = (byte) op;
        operandA.CopyTo(conditionArr, 2);
        operandB.CopyTo(conditionArr, 2 + operandA.Length);
        return conditionArr;
    }
    public static byte[] CreateConditionLiteral(CompareNum condition) {
        byte[] operandA = CreateIntLiteral(condition.OperandA);
        byte[] operandB = CreateIntLiteral(condition.OperandB);
        byte[] conditionArr = new byte[2 + operandA.Length + operandB.Length];
        conditionArr[0] = (byte) ConditionType.NUM;
        conditionArr[1] = (byte) condition.Operator;
        operandA.CopyTo(conditionArr, 2);
        operandB.CopyTo(conditionArr, 2 + operandA.Length);
        return conditionArr;
    }
    public Condition ReadConditionLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.CONDITION);
            byte conditionType = pop();
            switch ((ConditionType) conditionType) {
                case ConditionType.NUM:
                    int a = ReadIntLiteral();
                    int b = ReadIntLiteral();
                    ConditionOperator op = (ConditionOperator) pop();
                    return new Condition(a, b, op);
                default:
                    throw new UnexpectedByteException("Didn't understand the condition type! " + conditionType);
            }
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public List<byte[]> ReadList () {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.LIST);
            // discard list type
            pop();
            int num = ReadIntLiteral();
            List<byte[]> items = new List<byte[]>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                int size = ReadIntLiteral();
                byte[] array = new byte[size];
                for (int j = 0; j < size; j++) {
                    array[j] = pop();
                }
                items.Add(array);
            }
            return items;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public List<GamePlayer> ReadPlayerList () {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.LIST);
            CheckType(ListType.PLAYER);
            int num = ReadIntLiteral();
            List<GamePlayer> players = new List<GamePlayer>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                // discard size
                ReadIntLiteral();
                players.Add(ReadPlayerLiteral());
            }
            return players;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }
    public List<Card> ReadCardList () {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.LIST);
            CheckType(ListType.CARD);
            int num = ReadIntLiteral();
            List<Card> cards = new List<Card>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                // discard size
                ReadIntLiteral();
                cards.Add(ReadCardLiteral());
            }
            return cards;
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
        listLiteral[0] = (byte) Instruction.LIST;
        listLiteral[1] = (byte) type;
        objects.CopyTo(listLiteral, 2);
        return listLiteral;
    }
    public static byte[] CreateListLiteral(List<GamePlayer> players) {
        List<byte> bytes = new List<byte>();
        foreach (GamePlayer player in players) {
            bytes.AddRange(
                new List<byte>(CreatePlayerLiteral(player))
            );
        }
        return CreateListLiteral(bytes.ToArray(), ListType.PLAYER, players.Count);
    }
    public static byte[] CreateListLiteral(List<Card> cards) {
        List<byte> bytes = new List<byte>();
        foreach (Card card in cards) {
            bytes.AddRange(
                new List<byte>(CreateCardLiteral(card.GetID()))
            );
        }
        return CreateListLiteral(bytes.ToArray(), ListType.CARD, cards.Count);
    }

    public void next () {
        if (currentStackSize <= 0) {
            throw new StackEmptyException($"Stack is empty: ({currentStackSize}, can't do next!");
        }
        Instruction next = (Instruction) pop();

        try {
            switch(next) {

                // FUNCTIONS

                case Instruction.RANDOM_NUMBER: {
                    int upperBound = ReadIntLiteral();
                    push(CreateIntLiteral(UnityEngine.Random.Range(1, upperBound)));
                    break;
                }

                case Instruction.ADD: {
                    int a = ReadIntLiteral();
                    int b = ReadIntLiteral();
                    push(CreateIntLiteral(a + b));
                    break;
                }

                case Instruction.IF: {
                    Condition condition = ReadConditionLiteral();
                    if (!condition.Evaluate()) {
                        while (currentStackSize > 0 && peek() != (byte) Instruction.ENDIF) {
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
                    int ID = ReadIntLiteral();
                    List<byte[]> items = ReadList();
                    
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
                                int chunkSize = ReadIntLiteral();
                                for (int n = 0; n < chunkSize; n++) {
                                    // add to start, to retain stack ordering
                                    compiled.Insert(0, pop());
                                }
                            } else if (currentByte == (byte) Instruction.PLACEHOLDER) {
                                int placeholderID = ReadIntLiteral();
                                  if (placeholderID == ID) {
                                    compiled.AddRange(new List<byte>(items[i]));
                                } else {
                                    compiled.Add((byte) Instruction.PLACEHOLDER);
                                    compiled.AddRange(new List<byte>(CreateIntLiteral(placeholderID)));
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
                    push(CreatePlayerLiteral(game.GetActivePlayer()));
                    break;
                }

                case Instruction.GET_PLAYER : {
                    int id = ReadIntLiteral();
                    push(CreatePlayerLiteral(id));
                    break;
                }

                case Instruction.GET_PLAYER_POINTS: {
                    int id = ReadIntLiteral();
                    int points = game.GetPlayer(id).Points;
                    push(CreateIntLiteral(points));
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
                    card.Zone.MoveCard(game.Deck, card.GetID());
                    game.Deck.MoveLastAddedCard(posEnum);
                    break;
                }

                case Instruction.MOVE_TO_DISCARD: {
                    Card card = ReadCardLiteral();
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
            byte[] player = Interpreter.CreatePlayerLiteral(chosenPlayer);
            game.AddToStack(player);
        }
        next();
    }  

    public void CardChoiceCallback (Card chosenCard) {
        if (chosenCard != null) {
            byte[] card = Interpreter.CreateCardLiteral(chosenCard.GetID());
            game.AddToStack(card);
        }
        next();
    }  
}
