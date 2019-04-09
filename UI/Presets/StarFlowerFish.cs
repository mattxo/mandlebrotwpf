using OpenTK;

namespace UI
{
    public class StarFlowerFish : PresetModel
    {
        public StarFlowerFish()
        {
            Name = "Star Flower Fish";
            Center = new Vector2(-0.224f, 0.12f);
            Scale = 1.94f;
            MaxIterations = 12;
            Code =
            @"
                z = c_pow(z, 3) + c_inv(c) + frameIndex / 2000.0;
                z = c_sin(z);
            ";
        }
    }

}
