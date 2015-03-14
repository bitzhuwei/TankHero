using UnityEngine;
using System.Collections;

public class AudioClipManager : MonoBehaviour {

    public System.Collections.Generic.List<AudioClip> audioClips;
    public readonly System.Collections.Generic.Dictionary<string, AudioClip> AudioClipDict = new System.Collections.Generic.Dictionary<string, AudioClip>();

    void Awake()
    {
        if (AudioClipDict.Count > 0) { return; }

        foreach (var item in audioClips)
        {
            AudioClipDict.Add(item.name, item);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
