namespace UI
{
    public abstract class Uniform
    {
        protected readonly string _name;

        public Uniform(string name)
        {
            _name = name;
        }

        public abstract void Set(ShaderProgram program);

        public int GetLocation(ShaderProgram program) => program.GetUniformLocation(_name);
    }
}