using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{

    [TestFixture]
    public class RulesTextGenerationTest
    {

        public string getText(List<byte> arr) {
            RulesTextInterpreter textGen = new RulesTextInterpreter(arr);
            return textGen.GetFullRulesText();
        }

        public void PrintBytes(List<byte> arr) {
            PrintStack printer = new PrintStack(arr);
            Debug.Log(printer.PrintStackInstructions());
        }

        [Test]
        public void YouGainRandomPoints () {
            List<byte> bytes = InstructionFactory.Make_IncrementPlayerPoints(
                new List<byte>{ (byte) Instruction.GET_ACTIVE_PLAYER },
                InstructionFactory.Make_RandomNumber(LiteralFactory.CreateIntLiteral(10))
            );
            string text = getText(bytes);
            Assert.AreEqual("You gain points equal to a random number between 1 and 10.", text);
        }

        [Test]
        public void YouSet10Points () {
            List<byte> bytes = InstructionFactory.Make_SetPlayerPoints(
                new List<byte>{ (byte) Instruction.GET_ACTIVE_PLAYER },
                LiteralFactory.CreateIntLiteral(10)
            );
            string text = getText(bytes);
            Assert.AreEqual("Set your score to 10.", text);
        }

        [Test]
        public void SetStenchTo10 () {
            List<byte> bytes = InstructionFactory.Make_SetCounter(
                LiteralFactory.CreateStringLiteral("stench"),
                LiteralFactory.CreateIntLiteral(10)
            );
            string text = getText(bytes);
            Assert.AreEqual("Set STENCH to 10.", text);
        }

        [Test]
        public void TargetPlayerDraws () {
            List<byte> bytes = InstructionFactory.Make_PlayerDrawCards(
                new List<byte>{ (byte) Instruction.TARGET_PLAYER },
                LiteralFactory.CreateIntLiteral(1)
            );
            string text = getText(bytes);
            Assert.AreEqual("A player of your choice draws 1 card.", text);
        }

        [Test]
        public void MillTargetCard () {
            List<byte> bytes = InstructionFactory.Make_MoveToDiscard(
                new List<byte>{ (byte) Instruction.TARGET_CARD_IN_DECK }
            );
            string text = getText(bytes);
            Assert.AreEqual("Put a card of your choice from the deck into Discard.", text);
        }

        [Test]
        public void IfStenchOver50ResetStench () {
            List<byte> bytes = InstructionFactory.Make_If (
                InstructionFactory.Make_SetCounter(
                    LiteralFactory.CreateStringLiteral("stench"),
                    LiteralFactory.CreateIntLiteral(0)
                ),
                InstructionFactory.Make_NumComparison(
                    InstructionFactory.Make_ReadCounter(
                        LiteralFactory.CreateStringLiteral("stench")
                    ),
                    LiteralFactory.CreateIntLiteral(50),
                    (byte) ConditionOperator.MORE_THAN
                )
            );
            string text = getText(bytes);
            Assert.AreEqual("If STENCH is more than 50, then set STENCH to 0.", text);
        }

        [Test]
        public void LoopPlayersDiscardCards () {
            List<byte> bytes = InstructionFactory.Make_Loop (
                InstructionFactory.Make_ListLength(
                    new List<byte>{(byte) Instruction.GET_ALL_PLAYERS}
                ),
                InstructionFactory.Make_MoveToDiscard(
                    new List<byte>{(byte) Instruction.TARGET_CARD_IN_DECK} 
                )
            );
            string text = getText(bytes);
            Assert.AreEqual("Do this a number of times equal to the number of players: put a card of your choice from the deck into Discard.", text);
        }

        [Test]
        public void EachPlayerDrawsACard () {
            List<byte> items = new List<byte>{ (byte) Instruction.GET_ALL_PLAYERS };
            List<byte> code = InstructionFactory.Make_SetPlayerPoints(
                LiteralFactory.CreatePlaceholderLiteral(0),
                LiteralFactory.CreateIntLiteral(50)
            );
            List<byte> bytes = InstructionFactory.Make_ForLoop(items, code, 0);

            string text = getText(bytes);
            Assert.AreEqual("For each of the players: that player has their score set to 50", text);
        }

        
    }
}
