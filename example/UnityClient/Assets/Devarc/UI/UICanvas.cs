using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Devarc
{
    public abstract class UICanvas : PrefabSingleton<UICanvas>
    {
        public abstract void Clear();
        public abstract void onInit();

        public Canvas canvas => mCanvas;
        Canvas mCanvas;

        bool mInitialized = false;
        List<UIFrame> mFrames = new List<UIFrame>();

        public void Init()
        {
            if (mInitialized) return;
            mInitialized = true;

            var list = GetComponentsInChildren<UIFrame>();
            foreach (var frame in list)
            {
                mFrames.Add(frame);
                frame.Init();
            }
            onInit();
        }

        public UIFrame CreateFrame<T>(string asset_name)
        {
            var prefab = AssetManager.Instance.GetAsset<GameObject>(asset_name);
            return CreateFrame<T>(prefab);
        }

        public UIFrame CreateFrame<T>(GameObject prefab)
        {
            UIFrame prefabFrame = prefab?.GetComponent<UIFrame>();
            if (prefabFrame == null) return null;

            var prefabTr = prefab.GetComponent<RectTransform>();
            if (prefabTr == null) return null;

            var obj = Instantiate(prefab, transform);
            var frame = obj.GetComponent<UIFrame>();
            var rectTransform = frame.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = prefabTr.anchoredPosition;
            rectTransform.localScale = Vector3.one;

            if (mFrames.Contains(frame) == false)
            {
                mFrames.Add(frame);
            }
            if (mInitialized)
            {
                frame.Init();
            }
            return frame;
        }

        public void RemoveFrame(UIFrame frame)
        {
            frame.Clear();
            GameObject.Destroy(frame.gameObject);
        }


        private void Awake()
        {
            mCanvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            if (UIManager.IsCreated())
            {
                switch (mCanvas.renderMode)
                {
                    case RenderMode.ScreenSpaceCamera:
                        mCanvas.worldCamera = UIManager.Instance.uiCamera;
                        break;
                    default:
                        break;
                }
                UIManager.Instance.RegisterCanvas(this);
            }
        }

        private void OnDestroy()
        {
            if (UIManager.IsCreated())
            {
                UIManager.Instance.Remove(this);
            }
        }
    }
}