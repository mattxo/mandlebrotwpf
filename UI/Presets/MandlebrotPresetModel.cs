using OpenTK;

namespace UI
{
    public class MandlebrotPresetModel : PresetModel
    {
        public MandlebrotPresetModel()
        {
            Name = "Mandlebrot";            
            Center = new Vector2(-0.53f, 0);
            Scale = 1.77f;
            Code = "z = c_pow(z, 2) + c;";
        }
    }
}
