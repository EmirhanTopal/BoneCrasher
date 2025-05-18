using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void NextSceneHandler()
    {
        StartCoroutine(WaitNextScene());
    }
    
    private IEnumerator WaitNextScene()
    {
        _animator.SetTrigger("nextScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
}
