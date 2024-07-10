using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoBackground : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            rawImage.texture = videoPlayer.texture;
        }
    }

    void EndReached(VideoPlayer vp)
    {
        vp.frame = 0;
        vp.Play();
    }
}
