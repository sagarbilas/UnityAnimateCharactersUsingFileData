using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Player1;

public class PlayerNew : MonoBehaviour
{
    public int count = 0;

    public GameObject playerOriginal;

    void Start()
    {
        var dataset = Resources.Load<TextAsset>("test5");
        var splitDataset = dataset.text.Split('\n');
        var DoubleOfNumberOfCharacters = splitDataset[0].Split(',').Length;         // DoubleOfNumberOfCharacters= number of columns in the dataset. No of agents= DoubleOfNumberOfCharacters/2
        count = (DoubleOfNumberOfCharacters/2) -1;

        //GameObject PlayerClone = Instantiate(playerOriginal);
        CreateAgents();
    }

    //creating agents
    private void CreateAgents()
    {
        for (int i=0; i<count; i++)
        {
            GameObject PlayerClone = Instantiate(playerOriginal, new Vector3(256.309f, playerOriginal.transform.position.y, 198.922f), playerOriginal.transform.rotation);

            Player1 mover = PlayerClone.AddComponent<Player1>();
            mover.playerIndex = i;
        }

    }
}
