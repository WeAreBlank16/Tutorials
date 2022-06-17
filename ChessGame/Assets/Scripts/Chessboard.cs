using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour
{
    [Header("ArtStuff")]
    [SerializeField] private Material tileMaterial;


    //Logic Feild
    private const int tileCountX = 8;
    private const int tileCountY = 8;
    private GameObject[,] tiles;
    private void Awake()
    {
        GenerateAllTiles(1, tileCountX, tileCountY);
    }
    private void Update()
    {
        if (!currentCamera)
        {
            currentCamera = Camera.current;
            return;
        }
    }

    //Generate the board
    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY)
    {
        tiles = new GameObject[tileCountX, tileCountY];
        for (int x = 0; x < tileCountX; x++)
        {
            for (int y = 0; y < tileCountY; y++)
            {
                tiles[x, y] = GenerateSingleTile(tileSize, x, y);
            }
        }

    }
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        GameObject tileObject = new GameObject(String.Format("X:{0}, Y{1}", x, y));
        tileObject.transform.parent = transform;

        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, 0, y * tileSize);
        vertices[1] = new Vector3(x * tileSize, 0, (y + 1) * tileSize);
        vertices[2] = new Vector3((x + 1) * tileSize, 0, y * tileSize);
        vertices[3] = new Vector3((x + 1) * tileSize, 0, (y + 1) * tileSize);

        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        tileObject.AddComponent<BoxCollider>(); 
        return tileObject;
    }
}
