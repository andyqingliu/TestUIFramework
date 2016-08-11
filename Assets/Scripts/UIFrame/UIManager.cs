using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***
 * UIManager.cs
 *
 * @author LiuQing
 */
namespace Xsjm
{
    public class UIManager
    {
        public delegate void UIAction(UIBase ui);
        public UIAction OnShow;
        public UIAction OnHide;
        public UIAction OnDestroy;

        private static UIManager mInstance;
        public static UIManager Instance
        {
            get
            {
                if (null == mInstance)
                {
                    mInstance = new UIManager();
                }
                return mInstance;
            }
        }

        private GameObject uiRoot;
        public GameObject UIRoot
        {
            get
            {
                if (null == uiRoot)
                {
                    uiRoot = GameObject.Find("UI Root");
                }
                return uiRoot;
            }
        }

        //所有显示过的UI（不包含销毁的UI）
        private Dictionary<string, UIBase> allUIs;

        //当前显示的UI
        private Dictionary<string, UIBase> shownUIs;

        //当前显示的从小到大排序的UI
        private List<UIBase> shownSortUIs;

        private int curMinUIDepth = 0;
        private int curMaxUIDepth = 0;
        private int deltaDepth = 10;

        public UIManager()
        {
            allUIs = new Dictionary<string, UIBase>();
            shownUIs = new Dictionary<string, UIBase>();
            shownSortUIs = new List<UIBase>();
        }

        public UIBase GetUI(string uiName)
        {
            if (allUIs.ContainsKey(uiName))
            {
                return allUIs[uiName];
            }
            return null;
        }

        public T GetUI<T>() where T : UIBase
        {
            string needName = GetName<T>();
            UIBase ui = GetUI(needName);
            if (null != ui)
            {
                return (T)ui;
            }
            return (T)null;
        }

        public void Show<T>(object data = null) where T : UIBase
        {
            string uiName = GetName<T>();
            if (allUIs.ContainsKey(uiName))
            {
                if (!shownUIs.ContainsKey(uiName))
                {
                    UIBase ui = allUIs[uiName];
                    OpenUI(ui, data);

                    SortUIDepth(ui);

                    shownUIs.Add(uiName, ui);
                    shownSortUIs.Add(ui);
                }
                return;
            }
            GameObject prefab = Resources.Load<GameObject>(string.Format("{0}/{1}", UIConst.UI_PREFAB_PATH, uiName));
            if (null != prefab)
            {
                GameObject obj = NGUITools.AddChild(UIRoot, prefab);
                obj.name = prefab.name;
                UIBase uiBase = obj.GetComponent<T>();
                OpenUI(uiBase, data);

                SortUIDepth(uiBase);

                allUIs.Add(uiName, uiBase);
                shownUIs.Add(uiName, uiBase);
                shownSortUIs.Add(uiBase);
            }
        }

        public void Hide<T>() where T : UIBase
        {
            string uiName = GetName<T>();
            if (shownUIs.ContainsKey(uiName))
            {
                UIBase uiBase = shownUIs[uiName];
                CloseUI(uiBase);

                shownUIs.Remove(uiName);
                shownSortUIs.Remove(uiBase);

                ResetUIDepth();
            }
        }

        public void Destroy<T>() where T : UIBase
        {
            string uiName = GetName<T>();
            if (shownUIs.ContainsKey(uiName))
            {
                UIBase uiBase = shownUIs[uiName];
                DestroyUI(uiBase);

                shownUIs.Remove(uiName);
                shownSortUIs.Remove(uiBase);
                allUIs.Remove(uiName);

                ResetUIDepth();
            }
        }

        #region Private Methods

        private string GetName<T>() where T : UIBase
        {
            string fullName = typeof(T).FullName;
            int idx = fullName.IndexOf(".");
            return (idx != -1) ? fullName.Substring(idx + 1) : fullName;
        }

        private void SortUIDepth(UIBase ui)
        {
            int minDepth = GetUIMinDepth(ui);
            int maxDepth = GetUIMaxDepth(ui);
            if (minDepth < 0 || maxDepth < 0)
            {
                Debug.LogError(string.Format("The UI min or max depth is <0,please modify the depth!"));
            }
            if (minDepth <= curMaxUIDepth)
            {
                int delta = curMaxUIDepth - minDepth  + deltaDepth;
                UIPanel[] panels = ui.GetComponentsInChildren<UIPanel>();
                int[] depths = new int[panels.Length];
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].depth += delta;
                    depths[i] = panels[i].depth;
                }
                curMinUIDepth = Mathf.Min(depths);
                curMaxUIDepth = Mathf.Max(depths);
            }
            else
            {
                curMinUIDepth = minDepth;
                curMaxUIDepth = maxDepth;
            }
        }

        private void ResetUIDepth()
        {
            if (shownSortUIs.Count == 0)
            {
                curMaxUIDepth = curMinUIDepth = 0;
            }
            else
            {
                int sortUICount = shownSortUIs.Count;
                UIBase ui = shownSortUIs[sortUICount - 1];
                curMinUIDepth = GetUIMinDepth(ui);
                curMaxUIDepth = GetUIMaxDepth(ui);
            }
        }

        private int GetUIMinDepth(UIBase ui)
        {
            int[] depths = GetUIDepths(ui);

            return Mathf.Min(depths);
        }

        private int GetUIMaxDepth(UIBase ui)
        {
            int[] depths = GetUIDepths(ui);

            return Mathf.Max(depths);
        }

        private int[] GetUIDepths(UIBase ui)
        {
            if (null == ui)
            {
                Debug.LogError(string.Format("Target UI is NULL!"));
            }
            UIPanel[] panels = ui.GetComponentsInChildren<UIPanel>();
            int[] depths = new int[panels.Length];
            for (int i = 0; i < panels.Length; i++)
            {
                depths[i] = panels[i].depth;
            }
            return depths;
        }

        private void OpenUI(UIBase ui,object data = null)
        {
            if (null == ui)
            {
                return;
            }

            if (null != data)
            {
                ui.SetData(data);
            }

            ui.Open();

            if (null != OnShow)
            {
                OnShow(ui);
            }
        }

        private void CloseUI(UIBase ui)
        {
            if (null == ui)
            {
                return;
            }

            ui.Close();

            if (null != OnHide)
            {
                OnHide(ui);
            }
        }

        private void DestroyUI(UIBase ui)
        {
            if (null == ui)
            {
                return;
            }

            ui.Destroy();

            if (null != OnDestroy)
            {
                OnDestroy(ui);
            }
        }

        #endregion
    }
}
