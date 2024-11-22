using System;

namespace YG.Insides
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApplySettingsAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class SelectPlatformAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class DeletePlatformAttribute : Attribute { }
}