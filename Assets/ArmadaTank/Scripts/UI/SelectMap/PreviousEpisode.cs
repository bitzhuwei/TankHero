using UnityEngine;
using System.Collections;

public class PreviousEpisode : MonoBehaviour {
    private SelectMapManager selectMapManagerScript;
    private SelectEpisode[] selectEpisodeScripts;


    // Use this for initialization
    void Start()
    {
        var manager = GameObject.FindGameObjectWithTag(Tags.SelectMapManager);
        this.selectMapManagerScript = manager.GetComponent<SelectMapManager>();
        var buttons = GameObject.FindGameObjectsWithTag(Tags.EpisodeButton);
        selectEpisodeScripts = new SelectEpisode[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            selectEpisodeScripts[i] = buttons[i].GetComponent<SelectEpisode>();
        }
        var names = new int[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            names[i] = int.Parse(selectEpisodeScripts[i].episodeName.Substring("episode".Length));
        }
        for (int i = 0; i < names.Length; i++)
        {
            var p = i;
            for (int j = i + 1; j < names.Length; j++)
            {
                if (names[p] > names[j])
                { p = j; }
            }
            if (p != i)
            {
                var tmp = names[i];
                names[i] = names[p];
                names[p] = tmp;
                var tmp2 = selectEpisodeScripts[i];
                selectEpisodeScripts[i] = selectEpisodeScripts[p];
                selectEpisodeScripts[p] = tmp2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PreviousEpisode_Click()
    {
        var current = 0;
        var episode = this.selectMapManagerScript.selectedEpisode;
        for (int i = 0; i < this.selectEpisodeScripts.Length; i++)
        {
            if (selectEpisodeScripts[i].episodeName == episode)
            {
                current = i + 1;
                break;
            }
        }
        current--;
        if (current < 1)
        { current = selectEpisodeScripts.Length; }
        selectEpisodeScripts[current - 1].SelectEpisode_Click();
    }
}
