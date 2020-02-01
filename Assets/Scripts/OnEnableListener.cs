using UnityEngine;

[ExecuteInEditMode]
public class OnEnableListener : MonoBehaviour
{
    private OnEnableCallback callback;

    private void OnEnable() {
        if (callback != null) {
            callback(this.gameObject);
        }
    }

    public void SetCallback(OnEnableCallback cb) {
        callback = cb;
    }
}