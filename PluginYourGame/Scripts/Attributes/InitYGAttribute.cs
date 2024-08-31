using System;
using System.Collections.Generic;
using System.Reflection;

namespace YG
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InitYG_0Attribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class InitYG_1Attribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class InitYG_2Attribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class InitYGAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class StartYGAttribute : Attribute { }

    public partial class YG2
    {
        private static void CallInitYG_0()
        {
            CallIByAttribute(typeof(InitYG_0Attribute));
        }

        private static void CallInitYG_1()
        {
            CallIByAttribute(typeof(InitYG_1Attribute));
        }

        private static void CallInitYG_2()
        {
            CallIByAttribute(typeof(InitYG_2Attribute));
        }

        private static void CallInitYG()
        {
            CallIByAttribute(typeof(InitYGAttribute));
        }

        private static void CallStartYG()
        {
            CallIByAttribute(typeof(StartYGAttribute));
        }

        private static void CallIByAttribute(Type attribute)
        {
            List<Action> methodsToCall = new List<Action>();

            Type type = typeof(YG2);
            MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                var attributes = method.GetCustomAttributes(attribute, true);
                if (attributes.Length > 0)
                {
                    MethodInfo currentMethod = method;
                    methodsToCall.Add(() => currentMethod.Invoke(type, null));
                }
            }
            foreach (var action in methodsToCall)
            {
                action.Invoke();
            }
        }
    }
}