using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Techxenia.ProjectBrowserTitleOverwriter
{
    /// <summary>
    /// ProjectBrowser�̃^�C�g���ɊJ���Ă���t�H���_�[�̃p�X��\������g��
    /// </summary>
    [InitializeOnLoad]
    internal static class ProjectBrowserTitleOverwriter
    {
        private static Type _projectBrowserType;
        private static MethodInfo _getActiveFolderPath;

        static ProjectBrowserTitleOverwriter()
        {
            _projectBrowserType = Assembly.Load("UnityEditor").GetType("UnityEditor.ProjectBrowser");
            _getActiveFolderPath = _projectBrowserType.GetMethod("GetActiveFolderPath", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            EditorApplication.projectWindowItemOnGUI += (guid, selectionRect) =>
            {
                foreach (EditorWindow find in GetProjectBrowserWindows())
                {
                    OverwriteBrowserTitle(find);
                }
            };
        }

        /// <summary>
        /// �J���Ă���ProjectBrowserWindow�����ׂĎ擾����
        /// </summary>
        /// <returns></returns>
        private static EditorWindow[] GetProjectBrowserWindows()
        {
            return Resources.FindObjectsOfTypeAll(_projectBrowserType) as EditorWindow[];
        }

        /// <summary>
        /// ProjectBrowserWindow�̃^�C�g�����㏑������
        /// </summary>
        /// <param name="window"></param>
        private static void OverwriteBrowserTitle(EditorWindow window)
        {
            var key = ProjectBrowserTitleOverwriterEditorMenu.ProjectBrowserTitleOverwriterToggle.SaveKey;
            if (!EditorPrefs.GetBool(key, false))
            {
                window.titleContent = EditorGUIUtility.TrTextContent("Project");
            }
            else
            {
                string path = (string)_getActiveFolderPath.Invoke(window, null);
                window.titleContent = EditorGUIUtility.TrTextContent(path);
            }
        }
    }
}