using UnityEditor;
using UnityEditor.XR.Interaction.Toolkit;

public class SocketWithTagEditor : XRSocketInteractorEditor
{
    private SerializedProperty targetTag = null;

    [CustomEditor(typeof(SocketWithTagCheck))]
    protected override void OnEnable()
    {
        base.OnEnable();
        targetTag = serializedObject.FindProperty("targetTag");    
    }

    protected override void DrawProperties()
    {
        base.DrawProperties();
        EditorGUILayout.PropertyField(targetTag);
    }
}
