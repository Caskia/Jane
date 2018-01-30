//using Jane.Dependency;
//using System.Reflection;

//namespace Jane.Runtime.Validation.Interception
//{
//    internal static class ValidationInterceptorRegistrar
//    {
//        public static void Initialize(IObjectContainer objectConainter)
//        {
//            objectConainter.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
//        }

//        private static void Kernel_ComponentRegistered(string key, IHandler handler)
//        {
//            if (typeof(IApplicationService).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
//            {
//                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ValidationInterceptor)));
//            }
//        }
//    }
//}