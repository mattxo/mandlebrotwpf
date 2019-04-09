using OpenTK;

namespace UI
{
    public class JuliaMousePresetModel : PresetModel
    {
        public JuliaMousePresetModel()
        {
            Name = "Julia (Mouse)";
            Center = new Vector2(-0.178f, 0);
            Scale = 1.77f;
            Code =
            @"
                z = c_pow(z, 2);                 
                z = z + vec2(mousePosition.x, mousePosition.y); 
            ";
        }
    }
}
