using OpenTK;

namespace UI
{

    public class JuliaFramesPresetModel : PresetModel
    {
        public JuliaFramesPresetModel()
        {
            Name = "Julia (Frames)";
            Center = new Vector2(-0.178f, 0);
            Scale = 1.77f;
            Code =
            @"
                float frameDelta = frameIndex / 1000.0;
                z = c_pow(z, 2); 
                z = z + vec2(frameDelta, frameDelta);                 
            ";
        }
    }
}
