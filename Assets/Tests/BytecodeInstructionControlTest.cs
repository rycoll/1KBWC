using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class BytecodeInstructionControlTest
    {
        ByteManager bytes;
        GameMaster game;

        [SetUp]
        public void SetUp () {
            game = new GameMaster();
            bytes = game.Bytes;
        }

        [Test]
        public void IfStatement() {
            CompareNum compare = new CompareNum(1, 2, ConditionOperator.EQUAL);
            byte[] if_false = InstructionFactory.Make_If(
                new byte[]{255},
                LiteralFactory.CreateConditionLiteral(compare)
            );

            bytes.push(if_false);
            game.ExecuteNext();
            byte endif = bytes.pop();
            Assert.AreEqual(endif, (byte) Instruction.ENDIF);
            Assert.IsFalse(bytes.HasBytes());

            compare = new CompareNum(1, 2, ConditionOperator.NOT_EQUAL);
            byte[] if_true = InstructionFactory.Make_If(
                new byte[]{255},
                LiteralFactory.CreateConditionLiteral(compare)
            );

            bytes.push(if_true);
            game.ExecuteNext();
            Assert.IsTrue(bytes.pop() == 255);            
        }

        [Test]
        public void Loop() {
            int numLoops = 3;

            List<byte> loopCode = new List<byte>();
            loopCode.Insert(0, (byte) Instruction.EFFECT_DELIMITER);
            loopCode.InsertRange(0, LiteralFactory.CreateIntLiteral(4));
            loopCode.Insert(0, (byte) Instruction.EFFECT_DELIMITER);
            loopCode.InsertRange(0, LiteralFactory.CreateStringLiteral("Hello world"));
            loopCode.Insert(0, (byte) Instruction.EFFECT_DELIMITER);
            loopCode.InsertRange(0, LiteralFactory.CreateBoolLiteral(true));

            bytes.push(InstructionFactory.Make_Loop(
                LiteralFactory.CreateIntLiteral(numLoops),
                loopCode.ToArray()
            ));

            game.ExecuteNext();
 
            for (int i = 0; i < numLoops; i++) {
                Assert.AreEqual(true, bytes.ReadBoolLiteral(game.queryCheck));
                Assert.AreEqual(Instruction.EFFECT_DELIMITER, (Instruction) bytes.pop());
                Assert.AreEqual("Hello world", bytes.ReadStringLiteral(game.queryCheck));
                Assert.AreEqual(Instruction.EFFECT_DELIMITER, (Instruction) bytes.pop());
                Assert.AreEqual(4, bytes.ReadIntLiteral(game.queryCheck));
                Assert.AreEqual(Instruction.EFFECT_DELIMITER, (Instruction) bytes.pop());
            }
        }

        [Test]
        public void ForLoop() {
            game.Players = new PlayerManager(2);
            GamePlayer P1 = game.Players.GetPlayer(0);
            GamePlayer P2 = game.Players.GetPlayer(1);

            byte[] items = new byte[]{ (byte) Instruction.GET_ALL_PLAYERS };

            byte[] code = InstructionFactory.Make_SetPlayerPoints(
                LiteralFactory.CreateIntLiteral(50),
                LiteralFactory.CreatePlaceholderLiteral(0)
            );

            bytes.push(InstructionFactory.Make_ForLoop(items, code, 0));

    
            Assert.AreNotEqual(50, P1.Points);
            Assert.AreNotEqual(50, P2.Points);

            while(bytes.HasBytes()) {
                game.ExecuteNext();
            }

            Assert.AreEqual(50, P1.Points);
            Assert.AreEqual(50, P2.Points);
        }

        [Test]
        public void NestedForLoop() {
            throw new System.Exception("Test not implemented yet 😢");
        }
    }
}
