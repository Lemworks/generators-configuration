//HintName: appsettings.json.g.cs
using System;

namespace Lemworks.Generators.Configuration
{
    public static class AppSettings
    {
        public const string Test = "Test";
        public static class Object
        {
            public const string Value = "Object__Value";
            public static class Object
            {
                public const string Something = "Object__Object__Something";
            }
        }
        public const string Last = "Last";
    }
}