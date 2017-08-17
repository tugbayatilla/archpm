﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ArchPM.Core.Extensions.ObjectExtensions;
using ArchPM.Core.Extensions.TypeExtensions;
using ArchPM.Core.Extensions;
using ArchPM.QueryBuilder.ContentItems;

namespace ArchPM.QueryBuilder.MethodCalls
{
    class IsNullOrEmpty : IMethodCall
    {
        public String Name { get { return "IsNullOrEmpty"; } }

        public void Handle(DecisionMaker DecisionMaker, List<IContentItem> sb, MethodCallExpression expression)
        {
            Expression fieldExpression = expression.Arguments[0];

            sb.Add(new StartBlockContentItem()); //topmost blocks will be removed. so we add 2 blocks
            sb.Add(new StartBlockContentItem());
            DecisionMaker.ExpressionHandle(sb, fieldExpression);
            sb.Add(new OperatorContentItem(Operators.IS));
            sb.Add(new ValueContentItem() { Value = null });
            sb.Add(new OperatorContentItem(Operators.OR));
            DecisionMaker.ExpressionHandle(sb, fieldExpression);
            sb.Add(new OperatorContentItem(Operators.EQUALS));
            sb.Add(new ValueContentItem() { Value = String.Empty });
            sb.Add(new EndBlockContentItem());
            sb.Add(new EndBlockContentItem());
        }
    }
}
