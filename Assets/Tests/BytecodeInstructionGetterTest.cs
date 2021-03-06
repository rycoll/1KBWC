﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class BytecodeGetterInstructionTest
    {

        ByteManager bytes;
        GameMaster game;

        [SetUp]
        public void SetUp () {
            game = new GameMaster();
            bytes = game.Bytes;
        }

        [Test]
        public void Add()   
        {
            bytes.push(InstructionFactory.Make_Add(
                LiteralFactory.CreateIntLiteral(4),
                LiteralFactory.CreateIntLiteral(13)
            ));
            game.ExecuteNext();
            Assert.AreEqual(bytes.ReadIntLiteral(game.queryCheck), 17);
        }

        [Test]
        public void Multiply() {
            bytes.push(InstructionFactory.Make_Multiply(
                LiteralFactory.CreateIntLiteral(4),
                LiteralFactory.CreateIntLiteral(13)
            ));
            game.ExecuteNext();
            Assert.AreEqual(bytes.ReadIntLiteral(game.queryCheck), 52);
        }

        [Test]
        public void GetListLength() {
            List<GamePlayer> list = new List<GamePlayer>{
                new GamePlayer("P1", 0),
                new GamePlayer("P2", 1),
                new GamePlayer("P3", 2),
                new GamePlayer("P4", 3),
            };
            bytes.push(InstructionFactory.Make_ListLength(
                LiteralFactory.CreateListLiteral(list)
            ));

            game.ExecuteNext();
            Assert.AreEqual(bytes.ReadIntLiteral(game.queryCheck), 4);
        }

        [Test]
        public void GetPlayerPoints() {
            game.Players = new PlayerManager(4);
            GamePlayer target = game.Players.GetPlayer(1);
            target.Points = 22;
            bytes.push(InstructionFactory.Make_GetPlayerPoints(
                LiteralFactory.CreatePlayerLiteral(target)
            ));
            game.ExecuteNext();
            Assert.AreEqual(bytes.ReadIntLiteral(game.queryCheck), 22);
        }

        [Test]
        public void ReadCounter () {
            game.Variables.SetCounter("my-key", 11);
            bytes.push(InstructionFactory.Make_ReadCounter(
                LiteralFactory.CreateStringLiteral("my-key")
            ));
            game.ExecuteNext();
            Assert.AreEqual(bytes.ReadIntLiteral(game.queryCheck), 11);
        }

        [Test]
        public void IntQuery() {
            List<byte> addInstruction = InstructionFactory.Make_Add(
                LiteralFactory.CreateIntLiteral(4),
                LiteralFactory.CreateIntLiteral(13)
            );
            bytes.push(InstructionFactory.Make_Add(
                addInstruction,
                LiteralFactory.CreateIntLiteral(100)
            ));
            game.ExecuteNext();
            Assert.AreEqual(bytes.ReadIntLiteral(game.queryCheck), 117);
        }

        [Test]
        public void CheckConditions() {
            bytes.push(InstructionFactory.Make_IsFalse(
                LiteralFactory.CreateBoolLiteral(true)
            ));
            Condition falseCondition = bytes.ReadConditionLiteral(game.queryCheck);
            Assert.IsFalse(falseCondition.Evaluate());

            bytes.push(InstructionFactory.Make_IsTrue(
                LiteralFactory.CreateBoolLiteral(true)
            ));
            Condition trueCondition = bytes.ReadConditionLiteral(game.queryCheck);
            Assert.IsTrue(trueCondition.Evaluate());
        }

        [Test]
        public void RandomGettersReturnAppropriately() {
            game.Players = new PlayerManager(4);
            game.Cards = new TestCardManager(5, 5, 5);
            
            // RANDOM_PLAYER
            bytes.push((byte) Instruction.RANDOM_PLAYER);
            game.ExecuteNext();
            Assert.NotNull(game.ReadPlayerFromStack());
            
            // RANDOM_OPPONENT
            bytes.push((byte) Instruction.RANDOM_OPPONENT);
            game.ExecuteNext();
            GamePlayer opponent = game.ReadPlayerFromStack();
            Assert.NotNull(opponent);
            Assert.AreNotEqual(game.Players.GetActivePlayer(), opponent);

            // RANDOM_CARD_IN_DECK
            bytes.push((byte) Instruction.RANDOM_CARD_IN_DECK);
            game.ExecuteNext();
            Assert.NotNull(game.ReadCardFromStack());

            // RANDOM_CARD_IN_DISCARD
            bytes.push((byte) Instruction.RANDOM_CARD_IN_DISCARD);
            game.ExecuteNext();
            Assert.NotNull(game.ReadCardFromStack());

            // RANDOM_CARD_IN_HAND
            Hand hand = new Hand(0);
            hand.AddCard(new TestCard());
            game.Players.GetPlayer(0).Hand = hand;
            bytes.push(InstructionFactory.Make_RandomCardInHand(
                LiteralFactory.CreatePlayerLiteral(0)
            ));
            game.ExecuteNext();
            Assert.NotNull(game.ReadCardFromStack());
        }
        
    }
}
