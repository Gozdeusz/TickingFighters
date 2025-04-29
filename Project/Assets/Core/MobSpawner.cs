using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private GameObject mob;
    private Vector2 newPos;

    public static bool isReady = false;

    private void Start()
    {
        spawnMobs();
      
    }


    public void spawnMobs() {
        isReady = false;


        for (int i = 0; i < spawnCount; i++)
        {
            float newPosX = Random.Range(-5, 5);
            float newPosY = Random.Range(-5, 5);
            newPos.x = spawnPoint.transform.position.x + newPosX;
            newPos.y = spawnPoint.transform.position.y + newPosY;

            GameObject enemyClone = Instantiate(mob, new Vector2(newPos.x, newPos.y), Quaternion.identity);
           
            

        }

        isReady = true;

    }
}
