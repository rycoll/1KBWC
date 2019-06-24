using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class BytecodePrinterTest
    {

        ByteManager bytes;
        GameMaster game;

        [SetUp]
        public void SetUp () {
            game = new GameMaster();
            bytes = game.Bytes;
        }

        [Test]
        public void Int()
        {
            bytes.push(LiteralFactory.CreateIntLiteral(4));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                "INT(4) "
            );
        }

        [Test]
        public void String()
        {
            bytes.push(LiteralFactory.CreateStringLiteral("hello world"));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                "STRING(hello world) "
            );
        }

        [Test]
        public void Bool()
        {
            bytes.push(LiteralFactory.CreateBoolLiteral(true));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                "BOOL(True) "
            );
        }

        [Test]
        public void Player()
        {
            bytes.push(LiteralFactory.CreatePlayerLiteral(1));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                "PLAYER(1) "
            );
        }

        [Test]
        public void Card()
        {
            bytes.push(LiteralFactory.CreateCardLiteral(5));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                "CARD(5) "
            );
        }

        private class TestCard : Card {
            public TestCard () {
                SetID();
            }
        }

        [Test]
        public void List()
        {   
            game.Players = new PlayerManager(5);
            GamePlayer[] playerList = game.Players.GetPlayers();
            bytes.push(LiteralFactory.CreateListLiteral(
                new List<GamePlayer>(playerList)
            ));

            List<Card> cardList = new List<Card>{
                new TestCard()
            };
            bytes.push(LiteralFactory.CreateListLiteral(cardList));

            Assert.AreEqual(
                bytes.ReportStackContent(),
                "LIST(size:1,type:CARD) LIST(size:5,type:PLAYER) "
            );
        }

        [Test]
        public void Placeholder()
        {
            bytes.push(LiteralFactory.CreatePlaceholderLiteral(99));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                "PLACEHOLDER(id:99) "
            );
        }

        [Test]
        public void Chunk()
        {
            byte[] someBytes = LiteralFactory.CreateStringLiteral("hello");
            bytes.push(LiteralFactory.CreateChunkLiteral(someBytes));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                $"CHUNK(size:{someBytes.Length}) STRING(hello) "
            );
        }

        [Test]
        public void Enum()
        {
            bytes.push(LiteralFactory.CreateEnumLiteral(200));
            Assert.AreEqual(
                bytes.ReportStackContent(),
                "enum:200 "
            );
        }
    }
}
