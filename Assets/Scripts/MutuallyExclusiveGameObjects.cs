using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MutuallyExclusiveGameObjects : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gameObjects;

    private OnEnableCallback callback;
    public void Callback (GameObject gameObjectToEnable) {
        EnableOnlyOneGameobject(gameObjectToEnable);
    }

    private void Start () {
        callback = Callback;
        foreach(GameObject gameObject in gameObjects) {
            OnEnableListener listener = gameObject.AddComponent(typeof(OnEnableListener)) as OnEnableListener;
            listener.SetCallback(callback);
        }
    }

    public void EnableOnlyOneGameobject(GameObject gameObjectToEnable) {
        foreach(GameObject gameObject in gameObjects) {
            gameObject.SetActive(gameObject == gameObjectToEnable);
        }
    }
}

public delegate void OnEnableCallback(GameObject gameObject);
