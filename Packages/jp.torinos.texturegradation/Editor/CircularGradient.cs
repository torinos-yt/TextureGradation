using UnityEngine;
using UnityEditor;

namespace TextureGradation 
{
    [System.Serializable]
    public sealed class CircularGradient
    {
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

                x /= texture.width;
                y /= texture.width;

                var p = Vector2.Distance(new Vector2(x,y), new Vector2(.5f,.5f))*1.5f;
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
