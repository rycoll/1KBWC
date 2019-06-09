using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BytecodeStackTest
    {

        ByteManager bytes = new ByteManager();

        [Test]
        public void TestStackIndividualBytes() {
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
        public void TestStackArrayOfBytes() {
            byte[] arr = new byte[] {0x00, 0x01, 0x02 };
            bytes.push(arr);

            Assert.AreEqual(bytes.pop(), 0x02);
            Assert.AreEqual(bytes.pop(), 0x01);
            Assert.AreEqual(bytes.pop(), 0x00);

            bytes.push(arr);
            arr = bytes.pop(3);
            Assert.AreEqual(arr[0], 0x00);
            Assert.AreEqual(arr[1], 0x01);
            Assert.AreEqual(arr[2], 0x02);
        }

        [Test]
        public void TestIntBytecode([NUnit.Framework.Range(0, 100, 25)] int num)
        {
            byte[] arr = LiteralFactory.CreateIntLiteral(num);
            bytes.push(arr);
            int n = bytes.ReadIntLiteral();

            Assert.AreEqual(num, n);
        }

    }
}