using OpenTK;

namespace UI
{
    public class PresetModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Vector2 Center { get; set; } = new Vector2();
        public float Scale { get; set; } = 1f;
        public int MaxIterations { get; set; } = 50;
    }
}
