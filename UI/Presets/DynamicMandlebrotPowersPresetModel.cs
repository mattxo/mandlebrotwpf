using OpenTK;

namespace UI
{
    public class DynamicMandlebrotPowersPresetModel : PresetModel
    {
        public DynamicMandlebrotPowersPresetModel()
        {
            Name = "Dynamic Mandlebrot Powers";
            Center = new Vector2(-0.178f, 0);
            Scale = 1.77f;
            Code =
            @"
                float frameDelta = 1 + frameIndex / 100.0;
                z = c_pow(z, frameDelta) + c;
            ";
        }
    }
}
