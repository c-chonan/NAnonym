/* 
 * File: FuncWithPrev.cs
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
using System.Linq;
using System.Text;

namespace Urasandesu.NAnonym
{
    public delegate TResult FuncWithPrev<TResult>(Func<TResult> prevFunc);
    public delegate TResult FuncWithPrev<T, TResult>(Func<T, TResult> prevFunc, T arg);
    public delegate TResult FuncWithPrev<T1, T2, TResult>(Func<T1, T2, TResult> prevFunc, T1 arg1, T2 arg2);
    public delegate TResult FuncWithPrev<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> prevFunc, T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult FuncWithPrev<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> prevFunc, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}
