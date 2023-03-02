using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCanvas : MonoBehaviour
{
    public void Restart() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
}