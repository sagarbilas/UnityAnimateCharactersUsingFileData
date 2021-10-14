using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColor : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    void start()
    {      
        myMaterial.color = Color.green;
    } 
}
