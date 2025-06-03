using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoleSpawn : MonoBehaviour
{
    private int fieldSquare = 3;
    public GameObject MolePrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private void Start()
    {
        for (int i = 0; i < fieldSquare; i++)
        {
            for (int j = 0; j < fieldSquare; j++)
            {
                Instantiate(MolePrefab, new Vector3(i, 0f, j), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        scoreText.text = Mole.score.ToString();
    }
}
