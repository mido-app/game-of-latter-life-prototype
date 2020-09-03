using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTopSceneButton : MonoBehaviour
{

    public void OnClick()
    {
        SceneManager.LoadScene("TopScene");
    }

}
