using UnityEngine;
using UnityEditor;
#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif

namespace TextureGradation
{
    [ScriptedImporter(1, "gradation")]
    public sealed class TextureGradationImporter : ScriptedImporter
    {
        [SerializeField] Shape _shape = Shape.Line;
        [SerializeField] Gradient _gradient = new Gradient();
        [SerializeField] uint _resolution = 128;
        [SerializeField] LineGradient _line = new LineGradient();
        [SerializeField] CircularGradient _circular = new CircularGradient();
        [SerializeField] RadialGradient _radial = new RadialGradient();
        [SerializeField] DiagonalGradient _diagonal = new DiagonalGradient();
        [SerializeField] BoxGradient _box = new BoxGradient();

        public override void OnImportAsset(AssetImportContext context)
        {
            Texture2D texture;

            switch(_shape)
            {
                case Shape.Line     : texture = _line.GenerateTexture(_resolution, _gradient); break;
                case Shape.Circular : texture = _circular.GenerateTexture(_resolution, _gradient); break;
                case Shape.Radial   : texture = _radial.GenerateTexture(_resolution, _gradient); break;
                case Shape.Diagonal : texture = _diagonal.GenerateTexture(_resolution, _gradient); break;
                case Shape.Box      : texture = _box.GenerateTexture(_resolution, _gradient); break;
                default             : texture = new Texture2D(1,1); break;
            }

            context.AddObjectToAsset("texture", texture);
            context.SetMainObject(texture);
        }
    }

    public enum Shape{ Line, Circular, Radial, Diagonal, Box }
}
