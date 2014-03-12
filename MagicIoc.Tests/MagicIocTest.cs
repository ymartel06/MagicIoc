using System;
using MagicIoc.AutoLoadExample.Cache;
using MagicIoc.SimpleExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicIoc.Tests
{
    [TestClass]
    public class MagicIocTest
    {
        [TestMethod]
        public void SimpleIocTest()
        {           
            //act
            bool result = DependencyResolver.ServiceLocator.GetService<IMyService>().Test();

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AutoLoadTest()
        {
            //act
            bool result = DependencyResolver.ServiceLocator.GetService<IMyCacheService>().Test();

            //assert
            Assert.IsTrue(result);
        }
    }
}
