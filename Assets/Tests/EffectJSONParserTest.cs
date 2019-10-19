using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;

namespace Tests
{
    [System.Serializable]
    public class ParseFieldObject {
        public string type;
        public string desc;
    }

    [System.Serializable]
    public class ParseObject {
        public string name;
        public string type;
        public string instruction;
        public string message;
        public string returnType;
        public ParseFieldObject[] fields;
        public bool canBeRoot;
    }

    public class ParseFullJSON {
        public List<ParseObject> effects;
    }

    [TestFixture]
    public class EffectJSONParserTest
    {
        [Test]
        public void ReadJSONFile () {
            string json = EffectJSONParser.ReadJSON("Assets/effects.json");
            Assert.Greater(json.Length, 0);
        }

        [Test]
        public void ParseJSON () {
            string json = EffectJSONParser.ReadJSON("Assets/effects.json");
            List<ParsedEffect> list = EffectJSONParser.ParseJSON(json);
            Assert.Greater(list.Count, 0);
        }

        [Test]
        public void ConvertJSON () {
            string json = EffectJSONParser.ReadJSON("Assets/effects.json");
            List<ParsedEffect> list = EffectJSONParser.ParseJSON(json);
            List<EffectData> effects = EffectJSONParser.ConvertParsedJSON(list);
            Assert.Greater(effects.Count, 0);
        }
        
    }
}
