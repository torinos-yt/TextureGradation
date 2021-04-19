using UnityEngine;
using UnityEditor;
using Unity.Mathematics;

namespace TextureGradation 
{
    [System.Serializable]
    public sealed class BoxGradient
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

                x = x / texture.width - .5f;
                y = y / texture.width - .5f;

                var p = sdBox(new float2(x, y)) * -2f;
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

        float sdBox(float2 p)
        {
            float2 d = math.abs(p)-.5f;
            return math.length(math.max(d,0f)) + math.min(math.max(d.x,d.y),0f);
        }

    }
}
