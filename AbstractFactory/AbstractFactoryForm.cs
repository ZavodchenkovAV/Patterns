﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AbstractFactory.Factories.Base;
using AbstractFactory.Families.Base;

namespace AbstractFactory
{
    public partial class AbstractFactoryForm : Form
    {
        private Graphics graphics;
        private Point location;
        private Type _currentFactoryType;
        public AbstractFactoryForm()
        {
            InitializeComponent();
        }

        private void AbstractFactoryForm_Load(object sender, EventArgs e)
        {
            graphics = panel2.CreateGraphics();
            location = new Point(panel2.Width / 2, panel2.Height / 2);
            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(t => t.GetInterface("IGuiFactory")!=null);
            int y = 20;
            foreach (var type in types)
            {
                var descrAttr = Attribute.GetCustomAttributes(type, typeof(DescriptionAttribute)).FirstOrDefault();
                string text = (descrAttr as DescriptionAttribute).Description;

                RadioButton rb = new RadioButton();
                rb.Text = text;
                rb.Tag = type;
                rb.Size = new Size(100, 25);
                rb.Location = new Point(20, y);
                rb.CheckedChanged -= rb_CheckedChanged;
                rb.CheckedChanged += rb_CheckedChanged;
                groupBox1.Controls.Add(rb);
                rb.Checked = true;
                y += 20;
            }
        }
        void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                _currentFactoryType = rb.Tag as Type;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var factory =  Activator.CreateInstance(_currentFactoryType) as IGuiFactory;
            var ellipse = factory.CreateEllipse();
            var rectElement = factory.CreateRectangle();
            var star = factory.CreateStar();
            List<BaseElement> elements = new List<BaseElement>(){ellipse,rectElement,star};
            DrawElements(elements);
        }
        private void DrawElements(List<BaseElement> elements)
        {
            graphics.Clear(Color.LightGray);
            int x = 50;
            foreach (var baseElement in elements)
            {
                Rectangle rect = new Rectangle(x, 100, 100, 100);
                baseElement.Graphics = graphics;
                baseElement.Rect = rect;
                baseElement.Draw();
                x += 200;
            }
        }
    }
}