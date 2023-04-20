using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColliderVisualiser))]
public class ColliderVisualiserEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ColliderVisualiser vis = (ColliderVisualiser)target;

        if (vis.sizeVisualiser != null)
        {
            BoxCollider2D boxCollider = vis.GetComponent<BoxCollider2D>();
            if (boxCollider != null) {
                vis.transform.localPosition = boxCollider.offset;
                vis.transform.localScale = boxCollider.size;
            }
        }

		if (!vis.isLevel || vis.rootTransform == null) return;

		Transform p = GameObject.FindWithTag("Level").transform;

		if(p == null) return;
        if(vis.rootTransform.parent == p) return;
        if(p.gameObject.scene != vis.gameObject.scene) return;

        vis.rootTransform.SetParent(p, true);
	}
}