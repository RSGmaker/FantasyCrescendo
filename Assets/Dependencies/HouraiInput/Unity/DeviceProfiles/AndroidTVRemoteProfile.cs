﻿using System;


namespace InControl {
    // Tested with ADT-1
    // Profile by Artūras 'arturaz' Šlajus <arturas@tinylabproductions.com>
    //
    // @cond nodoc
    [AutoDiscover]
    public class AndroidTVRemoteProfile : UnityInputDeviceProfile {
        public AndroidTVRemoteProfile() {
            Name = "Android TV Remote";
            Meta = "Android TV Remotet on Android TV";

            SupportedPlatforms = new[] {
                "Android"
            };

            JoystickNames = new[] {
                "touch-input",
                "navigation-input"
            };

            ButtonMappings = new[] {
                new InputControlMapping {
                    Handle = "A",
                    Target = InputControlTarget.Action1,
                    Source = Button0
                }
            };

            AnalogMappings = new[] {
                new InputControlMapping {
                    Handle = "DPad Left",
                    Target = InputControlTarget.DPadLeft,
                    Source = Analog4,
                    SourceRange = InputControlMapping.Range.Negative,
                    TargetRange = InputControlMapping.Range.Negative,
                    Invert = true
                },
                new InputControlMapping {
                    Handle = "DPad Right",
                    Target = InputControlTarget.DPadRight,
                    Source = Analog4,
                    SourceRange = InputControlMapping.Range.Positive,
                    TargetRange = InputControlMapping.Range.Positive
                },
                new InputControlMapping {
                    Handle = "DPad Up",
                    Target = InputControlTarget.DPadUp,
                    Source = Analog5,
                    SourceRange = InputControlMapping.Range.Negative,
                    TargetRange = InputControlMapping.Range.Negative
                },
                new InputControlMapping {
                    Handle = "DPad Down",
                    Target = InputControlTarget.DPadDown,
                    Source = Analog5,
                    SourceRange = InputControlMapping.Range.Positive,
                    TargetRange = InputControlMapping.Range.Positive,
                    Invert = true
                },
            };
        }
    }
}