using XiangJiang.Infrastructure.Serializer.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XiangJiang.Infrastructure.Abstractions;
using XiangJiang.Infrastructure.Serializer.JsonTests.Models;

namespace XiangJiang.Infrastructure.Serializer.Json.Tests
{
    [TestClass]
    public class JsonSerializerTests
    {
        private string _jsonText;
        private Person _person;
        private ISerializer _serializer;

        [TestInitialize]
        public void Init()
        {
            _serializer = new JsonSerializer();
            _person = new Person { Password = "123", UserName = "zhangsan" };
            _jsonText = "{'userName':'zhangsan','password':'123'}";
        }

        [TestMethod]
        public void SerializeTest()
        {
            var actual = _serializer.Serialize(_person);
            Assert.IsTrue(JsonHelper.IsValidJsonText(actual));
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            var actual = _serializer.Deserialize<Person>(_jsonText);
            Assert.IsNotNull(actual);
            Assert.AreEqual(_person.Password, actual.Password);
            Assert.AreEqual(_person.UserName, actual.UserName);
        }
    }
}