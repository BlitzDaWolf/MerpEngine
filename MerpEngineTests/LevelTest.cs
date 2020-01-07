﻿using MerpEngine;
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
            level.compoments.Add(new testCompoment());
        }

        [Test]
        public void CorrectName()
        {
            Assert.AreEqual("test", level.Name);
        }

        [Test]
        public void HasCompoments()
        {
            Assert.AreEqual(1, level.compoments.Count);
        }
    }
}
