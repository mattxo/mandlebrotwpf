using OpenTK;

namespace UI
{
    public class DynamicSpiderwebPresetModel : PresetModel
    {
        public DynamicSpiderwebPresetModel()
        {
            Name = "Dynamic Spiderwebs";
            Center = new Vector2(-0.54f, -0.12f);
            Scale = 2.04f;
            MaxIterations = 2;
            Code =
            @"
                z = c_pow(z, 2) + c;
                z = c_sin(z) + c_cos(z) + frameIndex / 200.0;
                z = tan(c_sin(z) + c_cos(z));
            ";
        }
    }

    
}
