﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Troublemaker.Editor.Framework
{
    public class GrayscaleEffect : DuoTone
    {
        static GrayscaleEffect()
        {
            _pixelShader.UriSource = MakePackUri("Framework/Shaders/GrayscaleEffect.ps");
        }

        public GrayscaleEffect()
        {
            this.PixelShader = _pixelShader;
            Initialize();
        }
    }
    
    public abstract class DuoTone : ShaderEffect
    {
        protected static PixelShader _pixelShader = new PixelShader();

        protected void Initialize()
        {
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DesaturationFactorProperty);
        }

        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(DuoTone), 0);
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty DesaturationFactorProperty = DependencyProperty.Register("DesaturationFactor", typeof(double), typeof(DuoTone), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0), CoerceDesaturationFactor));
        public double DesaturationFactor
        {
            get { return (double)GetValue(DesaturationFactorProperty); }
            set { SetValue(DesaturationFactorProperty, value); }
        }

        protected static object CoerceDesaturationFactor(DependencyObject d, object value)
        {
            DuoTone effect = (DuoTone)d;
            double newDesatFactor = (double)value;

            if (newDesatFactor < 0.0 || newDesatFactor > 1.0)
            {
                return effect.DesaturationFactor;
            }

            return newDesatFactor;
        }
        
        public static Uri MakePackUri(string relativeFile)
        {
            string uriString = "pack://application:,,,/Troublemaker.Editor;component/" + relativeFile;
            return new Uri(uriString);
        }
    }

}
