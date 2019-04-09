using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace UI
{
    public abstract class ShaderProgram
    {
        private readonly int _handle;

        public ShaderProgramOptions Options { get; set; } = new ShaderProgramOptions();

        public int Width => Options.Width;
        public int Height => Options.Height;
        public double Scale => Options.Scale;
        public Vector2 Center => Options.Center;
        public Vector2 MousePosition => Options.MousePosition;
        public float ColorCompressionFactor => Options.ColorCompressionFactor;
        public int FrameCount => Options.FrameCount;
        public int MaxIterations => Options.MaxIterations;
        public bool CycleColors => Options.CycleColors;

        public bool IsPaused
        {
            get => Options.IsPaused;
            set => Options.IsPaused = value;
        }

        public ShaderProgram(params Shader[] shaders)
        {
            // create program object
            _handle = GL.CreateProgram();

            // assign all shaders
            foreach (var shader in shaders)
            {
                GL.AttachShader(_handle, shader.Handle);
            }

            // link program (effectively compiles it)   
            GL.LinkProgram(_handle);

            var info = GL.GetProgramInfoLog(_handle);
            
            if (!string.IsNullOrWhiteSpace(info))
            {
                Debug.WriteLine($"GL.LinkProgram had info log: {info}");
                throw new Exception();
            }

            // detach shaders
            foreach (var shader in shaders)
            {
                GL.DetachShader(_handle, shader.Handle);
            }
        }

        // activate this program to be used
        public virtual void Use()
        {
            GL.UseProgram(_handle);

            Draw();
        }

        // get the location of a vertex attribute
        public int GetAttributeLocation(string name) => GL.GetAttribLocation(_handle, name);

        public void TogglePause()
        {
             Options.IsPaused = !Options.IsPaused;
        }

        // get the location of a uniform variable
        public int GetUniformLocation(string name) 
        {
            var location = GL.GetUniformLocation(_handle, name);

            if (location == -1)
            {                
                Debug.WriteLine($"Unform value '{name}' not found");
            }

            return location;
        }
        
        public static string ProcessIncludes(string code, string equation = "z = cPow(z, 2) + c", string functions = null)
        {
            foreach (Match t in Regex.Matches(code, "#include \"(.*)\""))
            {
                var line = t.Groups[0].Value;
                var includePath = t.Groups[1].Value;

                code = Regex.Replace(code, line, File.ReadAllText(includePath));
            }

            code = code.Replace("#equation", equation);            

            return code;
        }

        public static Shader GetFragmentShader(string path, string equation)
        {
            string code = File.ReadAllText(path);

            code = ProcessIncludes(code, equation);

            return new Shader(ShaderType.FragmentShader, code);
        }

        public static Shader GetVertexShader(string path)
        {
            string code = File.ReadAllText(path);

            code = ProcessIncludes(code);

            return new Shader(ShaderType.VertexShader, code);
        }

        public void ToggleCycleColors()
        {
            Options.CycleColors = !Options.CycleColors;
        }

        public void SetUniform4(string name, Vector4 value)
        {
            var location = GetUniformLocation(name);

            GL.Uniform4(location, value);
        }

        public void SetUniform2(string name, Vector2 value)
        {
            var location = GetUniformLocation(name);

            GL.Uniform2(location, value);
        }

        public void SetUniform1(string name, float value)
        {
            var location = GetUniformLocation(name);

            GL.Uniform1(location, value);
        }

        public void SetUniform1(string name, double value)
        {
            var location = GetUniformLocation(name);

            GL.Uniform1(location, value);
        }

        public void SetUniform1(string name, int value)
        {
            var location = GetUniformLocation(name);

            GL.Uniform1(location, value);
        }

        public void SetUniform1(string name, bool value)
        {
            var location = GetUniformLocation(name);

            GL.Uniform1(location, value ? 1 : 0);
        }

        public abstract void Draw();
    }
}
