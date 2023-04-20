using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerEvent))]
public class TriggerEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TriggerEvent t = target as TriggerEvent;

        t.needsPlayer = EditorGUILayout.BeginToggleGroup("Player Trigger", t.needsPlayer);

        t.needsGrounded = EditorGUILayout.Toggle("Must Be On Ground", t.needsGrounded);
        t.needsAlive = EditorGUILayout.Toggle("Must Be Alive", t.needsAlive);

        t.killOnTrigger = EditorGUILayout.Toggle("Kill Player", t.killOnTrigger);
        t.hidePlayerOnKill = !t.killOnTrigger || EditorGUILayout.Toggle("Hide Player On Kill", t.hidePlayerOnKill);

        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.Space();

        using (var _ = new EditorGUILayout.HorizontalScope()) {
            t.maxTriggers = EditorGUILayout.Toggle("Limit Triggers", t.maxTriggers > 0, GUILayout.ExpandWidth(false)) ? System.Math.Max(1, EditorGUILayout.IntField(t.maxTriggers, GUILayout.Width(50))) : 0;
        }
    }
}