using OpenTK;

namespace UI
{
    public class WiccaPresetModel : PresetModel
    {
        public WiccaPresetModel()
        {
            Name = "Wicca";
            Center = new Vector2(-0.69f, 0.3f);
            Scale = 3.66f;
            MaxIterations = 10;
            Code =
            @"
                z = c_pow(z, 2) + c / c_pow(z * z + c, 2);
            ";
        }
    }
}
