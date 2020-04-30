using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DeathVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    private void Start() 
    {
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
}
