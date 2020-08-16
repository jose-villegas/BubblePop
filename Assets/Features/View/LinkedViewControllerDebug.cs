using UnityEngine;

public partial class LinkedViewController : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        var configuration = Contexts.sharedInstance.configuration.gameConfiguration.value;

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, configuration.CircleCastRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, configuration.OverlapCircleRadius);
    }
#endif
}