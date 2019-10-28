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
            bytes.push(InstructionFactory.Make_Loop(
                LiteralFactory.CreateIntLiteral(numLoops),
                // have to reverse this manually since not handled by InstructionFactory or whatever
                new byte[]{0002, 0001, 0000}
            ));
            game.ExecuteNext();
            for (int i = 0; i < numLoops; i++) {
                Assert.AreEqual(bytes.pop(), 0000);
                Assert.AreEqual(bytes.pop(), 0001);
                Assert.AreEqual(bytes.pop(), 0002);
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
