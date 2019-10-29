using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Person Data", menuName = "Person Data", order = 1)]
public class PersonData : ScriptableObject
{
    public string username;
    public Image[] pictures;
    public Mesh personMesh;
}
