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
    //public GameObject portalPrefab;       // Prefab do portal (Collider2D isTrigger + PortalManager)
    public GameObject playerPrefab;       // Prefab do player (TAG "Player" + Rigidbody2D + Collider2D)



    public int obstacleLayerY = 2; // altera geração: “passo” entre linhas de obstáculos (1 = todas as linhas; 2 = linhas alternadas)
    //public int minObstacleGapX = 2; // altera geração: espaçamento mínimo após spawnar um obstáculo (garante passagens)
    //public int maxObstacleGapX = 4;  // altera geração: espaçamento máximo após spawnar um obstáculo

    public int minNoSpawnX = 1; // altera geração: avanço (gap) mínimo quando NÃO spawna 
    public int maxNoSpawnX = 2; // altera geração: avanço (gap) máximo quando NÃO spawna

    public int width => playableWidth; // Somente leitura — LARGURA da área jogável (em unidades/tiles). Para mudar, altere 'playableWidth' no Inspector.
    public int height => playableHeight; // Somente leitura — ALTURA  da área jogável (em unidades/tiles). Usada para gerar piso, paredes e distribuir obstáculos.
    public int offset => WallOffset; // Somente leitura — Distância (em unidades) entre a borda da área jogável e as PAREDES VISUAIS. Aumentar o offset/WallOffset muda apenas o visual;
                                     // não muda a área jogável.


    void Start()
    {
        // Se já gerou uma vez nesta cena, sai (previne duplicar)
        if (hasGeneratedThisScene) return;

        //Definindo como true, ou seja,instância já foi gerada
        hasGeneratedThisScene = true;

        //Função para gerar chão, paredes, obstáculos, portal e player proceduralmente
        GenerateRoom();

    }



    void GenerateRoom()
    {
        if (obstacleLines == null || obstacleLines.Length == 0) return;

        // ponto inicial em Y
        float currentY = 0f; // ou qualquer valor inicial desejado
        float xPosition = 0f; // posição fixa em X para todas as linhas

        // Defina quantas linhas você quer gerar
        int numLines = playableHeight; // ou outra lógica

        for (int i = 0; i < numLines; i++)
        {
            GameObject linePrefab = obstacleLines[Random.Range(0, obstacleLines.Length)];

            GameObject newLine = Instantiate(linePrefab, new Vector2(xPosition, currentY), Quaternion.identity, transform);

            float lineHeight = newLine.GetComponent<BoxCollider2D>().size.y * newLine.transform.localScale.y;
            Debug.Log($"Linha {i} tem altura {lineHeight}");

            currentY += lineHeight;
        }

        // por último, spawn do player
        Vector2 playerPos = new Vector2(xPosition, 1f);
        Instantiate(playerPrefab, playerPos, Quaternion.identity, transform);
    }


}