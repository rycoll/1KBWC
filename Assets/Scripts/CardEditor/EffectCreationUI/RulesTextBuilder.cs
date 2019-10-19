using System;
using UnityEngine;

public class RulesTextBuilder {

    public static int CountWordsInString (string str) {
        char[] separators = new char[]{' '};
        return str.Split(separators).Length;
    }

    public static string GetInstructionText (EffectData effect, string[] args) {
        try {
            switch (effect.instruction) {
                case Instruction.ADD: {
                    string numA = args[0], numB = args[1];
                    return $"({numA} + {numB})";
                }
                case Instruction.FOR_LOOP: {
                    string list = args[0];
                    string rootEffect = args[1];
                    return $"For each of the {list}: {rootEffect}";
                }
                case Instruction.IF: {
                    string condition = args[0];
                    string rootEffect = args[1];
                    return $"if {condition}, then {rootEffect}";
                }
                case Instruction.GET_ACTIVE_PLAYER: return "you";
                case Instruction.GET_ALL_PLAYERS: return "players";
                case Instruction.GET_ALL_OPPONENTS: return "opponents";
                case Instruction.GET_CARDS_IN_DECK: return "cards in deck";
                case Instruction.GET_CARDS_IN_DISCARD: return "cards in discard";
                case Instruction.GET_CARDS_IN_HAND: {
                    string player = args[0];
                    return (player.ToLower() == "you") 
                        ? $"cards in your hand"
                        : $"cards held by {player}";
                }
                case Instruction.GET_PLAYER: {
                    string id = args[0];
                    return $"player {id}";
                }
                case Instruction.GET_PLAYER_POINTS: {
                    string player = args[0];
                    return (player.ToLower() == "you")
                        ? "your score" 
                        : $"the score of {player}";
                }
                case Instruction.INCREMENT_PLAYER_POINTS: {
                    string player = args[0], num = args[1];
                    string verb = (player.ToLower() == "you") ? "gain" : "gains";
                    return (CountWordsInString(num) > 1)
                        ? $"{player} {verb} points equal to {num}."
                        : $"{player} {verb} {num} points.";
                }
                case Instruction.LIST_LENGTH: {
                    string list = args[0];
                    return $"the number of {list}";
                }
                case Instruction.CARD_HAS_TAG: {
                    string card = args[0], tag = args[1];
                    return $"{card} is {tag}";
                }
                case Instruction.LOOP: {
                    string num = args[0];
                    string rootEffect = args[1];
                    return (CountWordsInString(num) > 1)
                        ? $"do this a number of times equal to {num}: {rootEffect}"
                        : $"{num} times: {rootEffect}";
                }
                case Instruction.BOOL_COMPARISON: {
                    string firstBool = args[0];
                    string secondBool = args[1];
                    string operation = args[2].ToLower();
                    return (operation == "equals") 
                        ? $"{firstBool} is {secondBool}"
                        : $"{firstBool} isn't {secondBool}";
                }
                case Instruction.NUM_COMPARISON: {
                    string firstNum = args[0];
                    string secondNum = args[1];
                    string operation = args[2].ToLower();
                    switch(operation) {
                        case "equal":
                            return $"{firstNum} is equal to {secondNum}";
                        case "not_equal":
                            return $"{firstNum} is not equal to {secondNum}";
                        case "more_than":
                            return $"{firstNum} is more than {secondNum}";
                        case "at_least":
                            return $"{firstNum} is at least {secondNum}";
                        case "less_than":
                            return $"{firstNum} is less than {secondNum}";
                        case "at_most":
                            return $"{firstNum} is at most {secondNum}";
                    }
                    return $"{firstNum} {operation} {secondNum}";
                }
                case Instruction.MOVE_TO_DECK: {
                    string card = args[0], location = args[1].ToLower();
                    if (location == "top" || location == "bottom") {
                        return $"move {card} to the {location} of the deck.";
                    } else if (location == "random") {
                        return $"put {card} at a random position in the deck.";
                    } else {
                        return $"shuffle {card} into the deck.";
                    }
                }
                case Instruction.MOVE_TO_DISCARD: {
                    string card = args[0];
                    return $"put {card} into Discard.";
                }
                case Instruction.MULTIPLY: {
                    string numA = args[0], numB = args[1];
                    return $"({numA} * {numB})";
                }
                case Instruction.PLAYER_DRAW_CARD: {
                    string player = args[0], num = args[1];
                    string verb = (player.ToLower() == "you") ? "draw" : "draws";
                    return (CountWordsInString(num) > 1)
                        ? $"{player} {verb} cards equal to {num}."
                        : ((num == "1")
                            ? $"{player} {verb} {num} card."
                            : $"{player} {verb} {num} cards."
                        );
                }
                case Instruction.RANDOM_NUMBER: {
                    string upper = args[0];
                    return $"a random number between 1 and {upper}";
                }
                case Instruction.READ_COUNTER: {
                    return args[0].ToUpper();
                }
                case Instruction.SET_COUNTER: {
                    string key = args[0], num = args[1];
                    return $"set {key.ToUpper()} to {num}.";
                }
                case Instruction.SET_PLAYER_DRAW: {
                    string player = args[0], num = args[1];
                    string verb = (player.ToLower() == "you") ? "draw" : "draws";
                    return (CountWordsInString(num) > 1)
                        ? $"{player} now {verb} cards equal to {num} per turn."
                        : $"{player} now {verb} {num} cards per turn.";
                }
                case Instruction.SET_PLAYER_MAX_HAND: {
                    string player = args[0], num = args[1];
                    return (CountWordsInString(num) > 1)
                        ? $"{player} can now hold cards equal to {num} in hand."
                        : $"{player} can now hold a maximum of {num} cards in hand.";
                }
                case Instruction.SET_PLAYER_POINTS: {
                    string player = args[0], num = args[1];
                    if (player.ToLower() == "you") {
                        return (CountWordsInString(num) > 1)
                            ? $"set your score equal to {num}."
                            : $"set your score to {num}.";
                    }
                    return (CountWordsInString(num) > 1)
                        ? $"{player} has their score set equal to {num}."
                        : $"{player} has their score set to {num}";
                }
                case Instruction.TARGET_CARD: return "a card of your choice";
                case Instruction.TARGET_PLAYER: return "a player of your choice";


                // literals with EnterValues

                case Instruction.EFFECT_DELIMITER: {
                    return "\n";
                }
                case Instruction.INT: return args[0];
                case Instruction.STRING: return args[0];
                case Instruction.BOOL: return args[0];
                case Instruction.COLOUR: return args[0];

                default:
                    Debug.Log("No entry found for rules text builder: " + effect.instruction);
                    break;
            }
        } catch (Exception e) {
            Debug.Log("Couldn't build string for effect: " + effect.instruction);
            foreach (string arg in args) Debug.Log($"==> {arg}");
            Debug.Log(e);
        }
        return "";
    }

}