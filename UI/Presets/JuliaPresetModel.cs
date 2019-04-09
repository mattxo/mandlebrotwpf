using OpenTK;

namespace UI
{
    public class JuliaPresetModel : PresetModel
    {
        public JuliaPresetModel()
        {
            Name = "Julia";
            Center = new Vector2(-0.178f, 0);
            Scale = 1.77f;
            Code = "z = c_pow(z, 2) + 0.259;";
        }
    }
}
