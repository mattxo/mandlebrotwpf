using OpenTK;

namespace UI
{
    public class SinMandlebrotPresetModel : PresetModel
    {
        public SinMandlebrotPresetModel()
        {
            Name = "Sin Mandledrot";
            Center = new Vector2(-0.58f, -0.07f);
            Scale = 1.3f;
            MaxIterations = 8;
            Code =
            @"
                z = c_pow(z, 2) + c;
                z = c_sin(z);
                z = z + frameIndex / 2000.0;
                z = z + vec2(mousePosition.x, mousePosition.y);
            ";
        }
    }
}
