﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;

namespace Urasandesu.NAnonym.ILTools.Impl.System.Reflection
{
    class SRMethodBaseGeneratorImpl : SRMethodBaseDeclarationImpl, IMethodBaseGenerator
    {
        readonly MethodBase methodBase;
        public SRMethodBaseGeneratorImpl(MethodBase methodBase)
            : base(methodBase)
        {
            this.methodBase = methodBase;
        }

        #region IMethodBaseGenerator メンバ

        public new IMethodBodyGenerator Body
        {
            get { throw new NotImplementedException(); }
        }

        public new ITypeGenerator DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public new ReadOnlyCollection<IParameterGenerator> Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public IPortableScopeItem AddPortableScopeItem(FieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        public IMethodBaseGenerator CreateInstance(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
