using System;

namespace HouraiTeahouse {
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiredCharacterComponentAttribute : Attribute {
        public bool Runtime { get; private set; }

        public RequiredCharacterComponentAttribute(bool runtime = false) {
            Runtime = runtime;
        }
    }
}