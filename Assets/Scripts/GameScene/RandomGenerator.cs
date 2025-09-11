using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    private bool hasGeneratedThisScene = false;

    //Area Jogavel
    public int playableWidth = 12; //x 
    public int playableHeight = 8; //y

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
        // Verifica se há linhas de obstáculos para instanciar
        if (obstacleLines != null && obstacleLines.Length > 0)
        {
            // A posição inicial das linhas de obstáculos será logo acima da "moldura"
            int firstObstacleY = 2;
            int lastObstacleY = Mathf.Max(2, playableHeight - 3);

            // Gerar as linhas de obstáculos em posições verticais (uma em cima da outra)
            for (int tileY = firstObstacleY; tileY <= lastObstacleY; tileY++)
            {
                // Aqui escolhemos uma linha de obstáculos aleatória do array
                int lineIndex = Random.Range(0, obstacleLines.Length);
                GameObject line = obstacleLines[lineIndex];

                // Instancia a linha de obstáculos na posição correta (baseada no tileY)
                Instantiate(line, new Vector2(0, tileY), Quaternion.identity, transform);
            }
        }

        // Posicionamento do jogador
        int playerSpawnX = -WallOffset + 1;
        int playerSpawnY = 1; // Posição inicial do player (logo acima do chão)

        // Gera o player no local correto
        Instantiate(playerPrefab, new Vector2(playerSpawnX, playerSpawnY), Quaternion.identity, transform);
    }

}