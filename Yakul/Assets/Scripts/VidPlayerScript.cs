using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidPlayerScript : MonoBehaviour
{
    VideoPlayer player;
    public void Awake()
    {
        player = this.GetComponent<VideoPlayer>();
        if(this.gameObject.name == "IntroVid")
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.mp4");
            player.url = filePath;
        }
        else if(this.gameObject.name == "OutroVid")
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "outro.mp4");
            player.url = filePath;
        }
    }
    // Start is called before the first frame update
    //public void IntroVid()
    //{
    //    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.mp4");
    //    player.url = filePath;

    //    //player.renderMode = VideoRenderMode.CameraNearPlane;
    //    //player.targetCameraAlpha = 1.0f;
    //    //player.Play();
    //}
    //public void OutroVid()
    //{
    //    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "outro.mp4");
    //    player.url = filePath;
    //}
}
