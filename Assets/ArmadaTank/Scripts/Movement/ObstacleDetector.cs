using UnityEngine;
using System.Collections;

public abstract class ObstacleDetector : MonoBehaviour {

    public System.Collections.Generic.List<GameObject> obstacles;

    void Awake()
    {
        if (obstacles == null)
        {
            obstacles = new System.Collections.Generic.List<GameObject>();
        }
    }
	// Use this for initialization
	void Start () {
        this.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
#if DEBUG
        this.renderer.enabled = !this.IsUnblocked();
#endif
	}

    protected abstract void OnTriggerEnter(Collider other);

    void OnTriggerExit(Collider other)
    {
        this.obstacles.Remove(other.gameObject);
    }

    public bool IsUnblocked()
    {
        for (int i = 0; i < this.obstacles.Count; i++)
        {
            if(this.obstacles[i] != null && this.obstacles[i].collider.enabled)
            {
                return false;
            }
        }
        return true;
    }
}
