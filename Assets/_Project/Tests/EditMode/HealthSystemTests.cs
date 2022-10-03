using NUnit.Framework;

namespace Tests {
    public class HealthSystemTests {
        // A Test behaves as an ordinary method
        [Test]
        public void HealthSystemInitialize() {
            HealthSystem healthSystem = new HealthSystem(100);

            Assert.AreEqual(100, healthSystem.Health);
        }
    }
}
