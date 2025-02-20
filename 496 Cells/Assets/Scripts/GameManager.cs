using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int josephWidth;
    [SerializeField] public int josephHeight;
    [SerializeField] public float josephSize = 0.5f;
    [SerializeField] public GameObject josephPrefab;

    private Cell[,] joseph;
    private float updateInterval = 2.5f;
    private float timer;

    void Start()
    {
        InitializeJoseph();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            UpdateJoseph();
            timer = 0;
        }
    }

    void InitializeJoseph()
    {
        joseph = new Cell[josephWidth, josephHeight];

        for(int x = 0; x < josephWidth; x++)
        {
            for(int y = 0; y < josephHeight; y++)
            {
                GameObject joeyObj = Instantiate(josephPrefab, new Vector3(x*josephSize,y*josephSize,0), Quaternion.identity);
                joeyObj.transform.parent = transform;
                joseph[x,y] = joeyObj.GetComponent<Cell>();
                joseph[x,y].isAlive = Random.value > 0.5f; //Randomly set alive/dead state
                joseph[x,y].UpdateColor();
            }
        }
    }

    void UpdateJoseph()
    {
        bool[,] nextState = new bool[josephWidth, josephHeight];

        for(int x = 0; x < josephWidth; x++)
        {
            for (int y = 0; y < josephHeight; y++)
            {
                int aliveJoeys = CountAliveJoeys(x,y);
                bool isAlive = joseph[x,y].isAlive;

                //Game Life rules
                if(isAlive && (aliveJoeys < 2 || aliveJoeys >3))
                    nextState[x,y] = false;
                    else if(!isAlive && aliveJoeys == 3)
                        nextState[x,y] = true;
                else
                    nextState[x,y] = isAlive;
            }
        }
            for (int x = 0; x < josephWidth; x++)
            {
                for( int y = 0; y < josephHeight; y++)
                {
                    joseph[x,y].isAlive = nextState[x,y];
                    joseph[x,y].UpdateColor();
                }
            }
    }
    int CountAliveJoeys(int x, int y)
    {
        int count = 0;
        int[] dx = {-1,0,1,-1,1,-1,0,1};
        int[] dy = {-1,-1,-1,0,0,1,1,1};

        for( int i = 0; i < 8; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
        
        if (nx > 0 && nx < josephWidth && ny >= 0 && ny < josephHeight && joseph[nx , ny].isAlive)
        {
            count++;
        }
    }
    return count;
    }
}
