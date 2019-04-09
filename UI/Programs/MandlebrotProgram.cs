using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace UI
{
    public class MandlebrotProgram : ShaderProgram
    {
        private const string _defaultEquation = "z = c_pow(z, 2) + c;";

        public string Equation { get; set; } = _defaultEquation;

        public MandlebrotProgram(string equation = _defaultEquation) : base
        (
            GetVertexShader("Shaders/Mandlebrot/VertexShader.glsl"),
            GetFragmentShader("Shaders/Mandlebrot/FragmentShader.glsl", equation)
        )
        {            
        }

        public override void Draw()
        {
            SetUniforms();
            ScaleToWindow();            
            DrawSurface();

            if (!IsPaused) Options.FrameCount++;
        }

        private void SetUniforms()
        {
            SetUniform2("center", Center);
            SetUniform1("scale", (float)Scale);
            SetUniform1("maxIterations", MaxIterations);
            SetUniform1("frameIndex", FrameCount);
            SetUniform1("cycleColors", CycleColors);
            SetUniform1("colorCompressionFactor", ColorCompressionFactor);
            SetUniform2("u_mousePosition", MousePosition);
        }

        private void ScaleToWindow()
        {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            var aspectRatio = Width / (float)Height;

            var projection = new Matrix4Uniform("projection")
            {
                Matrix = Matrix4.CreateScale(1, aspectRatio, 1)
            };

            projection.Set(this);
        }

        private void DrawSurface()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(-1, -1);
            GL.Vertex2(1, -1);
            GL.Vertex2(1, 1);
            GL.Vertex2(-1, 1);

            GL.End();
        }
    }    
}
