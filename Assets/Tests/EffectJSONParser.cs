using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using System.IO;

namespace Tests
{

    [TestFixture]
    public class EffectJSONParser
    {
        [Test]
        public void ReadJSONFile () {
            using (StreamReader reader = new StreamReader("Assets/effects.json")) {
                string json = reader.ReadToEnd();
                Debug.Log(json);
            }
        }

        [Test]
        public void ParseJSON () {
            using (StreamReader reader = new StreamReader("Assets/effects.json")) {
                string json = reader.ReadToEnd();

            }
        }
    }
}
