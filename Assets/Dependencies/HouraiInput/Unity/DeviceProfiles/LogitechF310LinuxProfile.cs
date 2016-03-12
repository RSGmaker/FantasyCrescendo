using System;


namespace InControl {
    // @cond nodoc
    [AutoDiscover]
    public class LogitechF310LinuxProfile : UnityInputDeviceProfile {
        public LogitechF310LinuxProfile() {
            Name = "Logitech F310 Controller";
            Meta = "Logitech F310 Controller on Linux";

            SupportedPlatforms = new[] {
                "Linux"
            };

            JoystickNames = new[] {
                "Logitech Gamepad F310"
            };

            MaxUnityVersion = new VersionInfo(4, 9);

            ButtonMappings = new[] {
                new InputControlMapping {
                    Handle = "A",
                    Target = InputControlTarget.Action1,
                    Source = Button0
                },
                new InputControlMapping {
                    Handle = "B",
                    Target = InputControlTarget.Action2,
                    Source = Button1
                },
                new InputControlMapping {
                    Handle = "X",
                    Target = InputControlTarget.Action3,
                    Source = Button2
                },
                new InputControlMapping {
                    Handle = "Y",
                    Target = InputControlTarget.Action4,
                    Source = Button3
                },
                new InputControlMapping {
                    Handle = "Left Bumper",
                    Target = InputControlTarget.LeftBumper,
                    Source = Button4
                },
                new InputControlMapping {
                    Handle = "Right Bumper",
                    Target = InputControlTarget.RightBumper,
                    Source = Button5
                },
                new InputControlMapping {
                    Handle = "Left Stick Button",
                    Target = InputControlTarget.LeftStickButton,
                    Source = Button9
                },
                new InputControlMapping {
                    Handle = "Right Stick Button",
                    Target = InputControlTarget.RightStickButton,
                    Source = Button10
                },
                new InputControlMapping {
                    Handle = "Start",
                    Target = InputControlTarget.Start,
                    Source = Button7
                },
                new InputControlMapping {
                    Handle = "Back",
                    Target = InputControlTarget.Select,
                    Source = Button6
                }
            };

            AnalogMappings = new[] {
                new InputControlMapping {
                    Handle = "Left Stick X",
                    Target = InputControlTarget.LeftStickX,
                    Source = Analog0
                },
                new InputControlMapping {
                    Handle = "Left Stick Y",
                    Target = InputControlTarget.LeftStickY,
                    Source = Analog1,
                    Invert = true
                },
                new InputControlMapping {
                    Handle = "Right Stick X",
                    Target = InputControlTarget.RightStickX,
                    Source = Analog3
                },
                new InputControlMapping {
                    Handle = "Right Stick Y",
                    Target = InputControlTarget.RightStickY,
                    Source = Analog4,
                    Invert = true
                },
                new InputControlMapping {
                    Handle = "Left Trigger",
                    Target = InputControlTarget.LeftTrigger,
                    Source = Analog2
                },
                new InputControlMapping {
                    Handle = "Right Trigger",
                    Target = InputControlTarget.RightTrigger,
                    Source = Analog5
                },
                new InputControlMapping {
                    Handle = "DPad Left",
                    Target = InputControlTarget.DPadLeft,
                    Source = Analog6,
                    SourceRange = InputControlMapping.Range.Negative,
                    TargetRange = InputControlMapping.Range.Negative,
                    Invert = true
                },
                new InputControlMapping {
                    Handle = "DPad Right",
                    Target = InputControlTarget.DPadRight,
                    Source = Analog6,
                    SourceRange = InputControlMapping.Range.Positive,
                    TargetRange = InputControlMapping.Range.Positive
                },
                new InputControlMapping {
                    Handle = "DPad Up",
                    Target = InputControlTarget.DPadUp,
                    Source = Analog7,
                    SourceRange = InputControlMapping.Range.Negative,
                    TargetRange = InputControlMapping.Range.Negative,
                    Invert = true
                },
                new InputControlMapping {
                    Handle = "DPad Down",
                    Target = InputControlTarget.DPadDown,
                    Source = Analog7,
                    SourceRange = InputControlMapping.Range.Positive,
                    TargetRange = InputControlMapping.Range.Positive
                }
            };
        }
    }
}