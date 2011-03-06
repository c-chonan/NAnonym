/* 
 * File: SRILOperatorImpl.cs
 * 
 * Author: Akira Sugiura (urasandesu@gmail.com)
 * 
 * 
 * Copyright (c) 2010 Akira Sugiura
 *  
 *  This software is MIT License.
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 *  THE SOFTWARE.
 */
 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;
using Urasandesu.NAnonym.ILTools.Impl.System.Reflection;
using Urasandesu.NAnonym.Mixins.Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NAnonym.ILTools
{
    sealed class SRILOperatorImpl : IILOperator
    {
        readonly ILGenerator ilGenerator;

        List<SRDirectiveGeneratorImpl> directives;
        ReadOnlyCollection<IDirectiveGenerator> readonlyDirectives;

        SRMethodBodyGeneratorImpl methodBodyGen;

        public SRILOperatorImpl(ILGenerator ilGenerator, SRMethodBodyGeneratorImpl methodBodyGen)
        {
            this.ilGenerator = ilGenerator;
            directives = new List<SRDirectiveGeneratorImpl>();
            readonlyDirectives = new ReadOnlyCollection<IDirectiveGenerator>(directives.TransformEnumerateOnly(directiveGen => (IDirectiveGenerator)directiveGen));
            this.methodBodyGen = methodBodyGen;
        }

        public object Source
        {
            get { throw new NotImplementedException(); }
        }

        public ILocalGenerator AddLocal(string name, Type localType)
        {
            var localBuilder = ilGenerator.DeclareLocal(localType);
            var localGen = new SRLocalGeneratorImpl(name, localBuilder);
            methodBodyGen.LocalGens.Add(localGen);
            return localGen;
        }

        public ILocalGenerator AddLocal(Type localType)
        {
            var localBuilder = ilGenerator.DeclareLocal(localType);
            var localGen = new SRLocalGeneratorImpl(localBuilder);
            methodBodyGen.LocalGens.Add(localGen);
            return localGen;
        }

        public ILocalGenerator AddLocal(Type localType, bool pinned)
        {
            throw new NotImplementedException();
        }

        public ILabelGenerator AddLabel()
        {
            var label = ilGenerator.DefineLabel();
            return new SRLabelGeneratorImpl(label);
        }

        public void Emit(OpCode opcode)
        {
            ilGenerator.Emit(opcode.ToClr());
            directives.Add(new SRDirectiveGeneratorImpl(opcode));
        }

        public void Emit(OpCode opcode, byte arg)
        {
            throw new NotImplementedException();
        }

        public void Emit(OpCode opcode, ConstructorInfo con)
        {
            ilGenerator.Emit(opcode.ToClr(), con);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, con));
        }

        public void Emit(OpCode opcode, double arg)
        {
            throw new NotImplementedException();
        }

        public void Emit(OpCode opcode, FieldInfo field)
        {
            ilGenerator.Emit(opcode.ToClr(), field);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, field));
        }

        public void Emit(OpCode opcode, float arg)
        {
            throw new NotImplementedException();
        }

        public void Emit(OpCode opcode, int arg)
        {
            ilGenerator.Emit(opcode.ToClr(), arg);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, arg));
        }

        public void Emit(OpCode opcode, ILabelDeclaration label)
        {
            ilGenerator.Emit(opcode.ToClr(), ((SRLabelDeclarationImpl)label).Label);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, label));
        }

        public void Emit(OpCode opcode, ILabelDeclaration[] labels)
        {
            throw new NotImplementedException();
        }

        public void Emit(OpCode opcode, ILocalDeclaration local)
        {
            ilGenerator.Emit(opcode.ToClr(), ((SRLocalGeneratorImpl)local).LocalBuilder);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, local));
        }

        public void Emit(OpCode opcode, long arg)
        {
            throw new NotImplementedException();
        }

        public void Emit(OpCode opcode, MethodInfo meth)
        {
            ilGenerator.Emit(opcode.ToClr(), meth);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, meth));
        }

        public void Emit(OpCode opcode, sbyte arg)
        {
            ilGenerator.Emit(opcode.ToClr(), arg);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, arg));
        }

        public void Emit(OpCode opcode, short arg)
        {
            ilGenerator.Emit(opcode.ToClr(), arg);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, arg));
        }

        public void Emit(OpCode opcode, string str)
        {
            ilGenerator.Emit(opcode.ToClr(), str);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, str));
        }

        public void Emit(OpCode opcode, Type cls)
        {
            ilGenerator.Emit(opcode.ToClr(), cls);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, cls));
        }

        public void Emit(OpCode opcode, IConstructorDeclaration constructorDecl)
        {
            ilGenerator.Emit(opcode.ToClr(), ((SRConstructorDeclarationImpl)constructorDecl).ConstructorInfo);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, constructorDecl));
        }

        public void Emit(OpCode opcode, IMethodDeclaration methodDecl)
        {
            ilGenerator.Emit(opcode.ToClr(), ((SRMethodDeclarationImpl)methodDecl).MethodInfo);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, methodDecl));
        }

        public void Emit(OpCode opcode, IParameterDeclaration parameterDecl)
        {
            ilGenerator.Emit(opcode.ToClr(), ((SRParameterDeclarationImpl)parameterDecl).Position);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, parameterDecl));
        }

        public void Emit(OpCode opcode, IFieldDeclaration fieldDecl)
        {
            ilGenerator.Emit(opcode.ToClr(), ((SRFieldDeclarationImpl)fieldDecl).FieldInfo);
            directives.Add(new SRDirectiveGeneratorImpl(opcode, fieldDecl));
        }

        public void Emit(OpCode opcode, IPortableScopeItem scopeItem)
        {
            throw new NotImplementedException();
        }

        public void SetLabel(ILabelDeclaration loc)
        {
            ilGenerator.MarkLabel(((SRLabelDeclarationImpl)loc).Label);
        }

        public ReadOnlyCollection<IDirectiveGenerator> Directives
        {
            get { return readonlyDirectives; }
        }


        public ILocalGenerator AddLocal(string name, ITypeDeclaration localType)
        {
            throw new NotImplementedException();
        }
    }
}

