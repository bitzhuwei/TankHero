using UnityEngine;
using System.Collections;

public class SelectMap : MonoBehaviour
{
    private string mapName;
    private SelectMapManager selectMapManagerScript;
    private UnityEngine.UI.Image image;
    private bool lastplayable;
    private GameObject flashChild;
    private bool lastFlash;
    private string episodeName;
    private GameObject[] episodeButtons;

    void Awake()
    {
        this.mapName = this.name;

        this.image = this.GetComponent<UnityEngine.UI.Image>();
        this.flashChild = this.transform.FindChild("Image").gameObject;
        this.lastplayable = true;
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
        var playable = this.selectMapManagerScript.IsPlayable(this.mapName);
        if (playable)
        {
            if (!lastplayable)
            {
                this.image.color = Color.white;
                lastplayable = true;
            }
        }
        else
        {
            if (lastplayable)
            {
                this.image.color = Color.gray;
                lastplayable = false;
            }
        }

        if (this.mapName == this.selectMapManagerScript.selectedMap)
        {
            if (!lastFlash)
            {
                this.flashChild.SetActive(true);
                lastFlash = true;
            }
        }
        else
        {
            if (lastFlash)
            {
                this.flashChild.SetActive(false);
                lastFlash = false;
            }
        }

        var selectedEpisode = this.selectMapManagerScript.selectedEpisode;
        if (this.episodeName != selectedEpisode)
        {
            this.episodeButtons = GameObject.FindGameObjectsWithTag(Tags.EpisodeButton);
            foreach (var item in episodeButtons)
            {
                var script = item.GetComponent<SelectEpisode>();
                if (script.episodeName == selectedEpisode)
                {
                    var index = int.Parse(this.mapName.Substring("map".Length)) - 1;
                    this.image.sprite = script.maps[index];
                    break;
                }
            }
            this.episodeName = selectedEpisode;
        }

    }

    public void SelectMap_Click()
    {
        this.selectMapManagerScript.selectedMap = this.mapName;
    }
}
