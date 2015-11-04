using System.Threading;
using NUnit.Framework;

namespace Atomic.Tests
{
    [TestFixture]
    public class VolatileLongTests
    {
        private _Volatile.Long _volatile;
        private const long InitialValue = 2;
        private const long NewValue = 3;

        [SetUp]
        public void SetUp()
        {
            _volatile = new _Volatile.Long(InitialValue);
        }

        [Test]
        public void ReadAcquireFenceReturnsInitialValue()
        {
            Assert.AreEqual(InitialValue, _volatile.ReadAcquireFence());
        }

        [Test]
        public void ReadFullFenceReturnsInitialValue()
        {
            Assert.AreEqual(InitialValue, _volatile.ReadFullFence());
        }

        [Test]
        public void ReadCompilerOnlyFenceReturnsInitialValue()
        {
            Assert.AreEqual(InitialValue, _volatile.ReadCompilerOnlyFence());
        }

        [Test]
        public void ReadUnfencedReturnsInitialValue()
        {
            Assert.AreEqual(InitialValue, _volatile.ReadUnfenced());
        }

        [Test]
        [TestCase(100000000)]
        public void WriteReleaseFenceChangesInitialValue(int a)
        {
            Assert.AreEqual(100000000, a);
            for (var i = 0; i < a; i++)
            {
                _volatile.WriteReleaseFence(NewValue);
                Assert.AreEqual(NewValue, _volatile.ReadUnfenced());
            }
        }

        [Test]
        [TestCase(100000000)]
        public void WriteFullFenceChangesInitialValue(int a)
        {
            Assert.AreEqual(100000000, a);
            for (var i = 0; i < a; i++)
            {
                _volatile.WriteFullFence(NewValue);
                Assert.AreEqual(NewValue, _volatile.ReadUnfenced());
            }
        }

        [Test]
        [TestCase(100000000)]
        public void WriteCompilerOnlyFenceChangesInitialValue(int a)
        {
            Assert.AreEqual(100000000, a);
            for (var i = 0; i < a; i++)
            {
                _volatile.WriteCompilerOnlyFence(NewValue);
                Assert.AreEqual(NewValue, _volatile.ReadUnfenced());
            }
        }

        [Test]
        [TestCase(100000000)]
        public void WriteUnfencedInitialValue(int a)
        {
         Assert.AreEqual(100000000, a);
            for (var i = 0; i < a; i++)
            {
            _volatile.WriteUnfenced(NewValue);
            Assert.AreEqual(NewValue, _volatile.ReadUnfenced());
            }
        }

        [Test]
        public void AtomicCompareExchangeReturnsTrueIfComparandEqualsCurrentValue()
        {
            Assert.IsTrue(_volatile.AtomicCompareExchange(NewValue, InitialValue));
        }

        [Test]
        public void AtomicCompareExchangeMutatesValueIfComparandEqualsCurrentValue()
        {
            _volatile.AtomicCompareExchange(NewValue, InitialValue);
            Assert.AreEqual(NewValue, _volatile.ReadUnfenced());
        }

        [Test]
        public void AtomicCompareExchangeReturnsFalseIfComparandDifferentFromCurrentValue()
        {
            Assert.IsFalse(_volatile.AtomicCompareExchange(NewValue, InitialValue + 1));
        }

        [Test]
        public void AtomicExchangeReturnsInitialValue()
        {
            Assert.AreEqual(InitialValue, _volatile.AtomicExchange(NewValue));
        }

        [Test]
        public void AtomicExchangeMutatesValue()
        {
            _volatile.AtomicExchange(NewValue);
            Assert.AreEqual(NewValue, _volatile.ReadUnfenced());
        }

        [Test]
        public void AtomicAddAndGetReturnsNewValue()
        {
            const long delta = 5L;
            Assert.AreEqual(InitialValue + delta, _volatile.AtomicAddAndGet(delta));
        }

        [Test]
        public void AtomicAddAndGetMutatesValue()
        {
            const long delta = 5L;
            _volatile.AtomicAddAndGet(delta);
            Assert.AreEqual(InitialValue + delta, _volatile.ReadUnfenced());
        }

        [Test]
        public void AtomicIncrementAndGetReturnsNewValue()
        {
            Assert.AreEqual(InitialValue + 1L, _volatile.AtomicIncrementAndGet());
        }

        [Test]
        public void AtomicIncrementAndGetMutatesValue()
        {
            _volatile.AtomicIncrementAndGet();
            Assert.AreEqual(InitialValue + 1L, _volatile.ReadUnfenced());
        }

        [Test]
        public void AtomicDecrementAndGetReturnsNewValue()
        {
            Assert.AreEqual(InitialValue - 1L, _volatile.AtomicDecrementAndGet());
        }

        [Test]
        public void AtomicDecrementAndGetMutatesValue()
        {
            _volatile.AtomicDecrementAndGet();
            Assert.AreEqual(InitialValue - 1L, _volatile.ReadUnfenced());
        }

        [Test]
        public void ToStringReturnsInitialValueAsString()
        {
            Assert.AreEqual(InitialValue.ToString(), _volatile.ToString());
        }
    }
}