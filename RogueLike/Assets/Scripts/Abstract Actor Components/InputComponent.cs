using UnityEngine;
using System.Collections;

abstract public class InputComponent : MonoBehaviour {
    abstract public void ReadInputs(GameObject gameObject);
}
