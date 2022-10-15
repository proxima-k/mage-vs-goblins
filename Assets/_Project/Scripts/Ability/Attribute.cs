using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem {
    [Serializable]
    public class Attribute<T> {
        public string Name;        
        
        [SerializeField] private List<T> attributeList;
        private int _levelIndex;
        public T Value => attributeList[_levelIndex];

        // todo: perhaps make the default list consist of a single element with value of the type's default
        public Attribute(string name, List<T> attributeAttributeList = default) {
            Name = name;
            _levelIndex = 0;
            attributeList = attributeAttributeList;
        }

        // SetLevel() -> for data saving purposes?
        
        public void NextLevel() {
            if (_levelIndex < attributeList.Count - 1) {
                _levelIndex++;
            }
        }

        public void PreviousLevel() {
            if (_levelIndex > 0) {
                _levelIndex--;
            }
        }

        public void ResetLevel() {
            _levelIndex = 0;
        }

        public string GetStringForLevelDifference() {
            if (_levelIndex < attributeList.Count - 1)
                return attributeList[_levelIndex + 1].ToString();
            return "Maxed";
        }
    }
}