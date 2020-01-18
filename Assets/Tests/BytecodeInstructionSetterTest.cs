using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class BytecodeInstructionSetterTest
    {
        ByteManager bytes;
        GameMaster game;

        [SetUp]
        public void SetUp () {
            game = new GameMaster();
            bytes = game.Bytes;
        }

        [Test]
        public void SetCounter () {
            bytes.push(InstructionFactory.Make_SetCounter(
                LiteralFactory.CreateStringLiteral("my-key"),
                LiteralFactory.CreateIntLiteral(44)
            ));
            game.ExecuteNext();
            Assert.AreEqual(game.Variables.GetCounter("my-key"), 44);
        }

        [Test]
        public void SetPlayerDraw () {
            game.Players = new PlayerManager(1);
            GamePlayer target = game.Players.GetPlayer(0);
            target.SetDrawPerTurn(0);
            bytes.push(InstructionFactory.Make_SetPlayerDraw(
                LiteralFactory.CreateIntLiteral(5),
                LiteralFactory.CreatePlayerLiteral(target)
            ));
            game.ExecuteNext();
            Assert.AreEqual(target.DrawPerTurn, 5);
        }

        [Test]
        public void SetPlayerMaxHand () {
            game.Players = new PlayerManager(1);
            GamePlayer target = game.Players.GetPlayer(0);
            target.Hand.SetMax(0);
            bytes.push(InstructionFactory.Make_SetPlayerMaxHand(
                LiteralFactory.CreateIntLiteral(5),
                LiteralFactory.CreatePlayerLiteral(target)
            ));
            game.ExecuteNext();
            Assert.AreEqual(target.Hand.MaxHandSize, 5);
        }

        [Test]
        public void SetPlayerPoints () {
            game.Players = new PlayerManager(1);
            GamePlayer target = game.Players.GetPlayer(0);
            target.Points = 0;
            bytes.push(InstructionFactory.Make_SetPlayerPoints(
                LiteralFactory.CreateIntLiteral(100),
                LiteralFactory.CreatePlayerLiteral(target)
            ));
            game.ExecuteNext();
            Assert.AreEqual(target.Points, 100);
        }

        [Test]
        public void IncrementPlayerPoints () {
            game.Players = new PlayerManager(1);
            GamePlayer target = game.Players.GetPlayer(0);
            target.Points = 5;
            bytes.push(InstructionFactory.Make_IncrementPlayerPoints(
                LiteralFactory.CreateIntLiteral(20),
                LiteralFactory.CreatePlayerLiteral(target)
            ));
            game.ExecuteNext();
            Assert.AreEqual(target.Points, 25);
        }

        private class TestCard : Card {
            public TestCard () {
                SetID();
                Zone = new CardZone();
            }
        }

        [Test]
        public void PlayerDrawCards() {
            game.Cards = new CardManager();
            Card c1 = new TestCard();
            Card c2 = new TestCard();
            Card c3 = new TestCard();
            Card c4 = new TestCard();
            game.Cards.Deck.AddCard(c1);
            game.Cards.Deck.AddCard(c2);
            game.Cards.Deck.AddCard(c3);
            game.Cards.Deck.AddCard(c4);

            game.Players = new PlayerManager(1);
            GamePlayer target = game.Players.GetPlayer(0);

            Assert.AreEqual(target.Hand.GetSize(), 0);
            bytes.push(InstructionFactory.Make_PlayerDrawCards(
                LiteralFactory.CreatePlayerLiteral(target),
                LiteralFactory.CreateIntLiteral(2)
            ));
            game.ExecuteNext();
            Assert.AreEqual(target.Hand.GetSize(), 2);
        }

        [Test]
        public void MoveToDeck() {
            game.Cards = new CardManager();

            Card c1 = new TestCard();
            Card c2 = new TestCard();
            Card c3 = new TestCard();
            Card c4 = new TestCard();

            game.Cards.Discard.AddCard(c1);
            game.Cards.Discard.AddCard(c2);
            game.Cards.Discard.AddCard(c3);
            game.Cards.Discard.AddCard(c4);

            bytes.push(InstructionFactory.Make_MoveToDeck(
                LiteralFactory.CreateCardLiteral(c1.GetID()),
                new List<byte>{(byte) DeckLocation.TOP}
            ));
            game.ExecuteNext();
            Assert.AreSame(game.Cards.Deck.GetCard(DeckLocation.TOP), c1);

            bytes.push(InstructionFactory.Make_MoveToDeck(
                LiteralFactory.CreateCardLiteral(c2.GetID()),
                new List<byte>{(byte) DeckLocation.BOTTOM}
            ));
            game.ExecuteNext();
            Assert.AreSame(game.Cards.Deck.GetCard(DeckLocation.BOTTOM), c2);

            bytes.push(InstructionFactory.Make_MoveToDeck(
                LiteralFactory.CreateCardLiteral(c3.GetID()),
                new List<byte>{(byte) DeckLocation.TOP}
            ));
            game.ExecuteNext();
            Assert.AreSame(game.Cards.Deck.GetCard(DeckLocation.TOP), c3);

            bytes.push(InstructionFactory.Make_MoveToDeck(
                LiteralFactory.CreateCardLiteral(c4.GetID()),
                new List<byte>{(byte) DeckLocation.SHUFFLE}
            ));
            game.ExecuteNext();
            Assert.IsNotNull(game.Cards.Deck.GetCard(c4.GetID()));
        }

        [Test]
        public void MoveToDiscard() {
            game.Cards = new CardManager();

            Card c1 = new TestCard();
            Card c2 = new TestCard();
            Card c3 = new TestCard();
            Card c4 = new TestCard();

            game.Cards.Deck.AddCard(c1);
            game.Cards.Deck.AddCard(c2);
            game.Cards.Deck.AddCard(c3);
            game.Cards.Deck.AddCard(c4);

            bytes.push(InstructionFactory.Make_MoveToDiscard(
                LiteralFactory.CreateCardLiteral(c1.GetID())
            ));
            game.ExecuteNext();
            Assert.IsNotNull(game.Cards.Discard.GetCard(c1.GetID()));

            bytes.push(InstructionFactory.Make_MoveToDiscard(
                LiteralFactory.CreateCardLiteral(c2.GetID())
            ));
            game.ExecuteNext();
            Assert.IsNotNull(game.Cards.Discard.GetCard(c2.GetID()));

            bytes.push(InstructionFactory.Make_MoveToDiscard(
                LiteralFactory.CreateCardLiteral(c3.GetID())
            ));
            game.ExecuteNext();
            Assert.IsNotNull(game.Cards.Discard.GetCard(c3.GetID()));

            bytes.push(InstructionFactory.Make_MoveToDiscard(
                LiteralFactory.CreateCardLiteral(c4.GetID())
            ));
            game.ExecuteNext();
            Assert.IsNotNull(game.Cards.Discard.GetCard(c4.GetID()));
        }
    }
}
