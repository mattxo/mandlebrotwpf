using OpenTK;

namespace UI
{
    public class SpiderwebPresetModel : PresetModel
    {
        public SpiderwebPresetModel()
        {
            Name = "Spiderwebs";
            Center = new Vector2(-0.54f, -0.12f);
            Scale = 2.04f;
            MaxIterations = 2;
            Code =
            @"
                z = c_pow(z, 2) + c;
                z = c_sin(z) + c_cos(z);
                z = tan(c_sin(z) + c_cos(z));
            ";
        }
    }    
}
