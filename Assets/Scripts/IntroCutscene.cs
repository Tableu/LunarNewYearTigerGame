using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public Image img;
    public Image background;
    public List<Sprite> Images;
    public GameObject Player;
    public EnemyManager EnemyManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeImage(true, delegate
        {
            img.sprite = Images[0];
            StartCoroutine(FadeImage(false, delegate
            {
                StartCoroutine(FadeImage(true, delegate
                {
                    img.sprite = Images[1];
                    StartCoroutine(FadeImage(false, delegate
                    {
                        StartCoroutine(FadeImage(true, delegate
                        {
                            img.sprite = Images[2];
                            StartCoroutine(FadeImage(false, delegate
                            {
                                StartCoroutine(FadeImage(true, delegate
                                {
                                    img.sprite = Images[3];
                                    StartCoroutine(FadeImage(false, delegate
                                    {
                                        StartCoroutine(FadeImage(true, delegate
                                        {
                                            Player.SetActive(true);
                                            background.gameObject.SetActive(false);
                                            gameObject.SetActive(false);
                                            foreach (GameObject enemy in EnemyManager.Enemies)
                                            {
                                                if (enemy != null)
                                                {
                                                    enemy.SetActive(true);
                                                }
                                            }
                                        }));
                                    }));
                                }));
                            }));
                        }));
                    }));
                }));
            }));
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeImage(bool fadeAway, System.Action callBack)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= 0.5f*Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            img.color = new Color(1, 1, 1, 0);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += 0.5f*Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            img.color = new Color(1, 1, 1, 1);
        }

        if (callBack != null)
        {
            callBack();
        }
    }
}
