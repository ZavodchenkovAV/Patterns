﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Visitors;

namespace Visitor.Elements
{
    public abstract class BaseElement
    {
        public abstract void Accept(BaseVisitor visitor);
        public Rectangle Rect { get; private set; }

        protected BaseElement(Rectangle rect)
        {
            Rect = rect;
        }
    }
}