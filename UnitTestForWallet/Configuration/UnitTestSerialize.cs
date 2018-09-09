using System;
using Xunit;
using BusinessLayer;

namespace UnitTestForWallet
{
    public class UnitTestSerialize
    {
        [Fact]
        public void TestSerialize()
        {
            Config.Load();
        }
    }
}
