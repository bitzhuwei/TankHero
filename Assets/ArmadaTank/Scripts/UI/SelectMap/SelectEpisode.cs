using UnityEngine;
using System.Collections;

public class SelectEpisode : MonoBehaviour
{
    public string episodeName { get; private set; }
    private SelectMapManager selectMapManagerScript;
    private UnityEngine.UI.Image image;
    public Sprite passedSprite;
    public Sprite unpassedSprite;
    private bool lastPassed;
    private System.Collections.Generic.List<FlashRotate> flashChildren;
    private bool lastFlash;

    public System.Collections.Generic.List<Sprite> maps;

    void Awake()
    {
        this.episodeName = this.name;

        this.image = this.GetComponent<UnityEngine.UI.Image>();

        flashChildren = new System.Collections.Generic.List<FlashRotate>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.transform.GetChild(i);
            var component = child.GetComponent<FlashRotate>();
            if(component!=null)
            { flashChildren.Add(component); }
        }
        this.lastPassed = false;
        this.lastFlash = false;
    }
    // Use this for initialization
    void Start()
    {
        var manager = GameObject.FindGameObjectWithTag(Tags.SelectMapManager);
        this.selectMapManagerScript = manager.GetComponent<SelectMapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var passed = this.selectMapManagerScript.IsPassed(this.episodeName);
        if (passed)
        {
            if (!lastPassed)
            {
                this.image.sprite = passedSprite;
                lastPassed = true;
            }
        }
        else
        {
            if (lastPassed)
            {
                this.image.sprite = unpassedSprite;
                lastPassed = false;
            }
        }

        if (this.episodeName == this.selectMapManagerScript.selectedEpisode)
        {
            if (!lastFlash)
            {
                foreach (var item in flashChildren)
                {
                    item.gameObject.SetActive(true);
                }
                lastFlash = true;
            }
        }
        else
        {
            if (lastFlash)
            {
                foreach (var item in flashChildren)
                {
                    item.gameObject.SetActive(false);
                }
                lastFlash = false;
            }
        }
    }

    public void SelectEpisode_Click()
    {
        this.selectMapManagerScript.selectedEpisode = this.episodeName;
    }
}
