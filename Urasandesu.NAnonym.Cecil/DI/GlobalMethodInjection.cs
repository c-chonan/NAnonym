﻿using System.Collections.Generic;
using Urasandesu.NAnonym.DI;

namespace Urasandesu.NAnonym.Cecil.DI
{
    class GlobalMethodInjection : MethodInjection
    {
        public new GlobalConstructorInjection ConstructorInjection { get { return (GlobalConstructorInjection)base.ConstructorInjection; } }
        public GlobalMethodInjection(GlobalConstructorInjection constructorInjection, HashSet<TargetMethodInfo> methodSet)
            : base(constructorInjection, methodSet)
        {
        }

        protected override void ApplyContent(TargetMethodInfo injectionMethod)
        {
            var definer = GlobalMethodInjectionDefiner.GetInstance(this, injectionMethod);
            definer.Create();

            var builder = new GlobalMethodInjectionBuilder(definer);
            builder.Construct();
        }
    }
}
