using OpenTK;
using System;
using System.Collections.Generic;

namespace UI
{
    public class ProgramViewModel : ViewModel
    {
        public ShaderProgramOptions Options => Program.Options;

        public ProgramViewModel(ShaderProgram program)
        {
            Program = program;
        }

        public List<PresetModel> Presets { get; } = new List<PresetModel>()
        {
            new MandlebrotPresetModel(),
            new JuliaPresetModel(),
            new JuliaFramesPresetModel(),
            new JuliaMousePresetModel(),
            new DynamicMandlebrotPowersPresetModel(),
            new SinMandlebrotPresetModel(),
            new WiccaPresetModel(),
            new SpiderwebPresetModel(),
            new DynamicSpiderwebPresetModel(),
            new StarFlowerFish(),
            new HollowStarFlowerFish()
        };

        public int Width { get; set; }
        public int Height { get; set; }

        public bool CycleColors
        {
            get => Options.CycleColors;
            set
            {
                Options.CycleColors = value;

                OnPropertyChanged();
            }
        }

        public float ColorCompressionFactor
        {
            get => Options.ColorCompressionFactor;
            set
            {
                Options.ColorCompressionFactor = value;

                OnPropertyChanged();
            }
        }

        public double Scale
        {
            get => Options.Scale;
            set
            {
                Options.Scale = value;

                OnPropertyChanged();
            }
        }

        public Vector2 Center
        {
            get => Options.Center;
            set
            {
                Options.Center = value;

                OnPropertyChanged();
            }
        }

        public bool IsPaused
        {
            get => Program.IsPaused;
            set
            {
                Program.IsPaused = value;

                OnPropertyChanged();
            }
        }

        public void TogglePause()
        {
            Program.TogglePause();

            OnPropertyChanged("IsPaused");
        }

        public int FrameCount { get; set; }

        public int MaxIterations
        {
            get
            {
                return Options.MaxIterations;
            }
            set
            {
                Options.MaxIterations = value;

                OnPropertyChanged();
            }
        }

        public void ToggleCycleColors()
        {
            CycleColors = !CycleColors;
        }

        public Vector2 MousePosition
        {
            get => Options.MousePosition;
            set => Options.MousePosition = value;
        }

        public ShaderProgram Program { get; set; }

        public void Resize(int width, int height)
        {
            Options.Width = width;
            Options.Height = height;
        }

        public void ScaleIn()
        {
            Scale += Program.Scale * .1;
        }

        public void ScaleOut()
        {
            Scale -= Program.Scale * .1;
        }

        public void LoadPreset(PresetModel preset)
        {
            Scale = preset.Scale;
            Center = preset.Center;
            MaxIterations = preset.MaxIterations;
        }
    }
}
