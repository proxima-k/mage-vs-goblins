using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ability {
    [Serializable]
    public class Attribute<T> {
        [SerializeField] private List<T> attributeList;
        private int _levelIndex;
        public T Value => attributeList[_levelIndex];

        // todo: perhaps make the default list consist of a single element with value of the type's default
        public Attribute(List<T> attributeAttributeList = default) {
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
    }
}