using UnityEngine;
using UnityEditor;

namespace YG.EditorScr
{
    [InitializeOnLoad]
    public static class YGEditorStyles
    {
        private static GUIStyle _selectable;
        private static GUIStyle _box;
        private static GUIStyle _boxLight;
        private static GUIStyle _error;
        private static GUIStyle _button;
        private static GUIStyle _warning;

        public static GUIStyle selectable
        {
            get
            {
                if (_selectable == null)
                    Selectable();
                return _selectable;
            }
        }

        public static GUIStyle box
        {
            get
            {
                if (_box == null)
                    Box();
                return _box;
            }
        }

        public static GUIStyle boxLight
        {
            get
            {
                if (_boxLight == null)
                    BoxLight();
                return _boxLight;
            }
        }

        public static GUIStyle error
        {
            get
            {
                if (_error == null)
                    Error();
                return _error;
            }
        }

        public static GUIStyle warning
        {
            get
            {
                if (_warning == null)
                    Warning();
                return _warning;
            }
        }

        public static GUIStyle button
        {
            get
            {
                if (_button == null)
                    Button();
                return _button;
            }
        }

        static YGEditorStyles()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorApplication.update += ReinitializeStyles;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            ReinitializeStyles();
        }

        private static void ReinitializeStyles()
        {
            _selectable = null;
            _box = null;
            _boxLight = null;
            _error = null;
            _button = null;
            _warning = null;
        }

        private static void Selectable()
        {
            _selectable = new GUIStyle(EditorStyles.helpBox);

            Color normalColor = new Color(1f, 1f, 1f, 0.07f);
            Color hoverColor = new Color(1f, 0.5f, 0f, 0.3f);

            _selectable.normal.background = MakeTexUnderlineLeft(normalColor);
            _selectable.hover.background = MakeTexUnderlineLeft(hoverColor);
            _selectable.active.background = MakeTexUnderlineLeft(hoverColor);
            _selectable.focused.background = MakeTexUnderlineLeft(hoverColor);
        }

        private static void Box()
        {
            Color color;

            if (EditorGUIUtility.isProSkin)
            {
                _box = new GUIStyle(EditorStyles.helpBox);
                color = new Color(0f, 0f, 0f, 0.2f);
            }
            else
            {
                _box = new GUIStyle();
                color = new Color(1f, 1f, 1f, 0.5f);
            }

            _box.normal.background = MakeTex(color);
            _box.hover.background = MakeTex(color);
            _box.active.background = MakeTex(color);
            _box.focused.background = MakeTex(color);
        }

        private static void BoxLight()
        {
            if (EditorGUIUtility.isProSkin)
            {
                _boxLight = new GUIStyle(EditorStyles.helpBox);

                Color color = new Color(1f, 1f, 1f, 0.05f);

                _boxLight.normal.background = MakeTex(color);
                _boxLight.hover.background = MakeTex(color);
                _boxLight.active.background = MakeTex(color);
                _boxLight.focused.background = MakeTex(color);

                _boxLight.border = new RectOffset(23, 23, 23, 23);
            }
            else
            {
                _boxLight = new GUIStyle(EditorStyles.helpBox);
            }
        }

        private static void Error()
        {
            _error = new GUIStyle(EditorStyles.helpBox);

            Color color = new Color(1f, 0f, 0f, 0.18f);

            _error.normal.background = MakeTex(color);
            _error.hover.background = MakeTex(color);
            _error.active.background = MakeTex(color);
            _error.focused.background = MakeTex(color);
        }

        private static void Warning()
        {
            _warning = new GUIStyle(EditorStyles.helpBox);

            Color color = new Color(1f, 0.6f, 0f, 0.25f);

            _warning.normal.background = MakeTex(color);
            _warning.hover.background = MakeTex(color);
            _warning.active.background = MakeTex(color);
            _warning.focused.background = MakeTex(color);
        }

        private static void Button()
        {
            if (EditorGUIUtility.isProSkin)
            {
                _button = new GUIStyle(EditorStyles.helpBox);

                Color hoverColor = new Color(1f, 0.5f, 0f, 0.5f);

                _button.normal.background = MakeTexUnderline(new Color(1f, 1f, 1f, 0.2f));
                _button.hover.background = MakeTexUnderline(hoverColor);
                _button.active.background = MakeTexUnderline(new Color(1f, 0.5f, 0f, 1f));
                _button.focused.background = MakeTexUnderline(hoverColor);

                _button.normal.textColor = Color.white;
                _button.hover.textColor = Color.white;
                _button.active.textColor = Color.white;
                _button.focused.textColor = Color.white;

                _button.fontSize = 12;
                _button.alignment = TextAnchor.MiddleCenter;
            }
            else
            {
                _button = new GUIStyle(GUI.skin.button);
            }
        }

        private static Texture2D MakeTex(Color col)
        {
            Color[] pix = new Color[1] { col };
            Texture2D result = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            result.SetPixels(pix);
            result.Apply(true);
            return result;
        }

        private static Texture2D MakeTexUnderline(Color color)
        {
            int squareSize = 16;
            int width = squareSize + 1;

            Texture2D result = new Texture2D(width, width, TextureFormat.ARGB32, false);

            Color[] pixels = new Color[width * width];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Color orange = new Color(1f, 0.5f, 0f, 1f);
            pixels[7] = orange;
            pixels[8] = orange;
            pixels[9] = orange;

            result.SetPixels(pixels);
            result.Apply(true);
            return result;
        }

        private static Texture2D MakeTexUnderlineLeft(Color color)
        {
            int squareSize = 16;
            int width = squareSize + 1;

            Texture2D result = new Texture2D(width, width, TextureFormat.ARGB32, false);

            Color[] pixels = new Color[width * width];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Color orange = new Color(1f, 0.5f, 0f, 1f);

            for (int i = 0; i < 6; i++)
                pixels[i] = orange;

            result.SetPixels(pixels);
            result.Apply(true);
            return result;
        }
    }
}