using UnityEngine;
using UnityEditor;

namespace TextureGradation 
{
    [System.Serializable]
    public sealed class DiagonalGradient
    {
        [SerializeField] float angle;

        public Texture2D GenerateTexture(uint size, Gradient gradient)
        {
            Texture2D texture;
            texture = new Texture2D((int)size, (int)size, TextureFormat.RGBA32, 0, true);
            SetPixels(texture, gradient);

            return texture;
        }

        void SetPixels(Texture2D texture, Gradient gradient)
        {
            int size = texture.width * texture.width;
            Color32[] colorArray = new Color32[size];

            for(int i = 0; i < size; i++)
            {
                var y = Mathf.Floor(i / (float)texture.width);
                var x = i - (y * texture.width);

                x = x / texture.width - .5f;
                y = y / texture.width - .5f;

                var v = new Vector2(1, 1);
                v = Quaternion.Euler(0,0,angle) * v;

                var p = Vector2.Dot(v.normalized, new Vector2(x, y)) / Mathf.Sqrt(2) + .5f;
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
}
