using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UI
{

    sealed class Matrix4Uniform : Uniform
    {        
        private Matrix4 _matrix;

        public Matrix4 Matrix
        {
            get => _matrix;
            set { _matrix = value; }
        }

        public Matrix4Uniform(string name) : base(name)
        {            
        }

        public override void Set(ShaderProgram program)
        {
            // get uniform location
            var location = GetLocation(program);

            // set uniform value
            GL.UniformMatrix4(location, false, ref _matrix);            
        }
    }
}