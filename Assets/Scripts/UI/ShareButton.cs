using System.IO;
using System.Collections;
using UnityEngine;

public class ShareButton : MonoBehaviour
{
    public void ClickShare()
    {
        StartCoroutine(TakeSSAndShare());
    }

    public void ClickHiddenShortcardShare(PlayerShortcard sc)
    {
        StartCoroutine(TakeHiddenSSAndShare(sc));
    }
    
    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();
        
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0,0,Screen.width, Screen.height),0,0);
        ss.Apply();
        string filePath = Path.Combine(Application.temporaryCachePath, "sharedImg.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        
        // to avoid memory loss
        Destroy(ss);
        new NativeShare().AddFile(filePath).SetSubject("").SetText("").Share();
    }
    
    private IEnumerator TakeHiddenSSAndShare(PlayerShortcard sc)
    {
        UIManager.Instance.OpenPlayerShortcard(sc);
        yield return new WaitForEndOfFrame();
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0,0,Screen.width, Screen.height),0,0);
        ss.Apply();
        string filePath = Path.Combine(Application.temporaryCachePath, "sharedImg.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        
        // to avoid memory loss
        Destroy(ss);
        new NativeShare().AddFile(filePath).SetSubject("").SetText("").Share();
        UIManager.Instance.ClosePlayerShortcard();
    }

}
