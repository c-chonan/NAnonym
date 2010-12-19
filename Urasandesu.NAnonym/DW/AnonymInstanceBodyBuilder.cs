/* 
 * File: AnonymInstanceBodyBuilder.cs
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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using SRE = System.Reflection.Emit;

namespace Urasandesu.NAnonym.DW
{
    class AnonymInstanceBodyBuilder : MethodBodyWeaveBuilder
    {
        public AnonymInstanceBodyBuilder(MethodBodyWeaveDefiner parentBodyDefiner)
            : base(parentBodyDefiner)
        {
        }

        public override void Construct()
        {
            var bodyDefiner = ParentBodyDefiner.ParentBody;
            var definer = bodyDefiner.ParentBuilder.ParentDefiner;

            var injectionMethod = definer.WeaveMethod;
            var gen = bodyDefiner.Gen;
            var ownerType = definer.Parent.ConstructorWeaver.DeclaringType;
            var cachedMethod = definer.CachedMethod;
            var cachedSetting = definer.CachedSetting;
            var returnType = injectionMethod.Source.ReturnType;
            var parameterTypes = definer.ParameterTypes;

            gen.Eval(_ => _.If(_.Ld(_.X(cachedMethod.Name)) == null));
            {
                var dynamicMethod = default(DynamicMethod);
                gen.Eval(_ => _.St(dynamicMethod).As(new DynamicMethod(
                                                            "dynamicMethod",
                                                            _.X(returnType),
                                                            new Type[] { _.X(ownerType) }.Concat(_.X(parameterTypes)).ToArray(),
                                                            _.X(ownerType),
                                                            true)));


                var cacheField = default(FieldInfo);
                gen.Eval(_ => _.St(cacheField).As(_.X(ownerType).GetField(
                                                        _.X(cachedSetting.Name),
                                                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)));

                var targetMethod = default(MethodInfo);
                gen.Eval(_ => _.St(targetMethod).As(_.X(injectionMethod.Destination.DeclaringType).GetMethod(
                                                        _.X(injectionMethod.Destination.Name),
                                                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)));


                var il = default(ILGenerator);
                gen.Eval(_ => _.St(il).As(dynamicMethod.GetILGenerator()));
                // �Ⴆ�΂���Ȋ����łǂ��H
                // 1. gen.EvalEmit(_ => _.Return(_.Invoke(_.This(), _.X(targetMethod), _.Ld(_.X(injectionMethod.Source.ParameterNames())))), () => il);
                //    �ˊ��ʑ�����c
                // 2. gen.EvalEmit(_ => _.Return(targetMethod.Invoke(_.This(), _.Ld(_.X(injectionMethod.Source.ParameterNames())))), () => il);
                //    ��Reflection �����̂܂܎g�������̂�������Ȃ��B���f�s�\����c
                // 
                // Decoration �p�^�[���͂ǂ����I
                //  �܂����O��������肪(��)
                //  ExpressiveMethodBodyGenerator -> ExpressiveGenerator
                //  ExpressiveMethodBodyGenerationEmitter -> GenerativeEmitter
                //  ExpressiveMethodBodyReflectionOptimizer -> ReflectionOptimizer
                // 3. var emitter = new GenerativeEmitter(gen, () => il);
                //    {
                //        emitter.Eval(_ => _.Return(_.Invoke(_.This(), _.X(targetMethod), _.Ld(_.X(injectionMethod.Source.ParameterNames())))));
                //    }
                // 4. var emitter = new GenerativeEmitter(gen, () => il);
                //    var optimizer = new ReflectionOptimizer(emitter);
                //    {
                //        optimizer.Eval(_ => _.Return(targetMethod.Invoke(_.This(), _.Ld(_.X(injectionMethod.Source.ParameterNames())))));
                //    }
                // 
                // �ŏI�I�ɂ́ALd �� X �͓�������邩��c   
                // 5. var emitter = new GenerativeEmitter(gen, () => il);
                //    var optimizer = new ReflectionOptimizer(emitter);
                //    {
                //        optimizer.Eval(_ => _.Return(targetMethod.Invoke(_.This(), _.Ld(injectionMethod.Source.ParameterNames()))));
                //    }
                // 
                // 
                //    
                // 
                //    
                // 
                // �ŏ��� Reflection �ŏ����Ă��Ȃ�A���ꂪ���̂܂܎����Ă��ꂽ�ق��������񂶂�ˁH
                // ��2. ���ǂ����B
                // �˂��̍l�������āAMethodInfo ��������Ȃ��AType �� FieldInfo�APropertyInfo �ɂ����������B
                // 


                gen.Eval(_ => il.Emit(SRE::OpCodes.Ldarg_0));
                gen.Eval(_ => il.Emit(SRE::OpCodes.Ldfld, cacheField));
                for (int parametersIndex = 0; parametersIndex < parameterTypes.Length; parametersIndex++)
                {
                    switch (parametersIndex)
                    {
                        case 0:
                            gen.Eval(_ => il.Emit(SRE::OpCodes.Ldarg_1));
                            break;
                        case 1:
                            gen.Eval(_ => il.Emit(SRE::OpCodes.Ldarg_2));
                            break;
                        case 2:
                            gen.Eval(_ => il.Emit(SRE::OpCodes.Ldarg_3));
                            break;
                        case 3:
                            gen.Eval(_ => il.Emit(SRE::OpCodes.Ldarg, (short)4));
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
                gen.Eval(_ => il.Emit(SRE::OpCodes.Callvirt, targetMethod));
                gen.Eval(_ => il.Emit(SRE::OpCodes.Ret));
                gen.Eval(_ => _.St(_.X(cachedMethod.Name)).As(dynamicMethod.CreateDelegate(_.X(injectionMethod.DelegateType), _.This())));
            }
            gen.Eval(_ => _.EndIf());
            var invoke = injectionMethod.DelegateType.GetMethod(
                                                        "Invoke",
                                                        BindingFlags.Public | BindingFlags.Instance,
                                                        null,
                                                        parameterTypes,
                                                        null);
            gen.Eval(_ => _.Return(_.Invoke(_.Ld(_.X(cachedMethod.Name)), _.X(invoke), _.Ld(_.X(injectionMethod.Source.ParameterNames())))));
        }
    }
}

