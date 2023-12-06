using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Called when click "Play" button.
    public void OnPlayButton ()
    {
        SceneManager.LoadScene(1);
    }
}