using UnityEditor;

namespace Techxenia.ProjectBrowserTitleOverwriter
{
    public class ProjectBrowserTitleOverwriterEditorMenu
    {
        public const string MenuRootPath = "Techxenia/View/ProjectBrowserTitleOverwriter/";

        public class ProjectBrowserTitleOverwriterToggle
        {
            public const string MenuPath = MenuRootPath + "Enable";
            public const string SaveKey = "Techxenia_ProjectBrowserTitleOverwriter_Enable";

            [MenuItem(MenuPath)]
            private static void ToggleMenu()
            {
                EditorPrefs.SetBool(SaveKey, !EditorPrefs.GetBool(SaveKey, true));
            }

            [MenuItem(MenuPath, true)]
            private static bool MenuToggleValidate()
            {
                Menu.SetChecked(MenuPath, EditorPrefs.GetBool(SaveKey, true));
                return true;
            }
        }
    }
}