using OpenTK;

namespace UI
{
    public class ShaderProgramOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double Scale { get; set; } = 1;
        public Vector2 Center { get; set; } = new Vector2();
        public bool IsPaused { get; set; } = false;
        public int FrameCount { get; set; }
        public int MaxIterations { get; set; } = 50;
        public bool CycleColors { get; set; } = true;
        public float ColorCompressionFactor { get; set; } = 1;
        public Vector2 MousePosition { get; set; }
    }
}
