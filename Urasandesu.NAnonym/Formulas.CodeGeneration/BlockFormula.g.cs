/* 
 * File: BlockFormula.g.cs
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
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NAnonym.Formulas
{
    public partial class BlockFormula : Formula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
			NodeType = NodeType.Block;
            ParentBlock = default(BlockFormula);
            ChildBlocks = new NodeCollection<BlockFormula>();
            Locals = new NodeCollection<LocalNode>();
            Formulas = new FormulaCollection<Formula>();
        }

        public const string NameOfParentBlock = "ParentBlock";
        BlockFormula parentBlock;
        public BlockFormula ParentBlock 
        { 
            get { return parentBlock; } 
            set 
            {
                SetNode(NameOfParentBlock, value, ref parentBlock);
            }
        }
        public const string NameOfChildBlocks = "ChildBlocks";
        NodeCollection<BlockFormula> childBlocks;
        public NodeCollection<BlockFormula> ChildBlocks 
        { 
            get { return childBlocks; } 
            set 
            {
                SetNode(NameOfChildBlocks, value, ref childBlocks);
            }
        }
        public const string NameOfLocals = "Locals";
        NodeCollection<LocalNode> locals;
        public NodeCollection<LocalNode> Locals 
        { 
            get { return locals; } 
            set 
            {
                SetNode(NameOfLocals, value, ref locals);
            }
        }
        public const string NameOfFormulas = "Formulas";
        FormulaCollection<Formula> formulas;
        public FormulaCollection<Formula> Formulas 
        { 
            get { return formulas; } 
            set 
            {
                SetFormula(NameOfFormulas, value, ref formulas);
            }
        }


        public override void Accept(IFormulaVisitor visitor)
        {
            visitor.Visit(this);
        }


        protected override void PinCore()
        {
            Formula.Pin(ParentBlock);
            Formula.Pin(ChildBlocks);
            Formula.Pin(Locals);
            Formula.Pin(Formulas);
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(NodeToString.Delimiter);
            sb.Append(NodeToString.StartOfName);
            sb.Append(NameOfLocals);
            sb.Append(NodeToString.EndOfName);
            if (Locals == null)
            {
                sb.Append(NodeToString.NullString);
            }
            else
            {
                Locals.AppendWithStartEndTo(sb);
            }
            sb.Append(NodeToString.Delimiter);
            sb.Append(NodeToString.StartOfName);
            sb.Append(NameOfFormulas);
            sb.Append(NodeToString.EndOfName);
            if (Formulas == null)
            {
                sb.Append(NodeToString.NullString);
            }
            else
            {
                Formulas.AppendWithStartEndTo(sb);
            }
        }
    }
}
