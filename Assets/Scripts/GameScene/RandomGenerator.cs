using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    private bool hasGeneratedThisScene = false;

    //Area Jogavel
    public int playableWidth = 12; //x 
    public int playableHeight = 8; //y

    //Moldura
    public int WallOffset = 10; // altera geração/câmera: distância da moldura até a área jogável (para a parade esquerda: -offset; para a direita: width +offset)


    //Prefabs
    //public GameObject floorPrefab;        // Prefab do chão (use TAG "Ground")
    //public GameObject wallPrefab;         // Prefab da parede visual (NÃO usar TAG "Ground")
    public GameObject[] obstaclePrefabs;  // Prefabs dos obstáculos (usem TAG "Ground")
    //public GameObject portalPrefab;       // Prefab do portal (Collider2D isTrigger + PortalManager)
    public GameObject playerPrefab;       // Prefab do player (TAG "Player" + Rigidbody2D + Collider2D)

    //public string nextSceneName = ""; // Nome da próxima cena (opcional) — pode ser colocado no PortalManager se desejado

    //Zona de segurança (zona sem obstaculos)
    //[Min(0)] public int spawnSafeZoneWidth = 2; // altera geração: quantas colunas à esquerda ficarão sem obstáculos (0..spawnSafeZoneWidthLeft-1)
    //[Min(1)] public int portalSafeZoneWidthTop = 2; // altera geração: largura da área livre do portal (no topo-direito)
    //[Min(1)] public int portalSafeZoneHeight = 3; // altera geração: altura da área livre do portal (no topo-direito)


    // controle dos obstaculos

    //densidade dos obstaculos
    //[Range(0f, 1f)] public float obstacleChance = 0.8f; //  altera geração: chance de instanciar obstáculo (mais alto = mais denso, ou seja, mais obstáculos)

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

    // Retorna true se (tileX,tileY) está em área proibida para obstáculos (spawn à esquerda ou retângulo do portal no topo-direito)
    //bool IsExcluded(int tileX, int tileY)
    //{
    //    //SE a coluna está na área de spawn (a esquerda), não irá spawnar obstáculos
    //    if (tileX < Mathf.Clamp(spawnSafeZoneWidth, 0, playableWidth))
    //    {
    //        return true;
    //    }

    //    // Calcula início da área livre do portal no topo-direito
    //    int portalStartX = Mathf.Max(0, playableWidth - portalSafeZoneWidthTop);
    //    int portalStartY = Mathf.Max(0, playableHeight - portalSafeZoneHeight);

    //    //Se os tiles ficam dentro da área inical: x >= startX e y >= startY, bloqueia obstáculo
    //    if (tileX >= portalStartX && tileY >= portalStartY)
    //    {
    //        return true;
    //    }
    //    // Caso contrário, não aconteça nada, está permitido gerar
    //    return false;
    //}

    void GenerateRoom()
    {

        // Se temos prefabs de obstáculos, inicia distribuição em camadas
        if (obstaclePrefabs != null && obstaclePrefabs.Length > 0)
        {

            // Define a posição inicial (y) onde os obstáculos podem ser colocados
            // A primeira linha de obstáculos começará no Y = 2, garantindo que há um espaço livre acima do chão/ Deixa espaço acima do chão
            int firstObstacleY = 2;

            // Define a posição final (y) onde os obstáculos podem ser colocados
            // A última linha de obstáculos será posicionada em Y = playableHeight - 4 
            int lastObstacleY = Mathf.Max(2, playableHeight - 3);

            // Para cada linha de obstáculos, com o intervalo de linhas definido por obstacleLayerStepY
            // obstacleLayerStepY controla a distância entre as linhas de obstáculos
            for (int tileY = firstObstacleY; tileY <= lastObstacleY; tileY += Mathf.Max(1, obstacleLayerY))
            {
                // Define a posição inicial (x) para começar a verificar os obstáculos da esquerda para a direita
                // Inicia na coordenada x=1 (evita posicionar no limite esquerdo da tela)
                int tileX = 1;

                // Enquanto a coluna (x) estiver dentro dos limites visíveis da tela/jogo
                // O limite direito é em playableWidth-2 (evita colocar obstáculos na borda da tela)
                while (tileX <= playableWidth - 2)
                {
                    // Se área atual for uma posição excluída (spawn/portal), pula para a próxima célula à direita
                    // A função IsExcluded verifica se a área não deve ter obstáculos, por exemplo, no caso de spawn ou portal
                    //if (IsExcluded(tileX, tileY))
                    //{

                    //    tileX += 1; // Avança 1 unidade e tenta a próxima posição
                    //    continue; // Continua para a próxima iteração do loop

                    //}

                    // Decide se um obstáculo será gerado com base na probabilidade (obstacleChance)
                    // Se o valor aleatório for menor que obstacleChance, um obstáculo será gerado
                    //if (Random.value < obstacleChance)
                    //{
                    //    // Escolhe aleatoriamente um prefab de obstáculo do array de obstáculos
                    //    int prefabIndex = Random.Range(0, obstaclePrefabs.Length);

                    //    // Instancia o prefab do obstáculo na posição atual (tileX, tileY)
                    //    Instantiate(obstaclePrefabs[prefabIndex], new Vector2(tileX, tileY), Quaternion.identity, transform);

                    //    // Após gerar o obstáculo, pula alguns tiles para garantir espaçamento entre os obstáculos
                    //    // O espaçamento é aleatório, com valor entre minObstacleGapX e maxObstacleGapX
                    //    tileX += Random.Range(minObstacleGapX, maxObstacleGapX + 1);
                    //}
                    //else
                    //{
                    //    // Se não gerou um obstáculo, ainda assim avança alguns tiles para preencher o espaço
                    //    // O avanço é aleatório, com valor entre minAdvanceNoSpawnX e maxAdvanceNoSpawnX
                    //    tileX += Random.Range(minNoSpawnX, maxNoSpawnX + 1);
                    //}


                }

            }

        }

        // Define a posição do portal no topo-direito da tela
        // A posição playableWidth - 0, playableHeight - 2 coloca o portal no limite direito da tela
        // Se preferir posicionar o portal um pouco para dentro, use por exemplo: playableWidth - 2, playableHeight - 2   
        Vector2 portalWorldPosition = new Vector2(playableWidth - 0, playableHeight - 2);

        // Instancia o portal na posição calculada
        // O portal é posicionado como filho deste objeto gerador para organização na Hierarchy
        //GameObject portalInstance = Instantiate(portalPrefab, portalWorldPosition, Quaternion.identity, transform);


        // Calcula a posição do spawn do jogador
        // O jogador será posicionado 1 unidade à direita da parede visual esquerda
        int playerSpawnX = -WallOffset + 1; // Ajusta a posição do spawn do jogador com base no limite visual esquerdo
        int playerSpawnY = 1; // Define a altura inicial do jogador, 1 unidade acima do chão

        // Instancia um "área de proteção" no chão (3 tiles) sob a posição de spawn para garantir que o jogador não caia
        // Mesmo que o jogador esteja fora da área jogável, o chão será gerado para evitar quedas
        for (int padX = playerSpawnX - 1; padX <= playerSpawnX + 1; padX++)
        {

            //Gera a instancia do chão na posição indicada
            //Instantiate(floorPrefab, new Vector2(padX, 0), Quaternion.identity, transform);
        }

        //Gera a instancia do player na posição calculada para o spawn (playerspawnX e playerSpawnY)
        Instantiate(playerPrefab, new Vector2(playerSpawnX, playerSpawnY), Quaternion.identity, transform);

    }

}