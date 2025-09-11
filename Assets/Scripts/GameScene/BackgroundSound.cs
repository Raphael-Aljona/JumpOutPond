using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
