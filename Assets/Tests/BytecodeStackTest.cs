using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute; 
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BytecodeStackTest
    {
        private GameController game;

        [Test]
        public void TestIntBytecode([NUnit.Framework.Range(0, 1000, 100)] int num)
        {
            Interpreter interpreter = new Interpreter(game);
            
        }

    }
}