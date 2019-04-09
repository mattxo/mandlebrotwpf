using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace UI
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private GLControl _glControl;        
        private ProgramViewModel _programViewModel;

        ShaderProgram Program
        {
            get => _programViewModel.Program;
            set => _programViewModel.Program = value;
        }
        
        public ProgramViewModel ProgramViewModel
        {
            get => _programViewModel;
            set
            {
                _programViewModel = value;

                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();                        
            InitializeProgramViewModel();
            HookWindowEvents();
            
            DataContext = this;

            CompositionTarget.Rendering += OnRendering;
        }

        private void InitializeProgramViewModel()
        {
            var program = new MandlebrotProgram
            {
                Options = new ShaderProgramOptions()
            };

            ProgramViewModel = new ProgramViewModel(program);
        }

        private void HookWindowEvents()
        {
            Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PresetComboBox.SelectedIndex = 0;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OnResize();
        }

        private void OnResize()
        {
            DoEvents();

            GL.Viewport(0, 0, _glControl.Width, _glControl.Height);

            ProgramViewModel.Resize(_glControl.Width, _glControl.Height);
        }

        private void DoEvents()
        {
            Action EmptyDelegate = () => { };

            Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

        private void WindowsFormsHost_Initialized(object sender, EventArgs e)
        {
            _glControl = CreateGlControl();

            HookGlControlEvents();

            WindowsFormsHost.Child = _glControl;            
        }

        private void HookGlControlEvents()
        {
            _glControl.MouseWheel += MainWindow_MouseWheel;
            _glControl.MouseMove += GlControl_MouseMove;
            _glControl.MouseLeave += GlControl_MouseLeave;
        }

        private GLControl CreateGlControl()
        {
            var flags = GraphicsContextFlags.Default;

            var control = new GLControl(new GraphicsMode(32, 24), 2, 0, flags);
            control.MakeCurrent();
            control.Dock = DockStyle.Fill;

            return control;
        }

        private void GlControl_MouseLeave(object sender, EventArgs e)
        {
            ProgramViewModel.MousePosition = new Vector2(0, 0);
        }

        private void GlControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var x = e.X / (float) _glControl.Width;
            var y = e.Y / (float)_glControl.Height;

            ProgramViewModel.MousePosition = new Vector2(e.X / (float)Width - 1, e.Y / (float)Height - 1);            
        }

        private void OnRendering(object sender, EventArgs e)
        {
            Title = $"Iterations: {ProgramViewModel.Program.MaxIterations} | Scale : { ProgramViewModel.Program.Scale }";

            // clear the screen
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // activate shader program and set uniforms
            ProgramViewModel.Program.Use();

            // reset state for potential further draw calls (optional, but good practice)
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.UseProgram(0);

            // swap backbuffer
            _glControl.SwapBuffers();
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F5) CompileAndExecute();

            if (e.Key == Key.F11)
            {
                Pane.Width = Pane.Width == 0 ? Pane.Width = double.NaN : Pane.Width = 0;

                OnResize();
            }

            if (e.Source != WindowsFormsHost)
            {
                return;
            }            

            float delta = 0.1f * (float)Program.Scale;

            if (e.Key == Key.A) ProgramViewModel.Center = new Vector2(Program.Center.X - delta, Program.Center.Y);
            if (e.Key == Key.D) ProgramViewModel.Center = new Vector2(Program.Center.X + delta, Program.Center.Y);
            if (e.Key == Key.W) ProgramViewModel.Center = new Vector2(Program.Center.X, Program.Center.Y - delta);
            if (e.Key == Key.S) ProgramViewModel.Center = new Vector2(Program.Center.X, Program.Center.Y + delta);
            if (e.Key == Key.Q) ProgramViewModel.ScaleOut();
            if (e.Key == Key.E) ProgramViewModel.ScaleIn();
            if (e.Key == Key.Space) ProgramViewModel.TogglePause();
            if (e.Key == Key.Left) Program.Options.FrameCount--;
            if (e.Key == Key.Right) Program.Options.FrameCount++;
            if (e.Key == Key.Oem4 || e.Key == Key.F) if (Program.MaxIterations > 1) ProgramViewModel.MaxIterations--;
            if (e.Key == Key.Oem6 || e.Key == Key.R) ProgramViewModel.MaxIterations++;
            if (e.Key == Key.C) ProgramViewModel.ToggleCycleColors();

            e.Handled = true;

            base.OnKeyDown(e);
        }

        private void MainWindow_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ProgramViewModel.ScaleOut();
            }
            else
            {
                ProgramViewModel.ScaleIn();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePreset();
        }

        private void UpdatePreset()
        {
            var preset = (PresetModel)PresetComboBox.SelectedValue;
            var code = CleanPresetCode(preset.Code);

            Equation.Text = code;

            ProgramViewModel.LoadPreset(preset);

            CompileAndExecute();
        }
      
        private string CleanPresetCode(string code)
        {
            var split = code.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var clean = split.Select(x => x.Trim()).ToList();
            var joined = string.Join(Environment.NewLine, clean);

            return joined;
        }

        private void CompileAndExecute_OnClick(object sender, RoutedEventArgs e)
        {
            CompileAndExecute();
        }

        private void CompileAndExecute()
        {
            var options = Program.Options;

            Errors.Content = null;

            try
            {
                Program = new MandlebrotProgram(Equation.Text);
                Program.Options = options;
                Program.Options.FrameCount = 0;

                WindowsFormsHost.Focus();
            }
            catch (ApplicationException ex)
            {
                Errors.Content = ex.Message;
            }

            OnResize();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }        
    }
}
