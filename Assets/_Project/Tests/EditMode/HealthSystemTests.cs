using NUnit.Framework;

namespace Tests {
    public class HealthSystemTests {
        [Test]
        public void InitialHealth() {
            HealthSystem healthSystem = new HealthSystem(100);
            Assert.AreEqual(100, healthSystem.Health);

            healthSystem = new HealthSystem(50, 100);
            Assert.AreEqual(50, healthSystem.Health);
        }

        [Test] 
        public void Heal() {
            HealthSystem healthSystem = new HealthSystem(50, 100);
            healthSystem.Heal(23);
            Assert.AreEqual(50+23, healthSystem.Health);
            
            healthSystem.Heal(50);
            Assert.AreEqual(100, healthSystem.Health);
        }

        [Test]
        public void Damage() {
            HealthSystem healthSystem = new HealthSystem(100);
            healthSystem.Damage(50);
            Assert.AreEqual(50, healthSystem.Health);
            
            healthSystem.Damage(200);
            Assert.AreEqual(0, healthSystem.Health);
        }
    }
}
