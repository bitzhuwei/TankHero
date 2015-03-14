using UnityEngine;
using System.Collections;

public class TestWallInfo : MonoBehaviour {
    public GameObject currentDetonator;
    private int _currentExpIdx = -1;
    public GameObject[] detonatorPrefabs;
    public float explosionLife = 10;
    public float timeScale = 1;
    public float detailLevel = 1;
    public GameObject wall;
    GameObject _currentWall;
    float _spawnWallTime = -1000;
    Rect _guiRect;

    // Use this for initialization
    void Start()
    {
    }


    public void NextEpisode()
    {
        if (_currentExpIdx >= detonatorPrefabs.Length - 1) _currentExpIdx = 0;
        else _currentExpIdx++;
        currentDetonator = detonatorPrefabs[_currentExpIdx];
    }

    public void SpawnWall()
    {
        if (_currentWall) Destroy(_currentWall);
        _currentWall = Instantiate(wall, new Vector3(-7, -12, 48), Quaternion.identity) as GameObject;
        _spawnWallTime = Time.time;
    }

    Rect checkRect = new Rect(0, 0, 224, 157);

    // Update is called once per frame
    void Update()
    {
        //keeps the play button from making an explosion
        if ((Time.time + _spawnWallTime) > 0.5f)
        {
            //don't spawn an explosion if we're in the UI's rect
            if (!checkRect.Contains(Input.mousePosition))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SpawnExplosion();
                }
            }
            Time.timeScale = timeScale;
        }

    }

    private void SpawnExplosion()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            var offsetSize = currentDetonator.GetComponent<Detonator>().size / 3;
            var hitPoint = hit.point + ((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize, offsetSize))));
            var exp = Instantiate(currentDetonator, hitPoint, Quaternion.identity) as GameObject;
            exp.GetComponent<Detonator>().detail = detailLevel;
            Destroy(exp, explosionLife);
        }
    }
}
