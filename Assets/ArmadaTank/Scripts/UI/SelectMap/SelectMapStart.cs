using UnityEngine;
using System.Collections;

public class SelectMapStart : MonoBehaviour
{

    private SelectMapManager selectMapManagerScript;
    private bool lastPlayable;
    private UnityEngine.UI.Text text;


    // Use this for initialization
    void Start()
    {
        var manager = GameObject.FindGameObjectWithTag(Tags.SelectMapManager);
        this.selectMapManagerScript = manager.GetComponent<SelectMapManager>();
        this.text = this.GetComponentInChildren<UnityEngine.UI.Text>();
        this.lastPlayable = true;
        string path = "";
        var builder = new System.Text.StringBuilder();
        builder.Append(Application.platform);
        builder.Append(": ");
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = Application.dataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Application.dataPath;
        }
        builder.Append(path);
        //this.text.text = builder.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        var playable = this.selectMapManagerScript.IsPlayable(this.selectMapManagerScript.selectedMap);
        if (playable)
        {
            if (!lastPlayable)
            {
                this.text.text = "Start";
                lastPlayable = true;
            }
        }
        else
        {
            this.text.text = string.Format("the {0} {1} is still locked.",
                selectMapManagerScript.selectedEpisode,
                selectMapManagerScript.selectedMap);
            lastPlayable = false;
        }
    }

    public void SelectMapStart_Click()
    {
        //if (this.lastPlayable)
        {
            Application.LoadLevel(Scenes.Workshop);
            //Application.LoadLevel(Scenes.BattleField);
        }
    }
}
