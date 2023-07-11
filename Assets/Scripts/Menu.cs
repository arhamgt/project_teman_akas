using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator anim;
    public void ChangeScene()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        anim.Play("cutscene_fadeout");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
