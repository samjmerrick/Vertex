using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text Rank;
    public Image ProfileImage;
    public Text DisplayName;
    public Text Score;

    public string UserID;

    public void Init(int rank, string imageUrl, string displayName, string score)
    {
        Rank.text = rank + ".";
        Score.text = score.ToString();
        DisplayName.text = displayName;

        if (imageUrl != null)
            StartCoroutine(DisplayImage(imageUrl));
    }

    IEnumerator DisplayImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        ProfileImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}