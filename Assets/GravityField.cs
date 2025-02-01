using Unity.VisualScripting;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public int priority = 0;
    public float gravityStrength = 2f;
    public FieldType fieldType;
}


public enum FieldType
{
    spherical,
    directional,
    transition

}


