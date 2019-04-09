using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;

namespace UI
{
    public class Shader
    {
        public int Handle { get; }

        public Shader(ShaderType type, string code)
        {
            // create shader object
            Handle = GL.CreateShader(type);

            // set source and compile shader
            GL.ShaderSource(this.Handle, code);
            GL.CompileShader(this.Handle);

            var info = GL.GetShaderInfoLog(Handle);
            int statusCode;

            Debug.WriteLine(string.Format("compile: {0}", info));
            GL.GetShader(Handle, ShaderParameter.CompileStatus, out statusCode);
            if (statusCode != 1) throw new ApplicationException(info);
        }
    }
}
