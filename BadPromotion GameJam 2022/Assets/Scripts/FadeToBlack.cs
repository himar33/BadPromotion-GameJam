using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("FadingIn");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }

}
