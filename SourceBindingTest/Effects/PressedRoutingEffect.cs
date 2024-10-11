using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
#if IOS
using UIKit;
#endif
#if WINDOWS
using Microsoft.UI.Xaml;
#endif

namespace SourceBindingTest.Effects
{
    public class PressedRoutingEffect : RoutingEffect
    {
        public static readonly BindableProperty ClickCommandProperty = BindableProperty.CreateAttached("ClickCommand", typeof(ICommand), typeof(PressedRoutingEffect), (object)null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(PressedRoutingEffect), (object)null);

        public static ICommand GetClickCommand(BindableObject view)
        {
            var val = view.GetValue(ClickCommandProperty);

            return val as ICommand;
        }

        public static void SetClickCommand(BindableObject view, ICommand value)
        {
            view.SetValue(ClickCommandProperty, value);
        }

        public static object GetCommandParameter(BindableObject view)
        {
            return view.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(BindableObject view, object value)
        {
            view.SetValue(CommandParameterProperty, value);
        }
    }

#if WINDOWS
    public class PressedPlatformEffect : Microsoft.Maui.Controls.Platform.PlatformEffect
    {
        public PressedPlatformEffect() : base()
        {
        }

        private bool _attached;

        /// <summary>
        /// Apply the handler
        /// </summary>
        protected override void OnAttached()
        {
            if (!_attached)
            {
                Container.Tapped += Container_Tapped;
                _attached = true;
            }
        }

        private void Container_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var command = PressedRoutingEffect.GetClickCommand(Element);
            command?.Execute(PressedRoutingEffect.GetCommandParameter(Element));
        }

        /// <summary>
        /// Clean the event handler on detach
        /// </summary>
        protected override void OnDetached()
        {
            if (_attached)
            {
                Container.Tapped -= Container_Tapped;
                _attached = false;
            }
        }
    }
#elif ANDROID
    public class PressedPlatformEffect : Microsoft.Maui.Controls.Platform.PlatformEffect
    {
        private bool _attached;

        public PressedPlatformEffect() : base()
        {
        }

        protected override void OnAttached()
        {
            if (!_attached)
            {
                if (Control != null)
                {
                    Control.Click += Control_Click;
                }
                else
                {
                    Control.Click += Control_Click;
                }
                _attached = true;
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            var command = PressedRoutingEffect.GetClickCommand(Element);
            command?.Execute(PressedRoutingEffect.GetCommandParameter(Element));
        }

        protected override void OnDetached()
        {
            if (_attached)
            {
                if (Control != null)
                {
                    Control.Click -= Control_Click;

                }
                else
                {
                    Control.Click -= Control_Click;
                }
                _attached = false;
            }
        }
    }
#elif IOS
    public class PressedPlatformEffect : Microsoft.Maui.Controls.Platform.PlatformEffect
    {
        private bool _attached;
        private readonly UITapGestureRecognizer tapGestureRecognizer;

        public PressedPlatformEffect() : base()
        {
            tapGestureRecognizer = new UITapGestureRecognizer(Control_Click);
        }

        protected override void OnAttached()
        {
            //because an effect can be detached immediately after attached (happens in listview), only attach the handler one time
            if (!_attached)
            {
                Container.AddGestureRecognizer(tapGestureRecognizer);
                _attached = true;
            }
        }

        private void Control_Click()
        {
            var command = PressedRoutingEffect.GetClickCommand(Element);
            command?.Execute(PressedRoutingEffect.GetCommandParameter(Element));
        }

        protected override void OnDetached()
        {
            if (_attached)
            {
                Container.RemoveGestureRecognizer(tapGestureRecognizer);
                _attached = false;
            }
        }
    }
#endif
}
