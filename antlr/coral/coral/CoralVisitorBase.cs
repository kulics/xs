﻿using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coral
{
    partial class CoralVisitorBase : CoralBaseVisitor<object>
    {
        const string Wrap = "\r\n";

        public override object VisitProgram([NotNull] CoralParser.ProgramContext context)
        {
            var list = context.statement();
            var result = "";
            foreach(var item in list)
            {
                result += VisitStatement(item);
            }
            return result;
        }

        public class Result
        {
            public object data { get; set; }
            public string text { get; set; }
            public string permission { get; set; }
        }

        public override object VisitId([NotNull] CoralParser.IdContext context)
        {
            var r = new Result();
            r.data = "var";
            if(context.typeBasic() != null)
            {
                r.permission = "public";
                r.text += Visit(context.typeBasic());
            }
            else if(context.op.Type == CoralParser.IDPublic)
            {
                r.permission = "public";
                r.text += context.op.Text;
            }
            else if(context.op.Type == CoralParser.IDPrivate)
            {
                r.permission = "private";
                r.text += context.op.Text;
            }

            if(keywords.IndexOf(r.text) >= 0)
            {
                r.text = "@" + r.text;
            }
            return r;
        }

        public override object VisitBool([NotNull] CoralParser.BoolContext context)
        {
            var r = new Result();
            if(context.t.Type == CoralParser.True)
            {
                r.data = "bool";
                r.text = context.True().GetText();
            }
            else if(context.t.Type == CoralParser.False)
            {
                r.data = "bool";
                r.text = context.False().GetText();
            }
            return r;
        }

    }
}