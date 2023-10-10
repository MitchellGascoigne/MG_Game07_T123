using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab; // Assign your 3D player prefab in the Inspector
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    public TMPro.TextMeshProUGUI PingText;

    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        PingText.text = "Ping: " + PhotonNetwork.GetPing();
    }
    private void Awake()
    {
        GameCanvas.SetActive(true); 
    }
    public void SpawnPlayer()
    {
        float randomValueX = Random.Range(-1f, 10f); // Adjust the range for X position
        float randomValueZ = Random.Range(-1f, 6f);  // Adjust the range for Z position

        // Set the Y position to -1
        float yPosition = -1f;

        Vector3 spawnPosition = new Vector3(randomValueX, yPosition, randomValueZ);

        PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPosition, Quaternion.identity, 0);

        // Disable or hide any UI or cameras as needed
        GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);

        Debug.Log("Player Spawned");
    }

}


