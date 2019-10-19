using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{

    [TestFixture]
    public class RulesTextGenerationTest
    {

        public string getText(byte[] arr) {
            RulesTextInterpreter textGen = new RulesTextInterpreter(arr);
            return textGen.GetFullRulesText();
        }

        public void PrintBytes(byte[] arr) {
            PrintStack printer = new PrintStack(arr, arr.Length);
            Debug.Log(printer.PrintStackInstructions());
        }

        [Test]
        public void YouGainRandomPoints () {
            byte[] bytes = InstructionFactory.Make_IncrementPlayerPoints(
                InstructionFactory.Make_RandomNumber(LiteralFactory.CreateIntLiteral(10)),
                new byte[]{ (byte) Instruction.GET_ACTIVE_PLAYER }
            );
            string text = getText(bytes);
            Assert.AreEqual("You gain points equal to a random number between 1 and 10.", text);
        }

        [Test]
        public void YouSet10Points () {
            byte[] bytes = InstructionFactory.Make_SetPlayerPoints(
                LiteralFactory.CreateIntLiteral(10),
                new byte[]{ (byte) Instruction.GET_ACTIVE_PLAYER }
            );
            string text = getText(bytes);
            Assert.AreEqual("Set your score to 10.", text);
        }

        [Test]
        public void SetStenchTo10 () {
            byte[] bytes = InstructionFactory.Make_SetCounter(
                LiteralFactory.CreateStringLiteral("stench"),
                LiteralFactory.CreateIntLiteral(10)
            );
            string text = getText(bytes);
            Assert.AreEqual("Set STENCH to 10.", text);
        }

        [Test]
        public void TargetPlayerDraws () {
            byte[] bytes = InstructionFactory.Make_PlayerDrawCards(
                new byte[]{ (byte) Instruction.TARGET_PLAYER },
                LiteralFactory.CreateIntLiteral(1)
            );
            string text = getText(bytes);
            Assert.AreEqual("A player of your choice draws 1 card.", text);
        }

        [Test]
        public void DiscardTargetCard () {
            byte[] bytes = InstructionFactory.Make_MoveToDiscard(
                new byte[]{ (byte) Instruction.TARGET_CARD }
            );
            string text = getText(bytes);
            Assert.AreEqual("Put a card of your choice into Discard.", text);
        }

        [Test]
        public void IfStenchOver50ResetStench () {
            byte[] bytes = InstructionFactory.Make_If (
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
            byte[] bytes = InstructionFactory.Make_Loop (
                InstructionFactory.Make_ListLength(
                    new byte[]{(byte) Instruction.GET_ALL_PLAYERS}
                ),
                InstructionFactory.Make_MoveToDiscard(
                    new byte[]{(byte) Instruction.TARGET_CARD} 
                )
            );
            string text = getText(bytes);
            Assert.AreEqual("Do this a number of times equal to the number of players: put a card of your choice into Discard.", text);
        }

        [Test]
        public void EachPlayerDrawsACard () {
            byte[] items = new byte[]{ (byte) Instruction.GET_ALL_PLAYERS };

            List<byte[]> chunkList = new List<byte[]>{
                new byte[]{(byte) Instruction.PLAYER_DRAW_CARD},
                LiteralFactory.CreateIntLiteral(1),
            };

            byte[] bytes = InstructionFactory.Make_ForLoop(items, chunkList);

            string text = getText(bytes);
            Assert.AreEqual("For each of the players: that player draws 1 card.", text);
        }

        
    }
}
