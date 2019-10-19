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
            bytes.ClearStack();
            List<GamePlayer> list = new List<GamePlayer>{
                new GamePlayer("P1", 0),
                new GamePlayer("P2", 1)
            };
            byte[] items = LiteralFactory.CreateListLiteral(list);

            List<byte[]> chunkList = new List<byte[]>{
                new byte[]{1, 2},
                new byte[]{3, 4},
                new byte[0],
            };

            bytes.push(InstructionFactory.Make_ForLoop(items, chunkList));

            game.ExecuteNext();
            
            for (int i = 0; i < list.Count; i++) {
                Assert.AreEqual(1, bytes.pop());
                Assert.AreEqual(2, bytes.pop());
                Assert.AreEqual(bytes.ReadPlayerLiteral(game.queryCheck), i);
                Assert.AreEqual(3, bytes.pop());
                Assert.AreEqual(4, bytes.pop());
                Assert.AreEqual(bytes.ReadPlayerLiteral(game.queryCheck), i);
            }
        }

        
        [Test]
        public void NestedForLoop() {
            // for each player:
            //   for each card in that players hand:
            //      discard card
            throw new System.Exception("Test not implemented yet 😢");
        }
    }
}
