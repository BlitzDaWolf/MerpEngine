using MerpEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngineTests
{
    public class LevelTest
    {
        Level level;

        [SetUp]
        public void Setup()
        {
            level = new Level() { Name = "test" };
        }

        [Test]
        public void CorrectName()
        {
            Assert.AreEqual(level.Name, "test");
        }
    }
}
