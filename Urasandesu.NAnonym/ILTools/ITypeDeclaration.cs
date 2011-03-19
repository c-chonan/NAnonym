/* 
 * File: ITypeDeclaration.cs
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
using System.Reflection;
using System.Collections.ObjectModel;

namespace Urasandesu.NAnonym.ILTools
{
    public interface ITypeDeclaration : IMemberDeclaration
    {
        string FullName { get; }
        string AssemblyQualifiedName { get; }
        ITypeDeclaration BaseType { get; }
        IModuleDeclaration Module { get; }
        ReadOnlyCollection<IFieldDeclaration> Fields { get; }
        ReadOnlyCollection<IConstructorDeclaration> Constructors { get; }
        ReadOnlyCollection<IMethodDeclaration> Methods { get; }
        IConstructorDeclaration GetConstructor(Type[] types);
        new Type Source { get; }
        bool IsValueType { get; }
        bool IsAssignableFrom(ITypeDeclaration that);
        bool IsAssignableExplicitlyFrom(ITypeDeclaration that);
        bool EqualsWithoutGenericArguments(ITypeDeclaration that);
        ReadOnlyCollection<ITypeDeclaration> Interfaces { get; }
        ITypeDeclaration MakeArrayType();
        ITypeDeclaration GetElementType();
        ReadOnlyCollection<IPropertyDeclaration> Properties { get; }
    }
}
