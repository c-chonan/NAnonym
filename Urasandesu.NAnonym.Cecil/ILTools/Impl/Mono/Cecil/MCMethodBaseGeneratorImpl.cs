﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.Linq;
using System.Runtime.Serialization;
using MC = Mono.Cecil;
using System.Reflection;
using Mono.Cecil.Cil;
using UNI = Urasandesu.NAnonym.ILTools;
using SR = System.Reflection;

namespace Urasandesu.NAnonym.Cecil.ILTools.Impl.Mono.Cecil
{
    class MCMethodBaseGeneratorImpl : MCMethodBaseDeclarationImpl, UNI::IMethodBaseGenerator
    {
        [NonSerialized]
        ReadOnlyCollection<UNI::IParameterGenerator> parameters;

        public MCMethodBaseGeneratorImpl(MethodDefinition methodDef)
            : base(methodDef)
        {
            parameters = new ReadOnlyCollection<UNI::IParameterGenerator>(
                base.Parameters.TransformEnumerateOnly(paramter => (UNI::IParameterGenerator)paramter));
        }

        public MCMethodBaseGeneratorImpl(MethodDefinition methodDef, ILEmitMode mode, Instruction target)
            : base(methodDef, mode, target)
        {
            parameters = new ReadOnlyCollection<UNI::IParameterGenerator>(
                base.Parameters.TransformEnumerateOnly(paramter => (UNI::IParameterGenerator)paramter));
        }

        public new UNI::IMethodBodyGenerator Body
        {
            get { return (UNI::IMethodBodyGenerator)BodyDecl; }
        }

        public new UNI::ITypeGenerator DeclaringType
        {
            get { return (UNI::ITypeGenerator)DeclaringTypeDecl; }
        }

        public new ReadOnlyCollection<UNI::IParameterGenerator> Parameters
        {
            get { return parameters; }
        }

        public UNI::IPortableScopeItem AddPortableScopeItem(FieldInfo fieldInfo)
        {
            var variableDef = new VariableDefinition(fieldInfo.Name, MethodDef.Module.Import(fieldInfo.FieldType));
            MethodDef.Body.Variables.Add(variableDef);
            var itemRawData = new UNI::PortableScopeItemRawData(this, variableDef.Name, variableDef.Index);
            var fieldDef = new FieldDefinition(itemRawData.FieldName, MC::FieldAttributes.Private | MC::FieldAttributes.SpecialName, MethodDef.Module.Import(fieldInfo.FieldType));
            MethodDef.DeclaringType.Fields.Add(fieldDef);
            return new MCPortableScopeItemImpl(itemRawData, fieldDef, variableDef);
        }

        public UNI::IMethodBaseGenerator ExpressBody(Action<UNI::ExpressiveMethodBodyGenerator> bodyExpression)
        {
            var gen = new UNI::ExpressiveMethodBodyGenerator(this);
            bodyExpression(gen);
            if (gen.Directives.Last().OpCode != UNI::OpCodes.Ret)
            {
                gen.Eval(_ => _.End());
            }
            return this;
        }

        public UNI::IParameterGenerator AddParameter(int position, SR::ParameterAttributes attributes, string parameterName)
        {
            throw new NotImplementedException();
        }

        public UNI::PortableScope CarryPortableScope()
        {
            var scope = new UNI::PortableScope(this);
            return scope;
        }
    }
}