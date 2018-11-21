using Blockly.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Blockly.Blocks.Text
{
    public class TextLength : IBlock
    {
        public override object Evaluate(Context context)
        {
            var text = (this.Values.Evaluate("VALUE", context) ?? "").ToString();

            return text.Length;
        }

		public override SyntaxNode Generate(Context context)
		{
			var textExpression = this.Values.Generate("VALUE", context) as ExpressionSyntax;
			if (textExpression == null) throw new ApplicationException($"Unknown expression for text.");

			return SyntaxGenerator.PropertyAccessExpression(textExpression, nameof(string.Length));
		}
	}
}