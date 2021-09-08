using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator transAnim;
    private void Start()
    {
        transAnim = GetComponent<Animator>();
    }

    public void loadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        transAnim.SetTrigger("End");
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene(sceneName);
    }
}
