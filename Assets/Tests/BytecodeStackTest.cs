using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class BytecodeStackTest
    {

        ByteManager bytes;
        GameMaster game;
        ReadCallback dummyCallback;

        public void doNothing() {}

        [SetUp]
        public void SetUp () {
            game = new GameMaster();
            bytes = game.Bytes;
            dummyCallback = doNothing;
        }

        [Test]
        public void StackIndividualBytes() {
            byte b1 = 0x00;
            byte b2 = 0x01;
            byte b3 = 0x02;

            bytes.push(b1);
            bytes.push(b2);
            bytes.push(b3);

            Assert.AreEqual(bytes.pop(), b3);
            Assert.AreEqual(bytes.pop(), b2);
            Assert.AreEqual(bytes.pop(), b1);
        }

        [Test]
        public void StackArrayOfBytes() {
            byte[] arr = new byte[] {0x00, 0x01, 0x02 };
            bytes.push(arr);

            Assert.AreEqual(bytes.pop(), 0x02);
            Assert.AreEqual(bytes.pop(), 0x01);
            Assert.AreEqual(bytes.pop(), 0x00);

            bytes.push(arr);
            arr = bytes.pop(3);
            Assert.AreEqual(arr[0], 0x02);
            Assert.AreEqual(arr[1], 0x01);
            Assert.AreEqual(arr[2], 0x00);
        }

        [Test]
        public void PopInstruction() {
            bytes.push(LiteralFactory.CreateStringLiteral("Hello world"));
            bytes.push(LiteralFactory.CreateIntLiteral(5));
            bytes.push(LiteralFactory.CreateBoolLiteral(true));
            bytes.push((byte) Instruction.EFFECT_DELIMITER);

            byte[] instructionArr = bytes.popInstruction(dummyCallback);
            Assert.AreEqual(1, instructionArr.Length);
            Assert.AreEqual(Instruction.EFFECT_DELIMITER, (Instruction) instructionArr[0]);

            byte[] boolArr = bytes.popInstruction(dummyCallback);
            Assert.AreEqual(2, boolArr.Length);
            bytes.push(boolArr);
            Assert.AreEqual(true, bytes.ReadBoolLiteral(dummyCallback));
 
            byte[] intArr = bytes.popInstruction(dummyCallback);
            Assert.AreEqual(5, intArr.Length);
            bytes.push(intArr);
            Assert.AreEqual(5, bytes.ReadIntLiteral(dummyCallback));

            byte[] stringArr = bytes.popInstruction(dummyCallback);
            bytes.push(stringArr);
            Assert.AreEqual("Hello world", bytes.ReadStringLiteral(dummyCallback));
        }

        [Test]
        public void IntBytecode([NUnit.Framework.Range(0, 100, 25)] int num)
        {
            byte[] arr = LiteralFactory.CreateIntLiteral(num);
            bytes.push(arr);
            int n = bytes.ReadIntLiteral(dummyCallback);

            Assert.AreEqual(num, n);
        }

        [Test]
        public void NegativeIntBytecode()
        {
            byte[] arr = LiteralFactory.CreateIntLiteral(-5);
            bytes.push(arr);
            int n = bytes.ReadIntLiteral(dummyCallback);

            Assert.AreEqual(-5, n);
        }

        [Test]
        public void StringBytecode()
        {
            bytes.push(LiteralFactory.CreateStringLiteral("Hello World 👋"));
            bytes.push(LiteralFactory.CreateStringLiteral("The quick brown fox jumped over the lazy dog."));
            bytes.push(LiteralFactory.CreateStringLiteral("Hello World!"));
            bytes.push(LiteralFactory.CreateStringLiteral("A"));
            
            Assert.AreEqual(bytes.ReadStringLiteral(dummyCallback), "A");
            Assert.AreEqual(bytes.ReadStringLiteral(dummyCallback), "Hello World!");
            Assert.AreEqual(bytes.ReadStringLiteral(dummyCallback), "The quick brown fox jumped over the lazy dog.");
            Assert.AreEqual(bytes.ReadStringLiteral(dummyCallback), "Hello World 👋");
        }

        [Test]
        public void BoolBytecode()
        {
            bytes.push(LiteralFactory.CreateBoolLiteral(true));
            bytes.push(LiteralFactory.CreateBoolLiteral(false));

            Assert.IsFalse(bytes.ReadBoolLiteral(dummyCallback));
            Assert.IsTrue(bytes.ReadBoolLiteral(dummyCallback));
        }

        [Test]
        public void PlayerBytecode()
        {
            game.Players = new PlayerManager(3);
            GamePlayer[] players = game.Players.GetPlayers();

            bytes.push(LiteralFactory.CreatePlayerLiteral(players[0]));
            bytes.push(LiteralFactory.CreatePlayerLiteral(players[1]));
            bytes.push(LiteralFactory.CreatePlayerLiteral(players[2]));

            Assert.AreSame(game.ReadPlayerFromStack(), players[2]);
            Assert.AreSame(game.ReadPlayerFromStack(), players[1]);
            Assert.AreSame(game.ReadPlayerFromStack(), players[0]);
        }

        private class TestCard : Card {
            public TestCard () {
                SetID();
            }
        }

        [Test]
        public void CardBytecode()
        {
            Card card1 = new TestCard();
            Card card2 = new TestCard();
            Card card3 = new TestCard();

            bytes.push(LiteralFactory.CreateCardLiteral(card1.GetID()));
            bytes.push(LiteralFactory.CreateCardLiteral(card2.GetID()));
            bytes.push(LiteralFactory.CreateCardLiteral(card3.GetID()));

            Assert.AreEqual(card3.GetID(), bytes.ReadCardLiteral(dummyCallback));
            Assert.AreEqual(card2.GetID(), bytes.ReadCardLiteral(dummyCallback));
            Assert.AreEqual(card1.GetID(), bytes.ReadCardLiteral(dummyCallback));
        }

        [Test]
        public void ConditionBytecode()
        {
            CompareBool compbool1 = new CompareBool(true, true, ConditionOperator.EQUAL);
            bytes.push(LiteralFactory.CreateConditionLiteral(compbool1));
            Condition boolCondition1 = bytes.ReadConditionLiteral(dummyCallback);
            Assert.IsTrue(boolCondition1.Evaluate());

            CompareBool compbool2 = new CompareBool(true, true, ConditionOperator.NOT_EQUAL);
            bytes.push(LiteralFactory.CreateConditionLiteral(compbool2));
            Condition boolCondition2 = bytes.ReadConditionLiteral(dummyCallback);
            Assert.IsFalse(boolCondition2.Evaluate());

            CompareNum compnum1 = new CompareNum(2, 4, ConditionOperator.LESS_THAN);
            bytes.push(LiteralFactory.CreateConditionLiteral(compnum1));
            Condition numCondition1 = bytes.ReadConditionLiteral(dummyCallback);
            Assert.IsTrue(numCondition1.Evaluate());

            CompareNum compnum2 = new CompareNum(2, -2, ConditionOperator.LESS_THAN);
            bytes.push(LiteralFactory.CreateConditionLiteral(compnum2));
            Condition numCondition2 = bytes.ReadConditionLiteral(dummyCallback);
            Assert.IsFalse(numCondition2.Evaluate());
        }

        [Test]
        public void PlayerListBytecode()
        {
            game.Players = new PlayerManager(3);
            List<GamePlayer> players = new List<GamePlayer>(game.Players.GetPlayers());
            bytes.push(LiteralFactory.CreateListLiteral(players));
            List<GamePlayer> readPlayers = game.ReadPlayerListFromStack();

            Assert.AreEqual(players.Count, readPlayers.Count);
            
            foreach (GamePlayer player in readPlayers) {
                Assert.IsTrue(players.Contains(player));
            }
        }

        [Test]
        public void CardListBytecode()
        {
            List<Card> cards = new List<Card>(new Card[]{
                new TestCard(),
                new TestCard(),
                new TestCard()
            });
            bytes.push(LiteralFactory.CreateListLiteral(cards));

            foreach (Card card in cards) {
                game.Cards.Deck.AddCard(card, DeckLocation.TOP);
            }

            List<Card> readCards = game.ReadCardListFromStack();

            Assert.AreEqual(cards.Count, readCards.Count);
            foreach (Card card in readCards) {
                Assert.IsTrue(cards.Contains(card));
            }
        }
    }
}