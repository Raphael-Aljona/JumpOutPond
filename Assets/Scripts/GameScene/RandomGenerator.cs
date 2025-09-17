using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    private bool hasGeneratedThisScene = false;

    //Area Jogavel
    public int playableWidth = 12; //x 
    public int playableHeight = 20; //y

    public GameObject[] obstacleLines;

    //Moldura
    public int WallOffset = 10; // altera geração/câmera: distância da moldura até a área jogável (para a parade esquerda: -offset; para a direita: width +offset)

    public GameObject[] obstaclePrefabs;  // Prefabs dos obstáculos (usem TAG "Ground")
    public GameObject playerPrefab  ;       // Prefab do player (TAG "Player" + Rigidbody2D + Collider2D)

    public int obstacleLayerY = 2; // altera geração: “passo” entre linhas de obstáculos (1 = todas as linhas; 2 = linhas alternadas)

    public int minNoSpawnX = 1; // altera geração: avanço (gap) mínimo quando NÃO spawna 
    public int maxNoSpawnX = 2; // altera geração: avanço (gap) máximo quando NÃO spawna

    public int width => playableWidth; // Somente leitura — LARGURA da área jogável (em unidades/tiles). Para mudar, altere 'playableWidth' no Inspector.
    public int height => playableHeight; // Somente leitura — ALTURA  da área jogável (em unidades/tiles). Usada para gerar piso, paredes e distribuir obstáculos.
    public int offset => WallOffset; // Somente leitura — Distância (em unidades) entre a borda da área jogável e as PAREDES VISUAIS. Aumentar o offset/WallOffset muda apenas o visual;
                                     // não muda a área jogável.

    public Camera camera;

    public float spawnLinesTop = 5f;
    float currentY = 0f;

    private Queue<GameObject> activeLines = new Queue<GameObject> ();

    float streetCount = 0;
    float grassCount = 0;

    void Start()
    {
        

        for (int i = 0; i < 10; i++)
        {
            SpawnNextLine();

        }

    }

    private void Update()
    {
        float cameraTop = camera.transform.position.y + camera.orthographicSize;

        while (cameraTop + spawnLinesTop > currentY)
        {
            SpawnNextLine();
        }

        float cameraBottom = camera.transform.position.y - camera.orthographicSize;
        while (activeLines.Count > 0 && activeLines.Peek().transform.position.y + 10f < cameraBottom)
        {
            Destroy (activeLines.Dequeue());
        }
    }


    void SpawnNextLine()
    {
        GameObject prefab = SelectPrefab();
        GameObject line = Instantiate(prefab, new Vector2(0, currentY), Quaternion.identity, transform);

        float h = line.GetComponent<BoxCollider2D>().size.y * line.transform.localScale.y;
        currentY += h;

        activeLines.Enqueue(line);

        if (line.CompareTag("Street"))
        {
            streetCount+= 1;
            grassCount = 0;
            Debug.Log($"{streetCount} rua seguida");
        }
        else { 
            grassCount+= 1;
            streetCount = 0;
            Debug.Log("contagem resetada"); 
        }
    }

    GameObject SelectPrefab()
    {
        if(streetCount >= 3)
        {
            var isNotStreet = obstacleLines.Where(e => !e.CompareTag("Street")).ToArray();

            if(isNotStreet.Length > 0)
            {
                return isNotStreet[Random.Range(0, isNotStreet.Length)];
            }
        }
        if(grassCount >= 3)
        {
            var isNotGrass = obstacleLines.Where(e => e.CompareTag("Street")).ToArray();
            if (isNotGrass.Length > 0)
            {
                return isNotGrass[Random.Range(0, isNotGrass.Length)];
            }
        }

        return obstacleLines[Random.Range(0, obstacleLines.Length - 1)];
    }
}