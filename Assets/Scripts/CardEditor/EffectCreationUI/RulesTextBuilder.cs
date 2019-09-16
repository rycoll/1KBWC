using System;
using UnityEngine;

public class RulesTextBuilder {

    public static int CountWordsInString (string str) {
        char[] separators = new char[]{' '};
        return str.Split(separators).Length;
    }

    // public static string GetFieldText ()

    // use InstructionFactory as a reference guide
    // how to handle control effects??
    public static string GetInstructionText (EffectData effect, string[] args) {
        try {
        switch (effect.instruction) {
            case Instruction.ADD: {
                string numA = args[0], numB = args[1];
                return $"{numA} + {numB}";
            }
            case Instruction.GET_PLAYER: {
                string id = args[0];
                return $"Player {id}";
            }
            case Instruction.GET_PLAYER_POINTS: {
                string player = args[0];
                return $"the score of {player}";
            }
            case Instruction.INCREMENT_PLAYER_POINTS: {
                string num = args[0], player = args[1];
                string verb = (player.ToLower() == "you") ? "gain" : "gains";
                return (CountWordsInString(num) > 1)
                    ? $"{player} {verb} points equal to {num}."
                    : $"{player} {verb} {num} points.";
            }
            case Instruction.MOVE_TO_DECK: {
                string card = args[0], location = args[1].ToLower();
                if (location == "top" || location == "bottom") {
                    return $"Move {card} to the {location} of the deck.";
                } else if (location == "random") {
                    return $"Put {card} at a random position in the deck.";
                } else {
                    return $"Shuffle {card} into the deck.";
                }
            }
            case Instruction.MOVE_TO_DISCARD: {
                string card = args[0];
                return $"Put {card} into Discard.";
            }
            case Instruction.MULTIPLY: {
                string numA = args[0], numB = args[1];
                return $"{numA} * {numB}";
            }
            case Instruction.PLAYER_DRAW_CARD: {
                string player = args[0], num = args[1];
                string verb = (player.ToLower() == "you") ? "draw" : "draws";
                return (CountWordsInString(num) > 1)
                    ? $"{player} {verb} cards equal to {num}."
                    : $"{player} {verb} {num} cards.";
            }
            case Instruction.RANDOM_NUMBER: {
                string upper = args[0];
                return $"a random number between 1 and {upper}";
            }
            case Instruction.READ_COUNTER: {
                return args[0];
            }
            case Instruction.SET_COUNTER: {
                string key = args[0], num = args[1];
                return $"Set {key.ToUpper()} to {num}.";
            }
            case Instruction.SET_PLAYER_DRAW: {
                string num = args[0], player = args[1];
                string verb = (player.ToLower() == "you") ? "draw" : "draws";
                return (CountWordsInString(num) > 1)
                    ? $"{player} now {verb} cards equal to {num} per turn."
                    : $"{player} now {verb} {num} cards per turn.";
            }
            case Instruction.SET_PLAYER_MAX_HAND: {
                string num = args[0], player = args[1];
                return (CountWordsInString(num) > 1)
                    ? $"{player} can now hold cards equal to {num} in hand."
                    : $"{player} can now hold a maximum of {num} cards in hand.";
            }
            case Instruction.SET_PLAYER_POINTS: {
                string num = args[0], player = args[1];
                string verb = (player.ToLower() == "you") ? "have" : "has";
                return (CountWordsInString(num) > 1)
                    ? $"{player} {verb} their score set equal to {num}."
                    : $"{player} {verb} their score set to {num}";
            }
        }
        } catch (Exception e) {
            Debug.Log("Couldn't build string for effect: " + effect.instruction);
            foreach (string arg in args) Debug.Log($"==> {arg}");
            Debug.Log(e);
        }
        return "This is a card.";
    }

}