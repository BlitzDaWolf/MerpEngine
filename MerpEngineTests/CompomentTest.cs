using MerpEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngineTests
{
    public class CompomentTest
    {
        testCompoment test;

        [SetUp]
        public void Setup()
        {
            test = new testCompoment();
        }

        [Test]
        public void CompomentStart()
        {
            test.Start();
            Assert.AreEqual(test.start, 1);
        }

        [Test]
        public void CompomentUpdate()
        {
            test.Update();
            Assert.AreEqual(test.update, 1);
        }

        [Test]
        public void CompomentDestroy()
        {
            test.Destroy();
            Assert.AreEqual(test.destroy, 1);
        }
    }

    public class testCompoment : Compoment
    {
        public int start = 0;
        public int update = 0;
        public int destroy = 0;

        public override void Start()
        {
            base.Start();
            start++;
        }

        public override void Update()
        {
            base.Update();
            update++;
        }

        public override void Destroy()
        {
            base.Destroy();
            destroy++;
        }
    }
}
