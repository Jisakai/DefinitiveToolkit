using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class CreateMarkdownUtility
{
    [MenuItem("Assets/Create/Markdown", priority = 80)]
    private static void CreateMarkdown()
    {
        var icon = EditorGUIUtility.IconContent("TextAsset Icon").image as Texture2D;

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<CreateMarkdownAction>(),
            "New Markdown.md",
            icon,
            null
        );
    }

    private class CreateMarkdownAction : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            if (!pathName.EndsWith(".md"))
                pathName += ".md";

            File.WriteAllText(pathName, "# New Document\n");

            AssetDatabase.ImportAsset(pathName);
            AssetDatabase.Refresh();

            Object asset = AssetDatabase.LoadAssetAtPath<TextAsset>(pathName);
            ProjectWindowUtil.ShowCreatedAsset(asset);
        }
    }
}