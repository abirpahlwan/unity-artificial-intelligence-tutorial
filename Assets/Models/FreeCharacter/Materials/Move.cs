using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeCharacter {
    public class Move : MonoBehaviour {

        Vector3 goal = new Vector3(5, 0, 4);

        void Start() {
            this.transform.Translate(goal);
        }

        void Update() {

        }
    }
}
