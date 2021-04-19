using UnityEngine;
using UnityEditor;

namespace TextureGradation 
{
    [System.Serializable]
    public sealed class LinearGradient
    {
        [SerializeField] Axis axis;

        public Texture2D GenerateTexture(uint size, Gradient gradient)
        {
            Texture2D texture;
            if(axis == Axis.U)
                texture = new Texture2D((int)size, 1, TextureFormat.RGBA32, 0, true);
            else
                texture = new Texture2D(1, (int)size, TextureFormat.RGBA32, 0, true);

            SetPixels(texture, gradient);

            return texture;
        }

        void SetPixels(Texture2D texture, Gradient gradient)
        {
            var size = Mathf.Max(texture.width, texture.height);
            Color32[] colorArray = new Color32[size];

            for(int i = 0; i < size; i++)
            {
                var p = i / (float)(size-1);
                colorArray[i] = ConvertColor(gradient.Evaluate(p));
            }

            texture.SetPixels32(colorArray);
            texture.Apply();
        }

        Color ConvertColor(Color c)
        {
            if(PlayerSettings.colorSpace == ColorSpace.Gamma)
                return c;
            else
                return c.linear;
        }

    }

    public enum Axis { U, V }
}
