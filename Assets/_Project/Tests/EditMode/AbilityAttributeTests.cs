using System;
using System.Collections.Generic;
using NUnit.Framework;
using AbilitySystem;

namespace Tests {
    public class AbilityAttributeTests {
        // [Test]
        // public void AttributeLevelSetup() {
            // List<int> attributeList = new List<int> {1, 3, 7, 10};
        // }
        
        [Test]
        public void GetAttributeValue() {
            List<int> attributeList = new List<int> {1, 3, 7, 10};
            Attribute<int> projectileDamage = new Attribute<int>(attributeList);
            
            Assert.AreEqual(attributeList[0], projectileDamage.Value);
        }

        [Test]
        public void LevelUpAttribute() {
            List<int> attributeList = new List<int> {1, 3, 7, 10};
            Attribute<int> projectileDamage = new Attribute<int>(attributeList);
            
            // 4th level
            projectileDamage.NextLevel();
            projectileDamage.NextLevel();
            projectileDamage.NextLevel();
            Assert.AreEqual(attributeList[3], projectileDamage.Value);
            
            // max level test
            projectileDamage.NextLevel();
            Assert.AreEqual(attributeList[attributeList.Count-1], projectileDamage.Value);
        }
    }
}